using Logging;
using PWManager.DataValidation;
using PWManager.SessionUser;
using PWManager_Model.DLL;
using SecurityAccessLayer;
using System;
using System.Windows.Forms;

namespace PWManager
{
    public partial class Login : Form
    {
        #region Variable Declarations
        string connectionString = PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;
        CurrentUser _CurrentUser = new CurrentUser();
        private string _UserName = null;
        private string _UserPw = null;
        private int incorrectLoginCount = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the login form. This constructor will run when the application loads.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            passwordTextBox.UseSystemPasswordChar = true;

            userNameTextBox.Text = "DionysiusDev";
            passwordTextBox.Text = "1P2u3$3$4y5L6i7c8k9e0r";
        }

        /// <summary>
        /// Constructor for the login form. 
        /// This constructor will run when the login form is loaded by the create new account form.
        /// </summary>
        /// <param name="strUserName">user name</param>
        /// <param name="strPassword">password</param>
        public Login(string strUserName, string strPassword)
        {
            InitializeComponent();

            passwordTextBox.UseSystemPasswordChar = true;

            // sets the text fields text
            userNameTextBox.Text = strUserName;
            passwordTextBox.Text = strPassword;
        }
        #endregion

        #region Form Controls
        /// <summary>
        /// handles user interaction with the show button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowBtn_Click_1(object sender, EventArgs e)
        {
            if (ShowBtn.Text == "Show")
            {
                // shows the password in plain text
                passwordTextBox.UseSystemPasswordChar = false;
                ShowBtn.Text = "Hide";
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = true;
                ShowBtn.Text = "Show";
            }
        }

        /// <summary>
        /// handles user interaction with the login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBtn_Click(object sender, EventArgs e)
        {
            // set the global user name and passwords each time the login button is clicked
            _UserName = userNameTextBox.Text;
            _UserPw = passwordTextBox.Text;

            if (IsDataValidated()) 
            {
                if(incorrectLoginCount < 3) // checks the user hasn't failed logging in too many times
                {
                    if (IsUserVerified())
                    {
                        MessageBox.Show("Login Successful!");

                        Hide(); //  Hides the login window
                        Home.Home frmload = new Home.Home();    //  Loads the home form
                        frmload.Show();
                    }
                    else
                    {
                        incorrectLoginCount++;  // increment the incorrect login count
                        
                        _UserName = ""; // resets the global user name and password variables
                        _UserPw = "";

                        MessageBox.Show("Unrecognised user name or password!");
                    }
                }
                else
                {
                    // if the user fails login 4 times quit application, this will slow down an attacker minimally
                    MessageBox.Show("Too many failed login attempts!");
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// handles user interaction with the create account button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            Hide();
            File.NewUser frmload = new File.NewUser();
            frmload.Show();
        }
        #endregion

        #region Form Events
        /// <summary>
        /// prompts the user before quitting the application.
        /// Quits the application when the user clicks the forms close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // shutdowns the application if windows is shutting down
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // shutsdown the application when the user clicks the close button
            if (e.CloseReason == CloseReason.UserClosing && !IsDisposed)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Quit Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:           // Stays on this form
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:          // exits application
                        _CurrentUser.ResetUser();   // resets the user
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
            string strUserName = userNameTextBox.Text;
            string strPassword = passwordTextBox.Text;

            int intMinUserName = 6;
            int intMinPassword = 8;

            // checks that the user name field is not empty
            if (!ValidateData.IsEmpty(_UserName))
            {
                // checks that the user name field meets length requirements
                if(ValidateData.IsLengthValid(_UserName) >= intMinUserName)
                {
                    // checks that the password field is not empty
                    if (!ValidateData.IsEmpty(_UserPw))
                    {
                        // checks that the password field meets length requirements
                        if (ValidateData.IsLengthValid(_UserPw) >= intMinPassword)
                        {
                            return true;
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
                    MessageBox.Show($"Please enter at least {intMinUserName} digits for user name...");
                }
            }
            else
            {
                MessageBox.Show("User name is required!");
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
            string strHashedPassword = PWManagerContext.GetPassword(_UserName);
            // gets the salt for the user name
            string strStoredSalt = PWManagerContext.GetSalt(_UserName);

            // checks the hashed password is not empty before comparing passwords
            if (!string.IsNullOrEmpty(strHashedPassword) && !string.IsNullOrEmpty(strStoredSalt))
            {
                // compares password and stored password and verifies that they match
                if (SecurityAccessor.VerifyPassword(_UserPw, strHashedPassword, strStoredSalt))
                {
                    // sets the current user for the session
                    _CurrentUser.SetUser(_UserName);
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
