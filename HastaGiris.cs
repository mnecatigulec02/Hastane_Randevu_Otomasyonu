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
    public partial class HastaGiris : Form
    {
        public HastaGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UyeOl kaydol = new UyeOl();
            kaydol.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komutHastaGiris = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@tc and HastaSifre=@hastaSifre;", bgl.baglanti());
            komutHastaGiris.Parameters.AddWithValue("@tc", mskTC.Text);
            komutHastaGiris.Parameters.AddWithValue("@hastaSifre", txtSifre.Text);
            SqlDataReader drHastaGiris = komutHastaGiris.ExecuteReader();
            if (drHastaGiris.Read())
            {
                HastaDetay hastaDetay = new HastaDetay();
                hastaDetay.tc = mskTC.Text;
                hastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC ve ya Şifre Hatalı.");
            }
            bgl.baglanti().Close();
        }

        private void HastaGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
