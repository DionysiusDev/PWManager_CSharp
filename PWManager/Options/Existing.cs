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
    public partial class Existing : Form
    {
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        bool _blnNew = false;
        private SqlConnection sqlConnectionTool;

        #region Constructors
        public Existing()
        {
            _blnNew = true;
            InitializeComponent();
            InitializeDataTable();
        }

        public Existing(long PKID)
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

        private void Existing_Load(object sender, EventArgs e)
        {
            // Upon loading the form, establish the binding of the controls in the form.
            BindControls();
        }

        /// <summary>
        /// this method will show a new existing form and close the current instance.
        /// </summary>
        private void RefreshForm()
        {
            Existing frm = new Existing();
            frm.Show();
            this.Close();
        }

        #endregion

        #region File
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the existing screen
            this.Close();
            //creates a new instance of the home screen
            Home.Home frmload = new Home.Home();
            //displays the home screen
            frmload.Show();
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login frm = new Login();
            frm.ShowDialog();
        }

        #endregion

        #region Options
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the existing screen
            this.Close();
            //creates a new instance of the create screen from the options menu
            Options.Create frmload = new Options.Create();
            //displays the create screen
            frmload.Show();
        }

        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.ViewAll frmload = new Options.ViewAll();
            frmload.Show();
        }
        #endregion

        #region Action Events
        private void saveBtn_Click(object sender, EventArgs e)
        {
            ValidateData();
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

            //if all text fields contain text check for existing entries in the database
            if(hasWebsite && hasEmail && hasPassword && hasAdditional)
            {
                CheckExistingEntries();
            }
        }

        /// <summary>
        /// checks for existing entry in database.
        /// </summary>
        private void CheckExistingEntries()
        {
            MessageBox.Show("CheckExistingEntries()");

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
                    MessageBox.Show("The website and password already exists in the database.",
                        MessageBoxIcon.Warning.ToString(), MessageBoxButtons.OK);
                }
                else
                {

                    //saves the data table in the database
                    SaveData();
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
            MessageBox.Show("SaveData()");

            // display message to user to verify Insert Success
            MessageBox.Show(wsTextBox.Text + " Record Saved.");

            //always do the EndEdit, otherwise the data will not persist.
            _dtbPassword.Rows[0].EndEdit();

            // calls the method in our Data Access Layer to save the changes to the data table
            PWManager_Model.DLL.PWManagerContext.SaveDatabaseTable(_dtbPassword);

            RefreshForm();
        }

        #region Helper Methods
        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            // Binding the text box with the data table '_dtbPassword'
            //  map it to the database entity
            //      uses the 'Text' property of the control for binding.
            wsTextBox.DataBindings.Add("Text", _dtbPassword, "Website");
            emTextBox.DataBindings.Add("Text", _dtbPassword, "Email");
            adTextBox.DataBindings.Add("Text", _dtbPassword, "AdditionalInfo");
            pwTextBox.DataBindings.Add("Text", _dtbPassword, "Password");
        }
        #endregion
    }
}
