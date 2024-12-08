using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film
{
    public partial class Login : Form
    {
        DbManagement dbManagement;
        string dbName = "filmTakip.db";
        public string user = "";
        public Login()
        {
            InitializeComponent();

            dbManagement = new DbManagement(dbName);
            dbManagement.CreateDatabase(dbName);
       

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            string username = usernameText.Text;
            string password = passwordText.Text;
            dbManagement.CheckLogin(username, password);
        }

        
    }
}
