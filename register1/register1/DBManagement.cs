using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace register1
{
    public class DBManagement
    {
        // Tüm database işlemlerini buraya yapıyoruz. (Veri ekleme, veri sorgulama, veri çıkarma, veri güncelleme, database oluşturma)
        // Bunların hepsi ayrı metotlar içerisinde olacak.

        string dbFileName; // Database ismi için bu class bir obje yardımıyla çağrıldığında çalışacak.
        string dbConString; // Yukarıdaki dbFileName'i kullanarak bir database kaynağına bağlanacak.
        private string query;
        private SQLiteConnection con;

        public DBManagement(string dbFileName)
        {
            this.dbFileName = dbFileName;
            dbConString = "Data Source =" + dbFileName;
        }

        public void CreateDatabase(string dbFileName)
        {
            if (System.IO.File.Exists(dbFileName))
            {

            }
            else
            {
                try
                {
                    using (SQLiteConnection con = new SQLiteConnection("Data Source =" + dbFileName))
                    {
                        con.Open();
                        MessageBox.Show("Database created!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CreateTable()
        {
            // Tablo oluşturacağız.
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();

                    string query = "CREATE TABLE IF NOT EXISTS register_table (username TEXT, password TEXT)";

                    using (SQLiteCommand cmd_create = new SQLiteCommand(query, con))
                    {
                        cmd_create.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void addUser(string username, string password)
        {
            try
            {
                // Bağlantı adresine gidildi ve bağlantı açıldı
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();
                    string query = @"INSERT INTO register_table (username, password) VALUES (@name, @pass)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        // Parametreler yukarıdan gönderilenler ile eşlendi
                        cmd.Parameters.AddWithValue("@name", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data inserted!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public (bool, string) LoginUserAndGetPassword(string username, string password)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();

                    string query = @"SELECT password FROM register_table WHERE username = @name AND password = @pass";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Kullanıcı bulundu, şifreyi döndür
                            return (true, result.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return (false, null); // Kullanıcı bulunamadı
        }
    }
}
