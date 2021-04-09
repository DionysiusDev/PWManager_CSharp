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
            InitializeComponent();
            _blnNew = true;
            InitializeDataTable();
        }

        public Create(long PKID)
        {
            InitializeComponent();
            _lngPKID = PKID;
            InitializeDataTable();
        }
        #endregion

        #region Form Events
        private void Create_Load(object sender, EventArgs e)
        {
            // Upon loading the form, establish the binding of the controls in the form.
            BindControls();
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
            $"SELECT * FROM PasswordInfo WHERE PwId = {_lngPKID}", "Website");

            // Create an empty row of password info
            if (_blnNew)
            {
                DataRow row = _dtbPassword.NewRow();
                _dtbPassword.Rows.Add(row);
            }
        }
        #endregion

        #region File
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the create screen
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
        private void enterExistingPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the create screen
            this.Close();
            //creates a new instance of the existing screen from the options menu
            Options.Existing frmload = new Options.Existing();
            //displays the existing screen
            frmload.Show();
        }
        private void viewAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Options.ViewAll frmload = new Options.ViewAll();
            frmload.Show();
        }
        #endregion

        private void pwBtn_Click(object sender, EventArgs e)
        {
            //calls the generate password method
            generatePassword();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //TODO SAVE DATA
            saveData();
        }

        public void saveData()
        {
            //Instantiates a string array to store the password data array values
            string[] datatoSave = getPasswordData();

            //instantiates a connection string
            string connectionString = PWManager_Model.DLL.PWManagerContext.ConnectionString =
                Properties.Settings.Default.ConnectionString;

            try
            {
                //instantiates SqlConnectionTool which connects to the database via the connection string
                sqlConnectionTool = new SqlConnection(connectionString);

                //instantiates an sqlcommand to query the database
                SqlCommand cmd = new SqlCommand("Select * from PasswordInfo where Website=@Website" +
                    " AND Email=@Email " +
                    "AND AdditionalInfo=@AdditionalInfo " +
                    "AND Password=@Password", sqlConnectionTool);

                //assigns values to the sqlcommand query
                cmd.Parameters.AddWithValue("@Website", wsTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", emTextBox.Text);
                cmd.Parameters.AddWithValue("@AdditionalInfo", adTextBox.Text);
                cmd.Parameters.AddWithValue("@Password", pwTextBox.Text);

                //cmd.Parameters.AddWithValue("@Website", datatoSave[0]);
                //cmd.Parameters.AddWithValue("@Email", datatoSave[1]);
                //cmd.Parameters.AddWithValue("@AdditionalInfo", datatoSave[2]);
                //cmd.Parameters.AddWithValue("@Password", datatoSave[3]);

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

                //  Try this:
                //  Validate product name, ID, and/or receipt number don't exist
                try
                {
                    if (count == 1)
                    {
                        //  This should display a receipt exists message
                        MessageBox.Show("Insert Website and Password Failed - [" + wsTextBox.Text + "] -and- [" + pwTextBox.Text +
                            "] - Already exists in the database", MessageBoxIcon.Warning.ToString(), MessageBoxButtons.OK);
                    }
                    else
                    {
                        // display message to user to verify Insert Success
                        MessageBox.Show(wsTextBox.Text + " Record Saved. " + DateTime.Now);

                        //always do the EndEdit, otherwise the data will not persist.
                        _dtbPassword.Rows[0].EndEdit();

                        // calls the method in our Data Access Layer to save the changes to the data table
                        PWManager_Model.DLL.PWManagerContext.SaveDatabaseTable(_dtbPassword);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// This method will generate a strong 15 character random string for the password.
        /// </summary>
        public void generatePassword()
        {
            //declares and assigns values to multiple strings of different characters
            string lower = "abcdefghjkmnpqrstuvwxyz";
            string upper = lower.ToUpper();
            string chars = "!@#$%^&*<>?:`~";
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
        }

        /// <summary>
        /// this method gets all text field data and returns an array.
        /// </summary>
        /// <returns>array</returns>
        public string[] getPasswordData()
        {
            //string array used to return text field values
            string[] data = {"","","",""};

            #region Website
            //checks if the website text field is not empty
            if (!isEmpty(wsTextBox.Text.ToString()))
            {
                //adds the website text to the data array
                data[0] = wsTextBox.Text.ToString();
            }
            else
            {
                //displays a message box to the user
                MessageBox.Show("Please enter the website the Password is for.");
            }
            #endregion

            #region Email
            //checks if the email text field is not empty
            if (!isEmpty(emTextBox.Text.ToString()))
            {
                //adds the email text to the data array
                data[1] = emTextBox.Text.ToString();
            }
            else
            {
                //displays a message box to the user
                MessageBox.Show("Please emter the email the Password is for.");
            }
            #endregion

            #region Additional info
            //checks if the additional text field is not empty
            if (!isEmpty(adTextBox.Text.ToString()))
            {
                //adds the additional info text to the data array
                data[2] = adTextBox.Text.ToString();
            }
            else
            {
                //displays a message box to the user
                MessageBox.Show("You are about to save no additional information.");
                //adds no additional info text to the data array
                data[4] = "No Additional Information";
            }
            #endregion

            #region Password
            //checks if the password text field is not empty
            if (!isEmpty(pwTextBox.Text.ToString()))
            {
                //adds password text to the data array
                data[3] = pwTextBox.Text.ToString();
            }
            #endregion

            return data;
        }

        /// <summary>
        /// This method checks if a string is empty
        /// </summary>
        /// <param name="data">the string to check</param>
        /// <returns>bool</returns>
        public bool isEmpty(string data)
        {
            if (data.Length < 1)
                return true;

            return false;
        }

        #region Helper Methods
        /// <summary>
        /// This method will bind the controls to each field in the data table.
        /// </summary>
        private void BindControls()
        {
            // Binding the text box txtUpdateToolId with the data table '_dtbTool' and
            // map it to the database field called 'ToolId' and use the
            // 'Text' property of the control for binding.
            wsTextBox.DataBindings.Add("Text", _dtbPassword, "Website");
            emTextBox.DataBindings.Add("Text", _dtbPassword, "Email");
            adTextBox.DataBindings.Add("Text", _dtbPassword, "AdditionalInfo");
            pwTextBox.DataBindings.Add("Text", _dtbPassword, "Password");
        }
        #endregion
    }
}
