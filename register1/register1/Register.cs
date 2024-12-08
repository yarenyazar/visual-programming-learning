using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace register1
{
    public partial class Register : Form
    {
        public string dbName = "register.db";
        DBManagement dbManagement;
        public Register()
        {
            InitializeComponent();

            dbManagement = new DBManagement(dbName);
            dbManagement.CreateDatabase(dbName);
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            // userNameText.Text
            // passwordText.Text

            // Kullanıcı ekleme fonksiyonu bunları göndereceğim ve kullanıcılar tabloya eklenecek.
            dbManagement.addUser(userNameText.Text, passwordText.Text);
            // Kullanıcıyı bilgilendir.
            MessageBox.Show("Registration successful! You can log in now.");
            // Login formuna geri dön.
            Login loginForm = new Login();
            loginForm.Show();
            // Register formunu gizle.
            this.Hide();

        }

        private void Register_Load(object sender, EventArgs e)
        {
            dbManagement.CreateTable();
        }
    }
}
