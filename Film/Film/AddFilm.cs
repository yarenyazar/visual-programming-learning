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
    public partial class AddFilm : Form
    {
        public string user = "";
        DbManagement db = new DbManagement("filmTakip.db");
        public AddFilm()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            string filmName = txtFilmAdi.Text;
            string category = comboBox.SelectedItem.ToString();
            double point = double.Parse(txtPuan.Text);
            DateTime watchingDate = dtIzlenmeTarihi.Value;

            db.AddFilm(filmName, category, point, watchingDate);
            MessageBox.Show("Movie inserted successfully!");
        }

       
    }
}
