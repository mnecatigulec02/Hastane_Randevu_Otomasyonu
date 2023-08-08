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
    public partial class Duyurular : Form
    {
        public Duyurular()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void Duyurular_Load(object sender, EventArgs e)
        {
            DataTable dtDuyuruTablosu = new DataTable();
            SqlDataAdapter  daDuyuruAdapter = new SqlDataAdapter("select * from Tbl_Duyurular;",bgl.baglanti());
            daDuyuruAdapter.Fill(dtDuyuruTablosu);
            dataGridView1.DataSource = dtDuyuruTablosu;
            bgl.baglanti().Close();
        }
    }
}
