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

namespace PWManager.Details
{
    public partial class Details : Form
    {
        private long _lngPKID = 0;
        private DataTable _dtbPassword = null;
        bool _blnNew = false;
        private SqlConnection sqlConnectionTool;

        #region Constructors
        public Details()
        {
            InitializeComponent();
        }
    
        public Details(long PKID)
        {
            InitializeComponent();
            _lngPKID = PKID;
        }
        #endregion

        private void Details_Load(object sender, EventArgs e)
        {

        }
    }
}
