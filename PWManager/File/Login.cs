using PWManager.Security;
using PWManager.SessionUser;
using PWManager_Model.DLL;
using System;
using System.Windows.Forms;

namespace PWManager
{
    public partial class Login : Form
    {
        string connectionString = PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;

        #region Constructor
        public Login()
        {
            InitializeComponent();

            passwordTextBox.UseSystemPasswordChar = true;

            userNameTextBox.Text = "DionysiusDev";
            passwordTextBox.Text = "1P2u3$3$4y5L6i7c8k9e0r";
        }

        public Login(string strUserName, string strPassword)
        {
            InitializeComponent();

            passwordTextBox.UseSystemPasswordChar = true;

            userNameTextBox.Text = strUserName;
            passwordTextBox.Text = strPassword;
        }
        #endregion

        #region Form Controls
        private void ShowBtn_Click_1(object sender, EventArgs e)
        {
            if (ShowBtn.Text == "Show")
            {
                passwordTextBox.UseSystemPasswordChar = false;
                ShowBtn.Text = "Hide";
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = true;
                ShowBtn.Text = "Show";
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                if (IsUserVerified())
                {
                    MessageBox.Show("Login Successful!");

                    Hide();                                 //  Hides the login window
                    Home.Home frmload = new Home.Home();    //  Loads the home form
                    frmload.Show();
                }
                else
                {
                    MessageBox.Show("Login Unsuccessful! Please Re-enter password...");

                    // clears the text fields
                    userNameTextBox.Clear();
                    passwordTextBox.Clear();
                }
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            Hide(); //  Hides the login window
            File.NewUser frmload = new File.NewUser();  //  Loads the new user form
            frmload.Show();
        }
        #endregion

        #region Form Events
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Data Validation
        /// <summary>
        /// validates data in text fields
        /// </summary>
        /// <returns>true if validated</returns>
        private bool IsDataValidated()
        {
            if (IsUserNameValidated())
            {
                if (IsPasswordValidated())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// validates data in user name text field
        /// </summary>
        /// <returns>true if validated</returns>
        private bool IsUserNameValidated()
        {
            if (!userNameTextBox.Text.Equals(""))
            {
                if (userNameTextBox.Text.Length >= 8)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Please enter at least 8 digits for user name...");
                }
            }
            else
            {
                MessageBox.Show("User name is required!");
            }
            return false;
        }

        /// <summary>
        /// validates data in password text field
        /// </summary>
        /// <returns>true if validated</returns>
        private bool IsPasswordValidated()
        {
            if (!passwordTextBox.Text.Equals(""))
            {
                if (passwordTextBox.Text.Length >= 8)
                {
                    return true;
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
            return false;
        }
        #endregion

        #region User Authentication
        /// <summary>
        /// verifies the user name and password
        /// </summary>
        /// <returns></returns>
        private bool IsUserVerified()
        {
            // gets the password for the user name
            string strHashedPassword = PWManagerContext.GetPassword(userNameTextBox.Text);

            // compares password hash and verifies that they match
            if(!Hashing.VerifyHash(passwordTextBox.Text, strHashedPassword))
            {
                // sets the current user for the session
                CurrentUser.SetUser(userNameTextBox.Text);
                return true;
            }
            return false;
        }
        #endregion
    }
}
