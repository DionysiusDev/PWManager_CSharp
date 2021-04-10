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

namespace PWManager.Options
{
    public partial class Create : Form
    {
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        bool _blnNew = false;
        private SqlConnection sqlConnectionTool;

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

        private void Create_Load(object sender, EventArgs e)
        {
            // Upon loading the form, establish the binding of the controls in the form.
            BindControls();
        }

        /// <summary>
        /// this method will show a new create form and close the current instance.
        /// </summary>
        private void RefreshForm()
        {
            Create frm = new Create();
            frm.Show();
            this.Close();
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
            //closes the create screen
            Close();
            //creates a new instance of the home screen
            Home.Home frmload = new Home.Home();
            //displays the home screen
            frmload.Show();

        }
        /// <summary>
        /// Handles user interaction with the file menu / logout button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            //closes the create screen
            this.Close();
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
            this.Close();
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
            Close();
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
            Close();
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
            Close();
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
                CheckExistingEntries();
            }
        }

        /// <summary>
        /// checks for existing entry in database.
        /// </summary>
        private void CheckExistingEntries()
        {
            bool doesntExist = false;

            //instantiates a connection string
            string connectionString = PWManager_Model.DLL.PWManagerContext.ConnectionString =
                Properties.Settings.Default.ConnectionString;

            try
            {
                //instantiates SqlConnectionTool which connects to the database via the connection string
                sqlConnectionTool = new SqlConnection(connectionString);

                //instantiates an sqlcommand to query the database
                SqlCommand cmd = new SqlCommand("SELECT * FROM PasswordInfo WHERE Website=@Website AND Email=@Email AND AdditionalInfo=@AdditionalInfo AND Password=@Password", sqlConnectionTool);

                //assigns values to the sqlcommand query
                cmd.Parameters.AddWithValue("@Website", wsTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", emTextBox.Text);
                cmd.Parameters.AddWithValue("@AdditionalInfo", adTextBox.Text);
                cmd.Parameters.AddWithValue("@Password", pwTextBox.Text);

                //Open connection
                sqlConnectionTool.Open();

                //instantiates an sql data adapter and adds the sql command
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                //instantiates a dataset
                DataSet ds = new DataSet();

                //fills the data set with the adapter / command values
                adapt.Fill(ds);

                //Close connection
                sqlConnectionTool.Close();

                //  This will count through the rows in the queried table
                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    //displays message to user to notify that the entry exists
                    MessageBox.Show("The website and password already exist in the database.", 
                        MessageBoxIcon.Warning.ToString(), MessageBoxButtons.OK);
                }
                else
                {
                    doesntExist = true;

                    if (doesntExist)
                    {
                        //saves the data table in the database
                        SaveData();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
            // display message to user to verify Insert Success
            MessageBox.Show(wsTextBox.Text + " Record Saved.");

            //always do the EndEdit, otherwise the data will not persist.
            _dtbPassword.Rows[0].EndEdit();

            // calls the method in our Data Access Layer to save the changes to the data table
            PWManager_Model.DLL.PWManagerContext.SaveDatabaseTable(_dtbPassword);

            RefreshForm();
        }

        /// <summary>
        /// This method will generate a strong 15 character random string for the password.
        /// </summary>
        private void GeneratePassword()
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
            //writes the text box value and updates the data binding
            pwTextBox.DataBindings["Text"].WriteValue();
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
        #endregion
    }
}
