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
    public partial class Login : Form
    {
        DBManagement dbManagement;
        public Login()
        {
            InitializeComponent();

            // Veritabanı yönetim sınıfını başlatıyoruz.
            string dbName = "register.db";
            dbManagement = new DBManagement(dbName);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameText1.Text;
            string password = passwordText1.Text;

            // Kullanıcı kontrolü ve şifre alma
            var (isSuccess, actualPassword) = dbManagement.LoginUserAndGetPassword(username, password);

            if (isSuccess)
            {
                MessageBox.Show("Login successful!");

                // MainForm'u aç ve kullanıcı bilgilerini gönder
                MainForm mainForm = new MainForm(username, actualPassword);
                mainForm.Show();

                // Login formunu gizle
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed! Username or password is incorrect.");
            }
        }

        private void ToRegister_Click_1(object sender, EventArgs e)
        {
            // Register formunu aç
            Register registerForm = new Register();
            registerForm.Show();

            // Login formunu gizle
            this.Hide();
        }
    }
}

