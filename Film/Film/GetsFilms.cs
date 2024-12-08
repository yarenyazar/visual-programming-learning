using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film
{
    
    public partial class GetsFilms : Form
    {
        DbManagement db = new DbManagement("filmTakip.db");
        public string loginUser = "";

        public GetsFilms()
        {
            InitializeComponent();
        }

        private void GetsFilms_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.GetFilms(1);
            helloText.Text += loginUser;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
           
            string filter = txtArama.Text;
            dataGridView1.DataSource = db.GetFilms(1, filter);
        }

        private void add_newFılmClick(object sender, EventArgs e)
        {
            AddFilm addfilm = new AddFilm();
            this.Hide();
            addfilm.Show();

        }
    }
}
