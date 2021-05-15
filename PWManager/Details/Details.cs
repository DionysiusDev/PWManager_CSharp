using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging;
using PWManager.SessionUser;

namespace PWManager.Details
{
    public partial class Details : Form
    {
        #region Variable Declarations
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        private bool _blnNew = false;
        private string strOriginalWs = "";
        static string _TableName = "";
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to create new record
        /// </summary>
        public Details()
        {
            _blnNew = true;
            InitializeComponent();
            InitializeDataTable();
        }
    
        /// <summary>
        /// Constructor to open and update existing record
        /// </summary>
        /// <param name="PKID"></param>
        public Details(long PKID)
        {
            InitializeComponent();
            _lngPKID = PKID;
            InitializeDataTable();
        }
        #endregion

        #region Accessors
        /// <summary>
        /// Initializes the data table by getting the record of an existing password or,
        /// create a new row when adding a new password.
        /// </summary>
        private void InitializeDataTable()
        {
            string strCurrentUser = CurrentUser._UserName;
            _TableName = $"{strCurrentUser}Passwords";

            // Get an existing password for Update
            _dtbPassword = PWManager_Model.DLL.PWManagerContext.GetDataTable($"SELECT * FROM {_TableName} WHERE PwId = {_lngPKID}", _TableName);

            // Create an empty row of password info
            if (_blnNew)
            {
                DataRow row = _dtbPassword.NewRow();
                _dtbPassword.Rows.Add(row);
            }
        }
        #endregion

        #region Form Events
        /// <summary>
        /// This method is called when the form first loads.
        /// This method binds the text fields to the data table data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Details_Load(object sender, EventArgs e)
        {
            // Upon loading the form, establish the binding of the controls in the form.
            BindControls();

            // checks its not a new data entry
            if (!_blnNew)
            {
                // after the controls are binded to the data - decrypt data
                DecryptData();
            }

            //if true is passed as args this will set the text fields to read only.
            SetTextReadOnly(true);
        }

        private void Details_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing && !IsDisposed)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    // Stay on this form
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    // exit application
                    case DialogResult.Yes:
                        CurrentUser.ResetUser();
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Control Events
        /// <summary>
        /// handles user interaction with the edit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            SetTextReadOnly(false);
        }

        /// <summary>
        /// handles user interaction with the save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            ValidateData();
            SetTextReadOnly(true);
        }

        /// <summary>
        /// handles user interaction with the back button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, EventArgs e)
        {
            // disposes this details form instance
            Dispose();
            //instantiates a new view all from
            Options.ViewAll frm = new Options.ViewAll();
            //dislays the view all form
            frm.Show();
        }
        #endregion

        #region Data Validation

        /// <summary>
        /// this method validates the data in the text fields and ensure required fields are not empty.
        /// </summary>
        private void ValidateData()
        {
            //used to control data validation
            bool hasWebsite = false;
            bool hasEmail = false;
            bool hasPassword = false;
            bool hasAdditional = false;

            try
            {
                //verifies that the required text fields are not empty
                if (isEmpty(wsTextBox.Text))
                {
                    MessageBox.Show("Please enter a website.");
                }
                else
                {
                    hasWebsite = true;
                }

                if (isEmpty(emTextBox.Text))
                {
                    MessageBox.Show("Please enter an email.");
                }
                else
                {
                    hasEmail = true;
                }

                if (isEmpty(pwTextBox.Text))
                {
                    MessageBox.Show("Please enter a password.");
                }
                else
                {
                    hasPassword = true;
                }

                //updates the additional info text field, if it is empty
                if (isEmpty(adTextBox.Text))
                {
                    //assigns the additional info value to the ad text field
                    adTextBox.Text = "No Additional Information";
                    //writes the text box value and updates the data binding
                    adTextBox.DataBindings["Text"].WriteValue();

                    MessageBox.Show("You are about to save No Additional Information.");

                    hasAdditional = true;
                }
                else
                {
                    hasAdditional = true;
                }

                if (hasWebsite && hasEmail && hasPassword && hasAdditional)
                {
                    if (EncryptData())
                    {
                        SaveData();
                    }
                }
            } catch(Exception e)
            {
                Logger.LogError("[PWManager.Details] [Validate Data] Error validating data " + e);
            }
        }

        /// <summary>
        /// This method checks if a string is empty
        /// </summary>
        /// <param name="data">the string to check</param>
        /// <returns>bool</returns>
        private bool isEmpty(string data)
        {
            if (data.Length < 1)
                return true;

            return false;
        }

        #endregion

        #region Data Encryption and Decryption

        /// <summary>
        /// this method decrypts data prior to displaying to user
        /// </summary>
        /// <returns></returns>
        private void DecryptData()
        {
            string strDecryptedWs = "";
            string strDecryptedEm = "";
            string strDecryptedAd = "";
            string strDecryptedPw = "";

            try
            {
                // decrypt the data
                strDecryptedWs = AESGCM.SimpleDecrypt(_dtbPassword.Rows[0].ItemArray[1].ToString(), Key.DbKey, 0);
                strDecryptedEm = AESGCM.SimpleDecrypt(_dtbPassword.Rows[0].ItemArray[2].ToString(), Key.DbKey, 0);
                strDecryptedAd = AESGCM.SimpleDecrypt(_dtbPassword.Rows[0].ItemArray[3].ToString(), Key.DbKey, 0);
                strDecryptedPw = AESGCM.SimpleDecrypt(_dtbPassword.Rows[0].ItemArray[4].ToString(), Key.DbKey, 0);
            }
            catch (Exception ex)
            {
                Logger.LogError("[Details] [Decrypt Data] Error decrypting data " + ex);
            }

            // displays the decrypted data
            DisplayDecryptedValues(strDecryptedWs, strDecryptedEm, strDecryptedAd, strDecryptedPw);
        }

        /// <summary>
        /// displays the decrypted values to the user
        /// </summary>
        /// <param name="strWs">decrypted website</param>
        /// <param name="strEm">decrypted email</param>
        /// <param name="strAd">decrypted additional info</param>
        /// <param name="strPw">decrypted password</param>
        private void DisplayDecryptedValues(string strWs, string strEm, string strAd, string strPw)
        {
            try
            {
                // sets the encrypted data to the text fields
                wsTextBox.Text = strWs;
                emTextBox.Text = strEm;
                adTextBox.Text = strAd;
                pwTextBox.Text = strPw;

                // writes the values to the binding controls
                wsTextBox.DataBindings["Text"].WriteValue();
                emTextBox.DataBindings["Text"].WriteValue();
                adTextBox.DataBindings["Text"].WriteValue();
                pwTextBox.DataBindings["Text"].WriteValue();
            }
            catch (Exception ex)
            {
                Logger.LogError("[Details] [Display Decrypted Values] Error writing values " + ex);
            }
        }

        /// <summary>
        /// this method encrypts data prior to saving in the database
        /// </summary>
        /// <returns></returns>
        private bool EncryptData()
        {
            // stores website before encrypting
            strOriginalWs = wsTextBox.Text;

            // encrypts the data
            string strEncryptedWs = AESGCM.SimpleEncrypt(wsTextBox.Text, Key.DbKey, null);
            string strEncryptedEm = AESGCM.SimpleEncrypt(emTextBox.Text, Key.DbKey, null);
            string strEncryptedAd = AESGCM.SimpleEncrypt(adTextBox.Text, Key.DbKey, null);
            string strEncryptedPw = AESGCM.SimpleEncrypt(pwTextBox.Text, Key.DbKey, null);

            // sets the encrypted data to the text fields
            wsTextBox.Text = strEncryptedWs;
            emTextBox.Text = strEncryptedEm;
            adTextBox.Text = strEncryptedAd;
            pwTextBox.Text = strEncryptedPw;

            // writes the values to the binding controls
            wsTextBox.DataBindings["Text"].WriteValue();
            emTextBox.DataBindings["Text"].WriteValue();
            adTextBox.DataBindings["Text"].WriteValue();
            pwTextBox.DataBindings["Text"].WriteValue();

            return true;
        }
        #endregion

        /// <summary>
        /// saves the data from the text fields to the data table.
        /// </summary>
        private void SaveData()
        {
            try
            {
                // display message to user to verify Insert Success
                MessageBox.Show(strOriginalWs + " Record Saved.");

                //always do the EndEdit, otherwise the data will not persist.
                _dtbPassword.Rows[0].EndEdit();

                // calls the method in our Data Access Layer to save the changes to the data table
                PWManager_Model.DLL.PWManagerContext.SaveDatabaseTable(_dtbPassword);
            }
            catch(Exception e)
            {
                Logger.LogError("[PWManager.Details] [Save Data] Error saving record " + e);
            }
        }

        #region Helper Methods

        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            wsTextBox.DataBindings.Add("Text", _dtbPassword, "Website");
            emTextBox.DataBindings.Add("Text", _dtbPassword, "Email");
            adTextBox.DataBindings.Add("Text", _dtbPassword, "AdditionalInfo");
            pwTextBox.DataBindings.Add("Text", _dtbPassword, "Password");
        }

        /// <summary>
        /// Sets the text fields to read only.
        /// </summary>
        private void SetTextReadOnly(bool isReadOnly)
        {
            wsTextBox.ReadOnly = isReadOnly;
            emTextBox.ReadOnly = isReadOnly;
            adTextBox.ReadOnly = isReadOnly;
            pwTextBox.ReadOnly = isReadOnly;
        }

        #endregion
    }
}
