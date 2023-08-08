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
    public partial class HastaDetay : Form
    {
        public HastaDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;
        private void HastaDetay_Load(object sender, EventArgs e)
        {
            //AD Soyad Ekleme
            lblTC.Text = tc;
            SqlCommand komutAdSoyadGetir = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@hastaTC;",bgl.baglanti());
            komutAdSoyadGetir.Parameters.AddWithValue("@hastaTC", tc);
            SqlDataReader drAdSoyadGetir = komutAdSoyadGetir.ExecuteReader();
            while (drAdSoyadGetir.Read())
            {
                lblAdSoyad.Text = drAdSoyadGetir[0] + " " + drAdSoyadGetir[1];
            }
            bgl.baglanti().Close();
            //Randevuları Çekme
            DataTable randevuTablosu = new DataTable();
            SqlDataAdapter randevuAdeptar = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" + tc,bgl.baglanti());
            randevuAdeptar.Fill(randevuTablosu);
            dataGridView1.DataSource = randevuTablosu;

            //Branşları Çekme
            SqlCommand komutBransGetir = new SqlCommand("Select BransAd from Tbl_Branslar;", bgl.baglanti());
            SqlDataReader drBransGetir = komutBransGetir.ExecuteReader();
            while (drBransGetir.Read())
            {
                cmbBrans.Items.Add(drBransGetir[0]);
            }
            bgl.baglanti().Close();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komutDoktorGetir = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@bransAd;",bgl.baglanti());
            komutDoktorGetir.Parameters.AddWithValue("@bransAd", cmbBrans.Text);
            SqlDataReader drDoktorGetir = komutDoktorGetir.ExecuteReader();
            while (drDoktorGetir.Read()) 
            {
                cmbDoktor.Items.Add(drDoktorGetir[0] + " " + drDoktorGetir[1]);
            }
            bgl.baglanti().Close();
            
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable randevuTablosu = new DataTable();
            SqlDataAdapter randevuAdapter = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBrans='"+cmbBrans.Text+"'"+" and RandevuDoktor='"+cmbDoktor.Text+"' and RandevuDurum=0;",bgl.baglanti());
            randevuAdapter.Fill(randevuTablosu);
            dataGridView2.DataSource = randevuTablosu;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lnkBilgiGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            BilgiGuncelle blgGuncelle= new BilgiGuncelle();
            blgGuncelle.tcNo = lblTC.Text;
            blgGuncelle.Show();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtBoxID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komutRandevuAl = new SqlCommand("Update Tbl_Randevular set RandevuDurum=1,HastaTC=@h1,HastaSikayet=@h2 where Randevuid=@h3;", bgl.baglanti());
            komutRandevuAl.Parameters.AddWithValue("@h1",lblTC.Text);
            komutRandevuAl.Parameters.AddWithValue("@h2",rchTxtBoxSikayet.Text);
            komutRandevuAl.Parameters.AddWithValue("@h3",txtBoxID.Text);
            komutRandevuAl.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı.");
        }
    }
}
