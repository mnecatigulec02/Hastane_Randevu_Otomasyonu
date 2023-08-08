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
    public partial class DoktorPaneli : Form
    {
        public DoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void DoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dtDoktorGetir = new DataTable();
            SqlDataAdapter daDoktorGetir = new SqlDataAdapter("select * from Tbl_Doktorlar;", bgl.baglanti());
            daDoktorGetir.Fill(dtDoktorGetir);
            dataGridView1.DataSource = dtDoktorGetir;

            SqlCommand komutBransGetir = new SqlCommand("select BransAd from Tbl_Branslar;", bgl.baglanti());
            SqlDataReader drBransGetir = komutBransGetir.ExecuteReader();
            while (drBransGetir.Read())
            {
                cmbBrans.Items.Add(drBransGetir[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komutDoktorEkle = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5);", bgl.baglanti());
            komutDoktorEkle.Parameters.AddWithValue("@d1", txtAd.Text);
            komutDoktorEkle.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komutDoktorEkle.Parameters.AddWithValue("@d3", cmbBrans.Text);
            komutDoktorEkle.Parameters.AddWithValue("@d4", mskTC.Text);
            komutDoktorEkle.Parameters.AddWithValue("@d5", txtSifre.Text);
            komutDoktorEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutDoktorSil = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@d1;",bgl.baglanti());
            komutDoktorSil.Parameters.AddWithValue("@d1", mskTC.Text);
            komutDoktorSil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Silindi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC=@d4;", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@d1", txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@d3", cmbBrans.Text);
            komutGuncelle.Parameters.AddWithValue("@d4", mskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@d5", txtSifre.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
