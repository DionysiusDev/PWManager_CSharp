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

namespace PWManager.Details
{
    public partial class Details : Form
    {
        #region Variable Declarations
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        private bool _blnNew = false;
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
            // Get an existing password for Update
            _dtbPassword = PWManager_Model.DLL.PWManagerContext.GetDataTable(
            $"SELECT * FROM PasswordInfo WHERE PwId = {_lngPKID}", "PasswordInfo");

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

            //if true is passed as args this will set the text fields to read only.
            SetTextReadOnly(true);
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
            //closes this details form instance
            this.Close();

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
                    SaveData();
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

        /// <summary>
        /// saves the data from the text fields to the data table.
        /// </summary>
        private void SaveData()
        {
            try
            {
                //writes the text box value and updates the data binding
                wsTextBox.DataBindings["Text"].WriteValue();
                emTextBox.DataBindings["Text"].WriteValue();
                adTextBox.DataBindings["Text"].WriteValue();
                pwTextBox.DataBindings["Text"].WriteValue();

            }catch(Exception e)
            {
                Logger.LogError("[PWManager.Details] [Save Data] Error writing values for data bindings " + e);
            }

            try
            {
                // display message to user to verify Insert Success
                MessageBox.Show(wsTextBox.Text + " Record Saved.");

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
            // Binding the text box with the data table '_dtbPassword'
            //  map it to the database entity called 'PwId'
            //      uses the 'Text' property of the control for binding.
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
