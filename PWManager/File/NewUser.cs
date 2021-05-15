using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Logging;
using PWManager.Security;
using PWManager_Model.DLL;

namespace PWManager.File
{
    public partial class NewUser : Form
    {
        private static string _UserName = "";
        private string _OriginalPW = "";
        private string _HashedPw = "";

        public NewUser()
        {
            InitializeComponent();
        }

        #region Form Events
        private void OpenLoginForm()
        {
            Dispose();
            Login frmload = new Login(_UserName, _OriginalPW);
            frmload.Show();
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
                // creates the users key for encrypting and decrypting the database
                KeyManager.CreateApplicationKey(_UserName);
                
                HashPassword();         // hashes the users password
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
            // checks if the user name text is empty
            if (!userNameTextBox.Text.Equals(""))
            {
                // checks if the user name is valid
                if (IsUserNameValid())
                {
                    // checks if the user name exists in the database
                    if (!PWManagerContext.IsUserExists(_UserName.ToLower()))
                    {
                        // checks if the password text is empty
                        if (!passwordTextBox.Text.Equals(""))
                        {
                            // checks if the password is valid
                            if (IsPasswordValid())
                            {
                                // checks if the confirmation password text is empty
                                if (!confirmPasswordTextBox.Text.Equals(""))
                                {
                                    // checks if the passwords match
                                    if (IsPasswordConfirmed())
                                    {
                                        return true;
                                    }
                                    else
                                    {
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
                                MessageBox.Show("Please enter at least 8 digits for password...");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password is required!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User already exists in database!");
                        OpenLoginForm();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter at least 8 digits for user name...");
                }
            }
            else
            {
                MessageBox.Show("User Name is required!");
            }
            return false;
        }

        /// <summary>
        /// Validates the new user's name
        /// </summary>
        /// <returns>true if the name is valid</returns>
        private bool IsUserNameValid()
        {
            int _MinNameLength = 8;

            // checks if the user name meets length requirements
            if (userNameTextBox.Text.Length >= _MinNameLength)
            {
                _UserName = userNameTextBox.Text;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates the new user's password
        /// </summary>
        /// <returns>true if the password is valid and matches</returns>
        private bool IsPasswordValid()
        {
            int _MinPwLength = 8;

            // checks if the password meets length requirements
            if (passwordTextBox.Text.Length >= _MinPwLength)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// confirms the users password matches the original
        /// </summary>
        /// <returns>true if the password matches</returns>
        private bool IsPasswordConfirmed()
        {
            // checks if the password matches the confirmation password
            if (confirmPasswordTextBox.Text.Equals(passwordTextBox.Text))
            {
                //sets the global password variable for hashing
                _OriginalPW = passwordTextBox.Text;
                return true;
            }
            return false;
        }
        #endregion

        #region Password Hashing
        /// <summary>
        /// hashes the users password and adds salt
        /// </summary>
        private void HashPassword()
        {
            // stores the users hashed password
            string strHashedPw = Hashing.Hash(_OriginalPW);

            //sets the global password variable with the hash
            _HashedPw = strHashedPw;
        }
        #endregion

        #region Database Helpers
        /// <summary>
        /// Creates the password info table for this user
        /// </summary>
        private void CreateUserPwTable()
        {
            PWManagerInitializer.CreateUserPasswordTables(_UserName.ToLower());
        }

        /// <summary>
        /// saves the users login details
        /// </summary>
        private void SaveLoginDetails()
        {
            // adds user to login table
            PWManagerInitializer.AddUserLogin(_UserName.ToLower(), _HashedPw);
        }
        #endregion
    }
}
