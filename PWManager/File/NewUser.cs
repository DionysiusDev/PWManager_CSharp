using System;
using System.Windows.Forms;
using PWManager.DataValidation;
using PWManager.SessionUser;
using PWManager_Model.DLL;
using SecurityAccessLayer;

namespace PWManager.File
{
    public partial class NewUser : Form
    {
        #region Variable Declarations
        CurrentUser _CurrentUser = new CurrentUser();
        private string _UserName = null;
        private string _OriginalPW = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the new user form. This constructor will run when to form loads.
        /// </summary>
        public NewUser()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        /// <summary>
        /// Creates a new instance and displays the login form.
        /// </summary>
        private void OpenLoginForm()
        {
            Dispose();
            // passes the arguments for the login constructor
            Login frmload = new Login(_UserName, _OriginalPW);
            frmload.Show();

            _UserName = "";
            _OriginalPW = "";
        }

        /// <summary>
        /// prompts the user before quitting the application.
        /// Quits the application when the user clicks the forms close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing && !IsDisposed)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:           // Stay on this form
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:          // exit application
                        _CurrentUser.ResetUser();   // resets the user
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Form Controls
        /// <summary>
        /// handles user interaction with the create account button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                // Creates the user file in the application directory and writes key
                FileHandling.SetupUserFile(_UserName);

                SaveLoginDetails();     // saves the users login details
                CreateUserPwTable();    // creates the users password table

                MessageBox.Show("Account Created!");

                OpenLoginForm();
            }
        }
        #endregion

        #region Data Validation
        /// <summary>
        /// Validates user name and password
        /// </summary>
        /// <returns>true if all data is validated</returns>
        private bool IsDataValidated()
        {
            int intMinUserName = 6;
            int intMinPassword = 8;

            // sets the global username and password variables
            _UserName = userNameTextBox.Text;
            _OriginalPW = passwordTextBox.Text;

            // checks if the user name text is empty
            if (!ValidateData.IsEmpty(_UserName))
            {
                // checks if the user name meets length requirements
                if (ValidateData.IsLengthValid(_UserName) >= intMinUserName)
                {
                    // checks if the user name exists in the database
                    if (!PWManagerContext.IsUserExists(_UserName))
                    {
                        // checks if the password text is empty
                        if (!ValidateData.IsEmpty(_OriginalPW))
                        {
                            // checks if the password meets length requirements
                            if (ValidateData.IsLengthValid(_OriginalPW) >= intMinPassword)
                            {
                                // checks if the confirmation password text is empty
                                if (!ValidateData.IsEmpty(confirmPasswordTextBox.Text))
                                {
                                    // checks if the passwords match
                                    if (ValidateData.IsEqual(_OriginalPW, confirmPasswordTextBox.Text))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        _OriginalPW = "";
                                        MessageBox.Show("Passwords do not match...");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter confirmation password!");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Please enter at least {intMinPassword} digits for password...");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password is required!");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"User name {userNameTextBox.Text} already exists in database!");
                        _UserName = userNameTextBox.Text;
                        OpenLoginForm();
                    }
                }
                else
                {
                    MessageBox.Show($"Please enter at least {intMinUserName} digits for user name!");
                }
            }
            else
            {
                MessageBox.Show("User Name is required!");
            }

            return false;
        }
        #endregion

        #region Database Helpers
        /// <summary>
        /// Creates the password info table for this user
        /// </summary>
        private void CreateUserPwTable()
        {
            PWManagerInitializer.CreateUserPasswordTables(_UserName);
        }

        /// <summary>
        /// saves the users login details
        /// </summary>
        private void SaveLoginDetails()
        {
            PWManagerInitializer.AddUserLogin(_UserName, SecurityAccessor.GenerateHash(_OriginalPW), SecurityAccessor.GetSalt());
        }
        #endregion
    }
}
