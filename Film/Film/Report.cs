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
    public partial class ReportForm : Form
    {
        DbManagement db = new DbManagement("filmTakip.db");
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            
            DataTable data = db.GetFilmStatics();

            foreach (DataRow row in data.Rows)
            {
                chart1.Series["Category"].Points.AddXY(row["Category"], row["NumberOfMovies"]);
            }
        }
    }
}
