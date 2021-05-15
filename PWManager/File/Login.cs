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
            if (userNameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Please enter User Name and Password!");
                return;
            }

            if (!PWManagerContext.IsLoginVerified(userNameTextBox.Text, passwordTextBox.Text))
            {
                // displays message to user
                MessageBox.Show("Unrecognised User Name and or Password...");

                // clears the text fields
                userNameTextBox.Clear();
                passwordTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Login Successful!");

                Hide();                                 //  Hides the login window
                Home.Home frmload = new Home.Home();    //  Loads the home form
                frmload.Show();                         //  Displays the home form
            }
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
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
