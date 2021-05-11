using PWManager_Model.DLL;
using System;
using System.Windows.Forms;

namespace PWManager
{
    public partial class Login : Form
    {
        string connectionString = PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;

        public Login()
        {
            InitializeComponent();

            userNameTextBox.Text = "DionysiusDev";
            passwordTextBox.Text = "1P2u3$3$4y5L6i7c8k9e0r";
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
    }
}
