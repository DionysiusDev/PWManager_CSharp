using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using PWManager_Model.DLL;
using Logging;
using PWManager.SessionUser;
using SecurityAccessLayer;

namespace PWManager.Options
{
    public partial class Create : Form
    {
        #region Variable Declarations
        CurrentUser _CurrentUser = new CurrentUser();
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        bool _blnNew = false;
        string strOriginalWs = "";
        static string _TableName = "";
        #endregion

        #region Constructors
        public Create()
        {
            _blnNew = true;
            InitializeComponent();
            InitializeDataTable();
        }

        public Create(long PKID)
        {
            InitializeComponent();
            _lngPKID = PKID;
            InitializeDataTable();
        }
        #endregion

        #region Accessors
        /// <summary>
        /// This method will initialize the data table by getting the record of an existing password or,
        /// create a new row when adding a new password.
        /// </summary>
        private void InitializeDataTable()
        {
            string strCurrentUser = CurrentUser._UserName;
            _TableName = $"{strCurrentUser}Passwords";

            // Get an existing password for Update
            _dtbPassword = PWManagerContext.GetDataTable($"SELECT * FROM {_TableName} WHERE PwId = {_lngPKID}", _TableName);

            // Create an empty row of password info
            if (_blnNew)
            {
                DataRow row = _dtbPassword.NewRow();
                _dtbPassword.Rows.Add(row);
            }
        }
        #endregion

        #region Form Events

        private void Create_Load(object sender, EventArgs e)
        {
            // Upon loading the form, establish the binding of the controls in the form.
            BindControls();
        }

        private void Create_FormClosing(object sender, FormClosingEventArgs e)
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
                        _CurrentUser.ResetUser();
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
        /// Handles user interaction with the password button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwBtn_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }
        /// <summary>
        /// Handles user interaction with the save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            ValidateData();
        }

        #region File Menu
        /// <summary>
        /// Handles user interaction with the file menu / home button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // hides the create screen
            Dispose();
            // creates a new instance of the home screen
            Home.Home frmload = new Home.Home();
            // displays the home screen
            frmload.Show();

        }
        /// <summary>
        /// Handles user interaction with the file menu / logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _CurrentUser.ResetUser();

            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }
        #endregion

        #region Options Menu
        /// <summary>
        /// Handles user interaction with the options menu / enter existing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // disposes the create screen
            Dispose();
            //creates a new instance of the existing screen from the options menu
            Options.Existing frmload = new Options.Existing();
            //displays the existing screen
            frmload.Show();
        }
        /// <summary>
        /// Handles user interaction with the options menu / view all button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Options.ViewAll frmload = new Options.ViewAll();
            frmload.Show();
        }
        #endregion

        #region Help Menu
        /// <summary>
        /// Handles user interaction with the help menu / create new button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewPasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
            Help.HelpCreate frm = new Help.HelpCreate();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles user interaction with the help menu / enter existing button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterExistingPasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
            Help.HelpExisting frm = new Help.HelpExisting();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles user interaction with the help menu / about button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Help.HelpAbout frm = new Help.HelpAbout();
            frm.ShowDialog();
        }
        #endregion

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
                    MessageBox.Show("Please generate a password.");
                }
                else
                {
                    hasPassword = true;
                }

                //updates the additional info text field, if it is empty
                if (isEmpty(adTextBox.Text))
                {
                    adTextBox.Text = "No Additional Information";
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
                    CheckExistingEntries();
                }
            } catch (Exception e)
            {
                Logger.LogError("[PWManager.Create] [Validate Data] Error validating data " + e);
            }
        }

        /// <summary>
        /// checks for existing entry in database.
        /// </summary>
        private void CheckExistingEntries()
        {
            //instantiates a connection string
            string connectionString = PWManagerContext.ConnectionString =
                Properties.Settings.Default.ConnectionString;

            if (!PWManagerContext.IsEntryExists(_TableName, wsTextBox.Text, pwTextBox.Text))
            {
                // checks if all data is encrypted before saving
                if (EncryptData())
                {
                    //saves the data table in the database
                    SaveData();
                }
            }
            else
            {
                //displays message to user to notify that the entry exists
                MessageBox.Show("The website and password already exist in the database.",
                    MessageBoxIcon.Warning.ToString(), MessageBoxButtons.OK);
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

        #region Data Encryption

        /// <summary>
        /// this method encrypts data prior to saving in the database
        /// </summary>
        /// <returns></returns>
        private bool EncryptData()
        {            
            // stores website before encrypting
            strOriginalWs = wsTextBox.Text;

            // encrypts the data
            string strEncryptedWs = SecurityAccessor.SimpleEncrypt(wsTextBox.Text);
            string strEncryptedEm = SecurityAccessor.SimpleEncrypt(emTextBox.Text);
            string strEncryptedAd = SecurityAccessor.SimpleEncrypt(adTextBox.Text);
            string strEncryptedPw = SecurityAccessor.SimpleEncrypt(pwTextBox.Text);

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
        /// this method resets the form controls and re-binds to the data table after it has been updated.
        /// </summary>
        private void Reset()
        {
            ClearBindings();        // clears the bindings
            _dtbPassword = null;    // sets the data table to null
            InitializeDataTable();  // gets the data table after it has been edited
            BindControls();         // re-binds the controls
        }

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
                PWManagerContext.SaveDatabaseTable(_dtbPassword);

                Reset();

            } catch (Exception e)
            {
                Logger.LogError("[PWManager.Create] [Save Data] Error saving data..." + e);
            }
        }

        /// <summary>
        /// This method will generate a strong 15 character random string for the password.
        /// </summary>
        private void GeneratePassword()
        {
            try
            {
                //declares and assigns values to multiple strings of different characters
                string lower = "abcdefghjkmnpqrstuvwxyz";
                string upper = lower.ToUpper();
                string chars = "!@#$%^&*?`~";
                string nums = "123456789";

                //instantiates a new random for selecting random characters
                Random randChar = new Random();

                //instantiates a new string to store the password value and formats the password
                string PW = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                    chars.ElementAt(randChar.Next(chars.Length)),
                    upper.ElementAt(randChar.Next(upper.Length)),
                    nums.ElementAt(randChar.Next(nums.Length)),
                    lower.ElementAt(randChar.Next(lower.Length)),
                    nums.ElementAt(randChar.Next(nums.Length)),
                    upper.ElementAt(randChar.Next(upper.Length)),
                    chars.ElementAt(randChar.Next(chars.Length)),
                    lower.ElementAt(randChar.Next(lower.Length)),
                    chars.ElementAt(randChar.Next(chars.Length)),
                    upper.ElementAt(randChar.Next(upper.Length)),
                    nums.ElementAt(randChar.Next(nums.Length)),
                    lower.ElementAt(randChar.Next(lower.Length)),
                    nums.ElementAt(randChar.Next(nums.Length)),
                    upper.ElementAt(randChar.Next(upper.Length)),
                    chars.ElementAt(randChar.Next(chars.Length)),
                    lower.ElementAt(randChar.Next(lower.Length)));

                //assigns the password value to the pw text field
                pwTextBox.Text = PW;

            } catch(Exception e)
            {
                Logger.LogError("[PWManager.Create] [Generate Password] Error generating password " + e);
            }
            
        }

        #region Helper Methods
        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            // Binds text boxes with the data table '_dtbPassword'
            //  maps each text box to their related database entity e.g. 'Website'
            //      uses the 'Text' property of the control for binding.
            wsTextBox.DataBindings.Add("Text", _dtbPassword, "Website");
            emTextBox.DataBindings.Add("Text", _dtbPassword, "Email");
            adTextBox.DataBindings.Add("Text", _dtbPassword, "AdditionalInfo");
            pwTextBox.DataBindings.Add("Text", _dtbPassword, "Password");
        }

        /// <summary>
        /// this method clears bindings
        /// </summary>
        private void ClearBindings()
        {
            wsTextBox.DataBindings.Clear();
            emTextBox.DataBindings.Clear();
            adTextBox.DataBindings.Clear();
            pwTextBox.DataBindings.Clear();
        }
        #endregion
    }
}
