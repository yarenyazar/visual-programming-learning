using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film
{
    public class DbManagement
    {
        string dbFileName;
        string dbConString;
        private string query;
        private SQLiteConnection con;


        public DbManagement(string dbFileName)
        {
            this.dbFileName = dbFileName;
            dbConString = "Data Source = filmTakip.db";
            CreateTable();
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
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();

                    string query = "CREATE TABLE IF NOT EXISTS movies (filmID PK, filmName TEXT, category TEXT, point REAL, watchingDate DATE)";
                    string query2 = "CREATE TABLE IF NOT EXISTS Users (UserID INTEGER PRIMARY KEY AUTOINCREMENT, UserName TEXT NOT NULL, Password TEXT NOT NULL)";

                    // kullanıcıların kaydeceği filmler burada yer alacak
                    string query3 = "CREATE TABLE user_films (username TEXT NOT NULL,filmName TEXT NOT NULL) " +
                        "PRIMARY KEY (username, filmName) " +
                        "FOREIGN KEY (username) REFERENCES Users(username)" +
                        "FOREIGN KEY (filmName) REFERENCES movies(filmName)";



                    using (SQLiteCommand cmd_create = new SQLiteCommand(query, con))
                    {
                        cmd_create.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd_create2 = new SQLiteCommand(query2, con))
                    {
                        cmd_create2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd_create3 = new SQLiteCommand(query2, con))
                    {
                        cmd_create3.ExecuteNonQuery();
                    }

                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        public void CheckLogin(string username, string password)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();
                    string query = "SELECT * FROM Users WHERE UserName = @username AND Password = @password";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                               
                                GetsFilms getsFilms = new GetsFilms();
                                getsFilms.loginUser = username;
                                getsFilms.Show();

                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı bulunamadı");
                            }




                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }

        public void AddFilm(string filmName, string category, double point, DateTime watchingDate)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection())
                {
                    con.Open();
                    string query = "INSERT INTO movies (filmName, category, point, watchingDate) VALUES (@filmName, @category, @point, @watchingDate)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                       
                        cmd.Parameters.AddWithValue("@filmName", filmName);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@point", point);
                        cmd.Parameters.AddWithValue("@watchingDate", watchingDate);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable GetFilms(string filter = "")
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbConString))
                {
                    con.Open();
                    string query = "SELECT FilmName, Category, Point, WatchingDate FROM Movies WHERE UserID = @userID";

                    if (!string.IsNullOrEmpty(filter))
                    {
                        query += "AND (FilmName  LIKE @filter OR Category LIKE @filter)";
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                      
                        if (!string.IsNullOrEmpty(filter))
                        {
                            cmd.Parameters.AddWithValue("@filter", "%" + filter + "%");
                        }

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
                return null;
            }
        }

        public DataTable GetFilmStatics()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection())
                {
                    con.Open();
                    string query = "SELECT Category, COUNT(*) AS NumberOfMovies FROM Movies GROUP BY Category";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
                return null;
            }
        }            
    }
}
