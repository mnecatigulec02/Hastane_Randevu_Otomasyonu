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
    public partial class UyeOl : Form
    {
        public UyeOl()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btnKayıtOl_Click(object sender, EventArgs e)
        {
            SqlCommand komutHastaEkle= new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@h1,@h2,@h3,@h4,@h5,@h6)",bgl.baglanti());
            komutHastaEkle.Parameters.AddWithValue("@h1", txtBoxAd.Text);
            komutHastaEkle.Parameters.AddWithValue("@h2", txtBoxSoyad.Text);
            komutHastaEkle.Parameters.AddWithValue("@h3", mskTC.Text);
            komutHastaEkle.Parameters.AddWithValue("@h4", mskTelefon.Text);
            komutHastaEkle.Parameters.AddWithValue("@h5", txtBoxSifre.Text);
            komutHastaEkle.Parameters.AddWithValue("@h6", cmbCinsiyet.Text);
            komutHastaEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Oluşturulmuştur. Şifreniz : "+txtBoxSifre.Text,"Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }
    }
}
