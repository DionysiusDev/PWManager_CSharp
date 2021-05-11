﻿using PWManager_Model.DLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Logging;

namespace PWManager.Options
{
    public partial class Existing : Form
    {
        #region Variable Declarations
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        bool _blnNew = false;
        #endregion

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

        #region Control Events
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
            //closes the existing screen
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
        /// Handles user interaction with the options menu / create new button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes the existing screen
            this.Close();
            //creates a new instance of the create screen from the options menu
            Options.Create frmload = new Options.Create();
            //displays the create screen
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
                if (hasWebsite && hasEmail && hasPassword && hasAdditional)
                {
                    CheckExistingEntries();
                }
            }
            catch(Exception e)
            {
                Logger.LogError("[PWManager.Existing] [Validate Data] Error validating data " + e);
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

            if (!PWManagerContext.IsEntryExists(wsTextBox.Text, pwTextBox.Text))
            {
                //saves the data table in the database
                SaveData();
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

        /// <summary>
        /// saves the data from the text fields to the data table.
        /// </summary>
        private void SaveData()
        {
            try
            {
                // display message to user to verify Insert Success
                MessageBox.Show(wsTextBox.Text + " Record Saved.");

                //always do the EndEdit, otherwise the data will not persist.
                _dtbPassword.Rows[0].EndEdit();

                // calls the method in our Data Access Layer to save the changes to the data table
                PWManagerContext.SaveDatabaseTable(_dtbPassword);

                RefreshForm();
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager.Existing] [Save Data] Error Saving Data " + e);
            }
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
