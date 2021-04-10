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

namespace PWManager
{
    public partial class Login : Form
    {
        //declaration of sql connection for login form
        private SqlConnection loginConnection;

        //declaration of sql transaction for login form
        private SqlTransaction loginTransaction;

        string connectionString = PWManager_Model.DLL.PWManagerContext.ConnectionString = Properties.Settings.Default.ConnectionString;

        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Please enter User Name and Password!");
                return;
            }
            try
            {
                //  Create SqlConnection
                loginConnection = new SqlConnection(connectionString);

                //  SqlCommand is used to hold the query linked to our connection
                SqlCommand loginCommand = new SqlCommand("Select * from Login where " +
                                                         "UserName=@UserName and" +
                                                         " Password=@Password",
                                                         loginConnection);

                //  Declaration of required parameters for verifying user login
                loginCommand.Parameters.AddWithValue("@UserName", userNameTextBox.Text);
                loginCommand.Parameters.AddWithValue("@Password", passwordTextBox.Text);

                //  This is where we open our connection
                loginConnection.Open();

                //  we start our transaction after immediately after we open our connection
                loginTransaction = loginConnection.BeginTransaction();

                loginCommand.Transaction = loginTransaction;    //  here we link our transaction to our command

                loginCommand.ExecuteNonQuery(); //  Query must be executed in order to process the roll back

                /*  ---------------------------------------------------------
                 *  the data adapter is used to specify SQL commands that,
                 *  provides elementary CRUD functionality,
                 *  Create, Update, Retrieve(Read), and Delete.
                 *  
                 *  DataAdapter functions as a bridge between a data source,
                 *  and a disconnected data class, such as a DataSet -->
                 *  ---------------------------------------------------------
                */
                SqlDataAdapter dataAdapter = new SqlDataAdapter(loginCommand);


                DataSet loginDataSet = new DataSet(); //  dataset refers to data selected and arranged in rows and columns

                dataAdapter.Fill(loginDataSet);       //  Here we use the data adapter to populate (Fill) the data set with data :-)

                /*  ---------------------------------------------
                 *  int is the variable or data type,
                 *  we will set int count to the first index of,
                 *  the table in the data set.
                 *  ---------------------------------------------
                 */
                int count = loginDataSet.Tables[0].Rows.Count;        //  int is the variable or data type

                if (count == 1)                             //  If count is equal to 1, than show frmMain form
                {
                    MessageBox.Show("Login Successful!");

                    loginTransaction.Commit();          //  If we are happy with the results commit the transaction

                    this.Hide();                                   //  Hide our login window
                    Home.Home frmload = new Home.Home();           //  Load our main menu form
                    frmload.Show();                               //  Show us the form on screen
                }
                // else do this-->
                else
                {
                    //  used to display a warning about login attempt error
                    MessageBox.Show("Unrecognised User Name and or Password.../nPlease try again..");

                    userNameTextBox.Clear();    //  this is here to clear the fields,
                    passwordTextBox.Clear();    //  after an unsuccessful login attempt

                }
            }
            /*  --------------------------------------------------------------
             *  its is important to always catch exceptions
             *  this exception has been named login Exception to,
             *  so we know if there is any errors caught during login attempt.
             *  ---------------------------------------------------------------
            */
            catch (Exception loginException)
            {
                //  we can create a custom error message to catch our exception like so
                MessageBox.Show(loginException.Message, "Exception Caught during login process", MessageBoxButtons.OK, MessageBoxIcon.Error);

                /*  -------------------------------------------------------------
                 *  This is where we create our rollback transaction procedure.
                 *  If this 'catch' catches any inconsistency in the transaction, 
                 *  it will perform a transaction roll back on the most recent,
                 *  performed task.
                 *  
                 *  This is safe practice to help maintain database integrity. 
                 *  --------------------------------------------------------------
                 */

                // Try Do This -->
                try
                {
                    //  here the login transaction attempts to roll back
                    loginTransaction.Rollback();


                    //  on successful roll back display confirmation of transaction rollback message
                    MessageBox.Show("Rollback Confirmed..");
                }

                //  again 'Catch' any inconsistencies - Exceptions
                catch (Exception LoginRollbackException)
                {
                    //  display the error message
                    MessageBox.Show(LoginRollbackException.Message, "Transaction Failed To Rolled Back..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                loginConnection.Close();    //  Once the query is execute and the data has been retrieved we Close the connection 
                //  Throw this:
                throw;

            }

        }
    }
}
