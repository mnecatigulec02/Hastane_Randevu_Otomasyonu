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
    public partial class DoktorDetay : Form
    {
        public DoktorDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string TCno;
        private void DoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCno;

            //doktor adsoyad çekme
            SqlCommand komutAdSoyadGetir = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTC='"+lblTC.Text+"'",bgl.baglanti());
            SqlDataReader drAdSoyadGetir = komutAdSoyadGetir.ExecuteReader();
            while (drAdSoyadGetir.Read())
            {
                lblAdSoyad.Text = drAdSoyadGetir[0] + " " + drAdSoyadGetir[1];
            }
            bgl.baglanti().Close();

            //Doktora ait randevu listesini çekme
            DataTable dtRandevuTablosu = new DataTable();
            SqlDataAdapter daRandevuAdeptar = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuDoktor='" + lblAdSoyad.Text + "'", bgl.baglanti());
            daRandevuAdeptar.Fill(dtRandevuTablosu);
            dataGridView1.DataSource = dtRandevuTablosu;
            bgl.baglanti().Close();
        }
        
        private void btnBilgiDüzenle_Click(object sender, EventArgs e)
        {
            DoktorBilgiDuzenle drBilgiDuzenle = new DoktorBilgiDuzenle();
            drBilgiDuzenle.tcNo = lblTC.Text;
            drBilgiDuzenle.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            Duyurular dyr = new Duyurular();
            dyr.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
