using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Randevu_Otomasyonu
{
    public partial class RandevuListesi : Form
    {
        public RandevuListesi()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void RandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable dtRandevuListesi = new DataTable();
            SqlDataAdapter daRandevuListesi = new SqlDataAdapter("select * from Tbl_Randevular;",bgl.baglanti());
            daRandevuListesi.Fill(dtRandevuListesi);
            dataGridView1.DataSource = dtRandevuListesi;
            bgl.baglanti().Close();
          
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
