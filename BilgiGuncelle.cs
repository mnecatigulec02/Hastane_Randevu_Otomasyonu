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
    public partial class BilgiGuncelle : Form
    {
        public BilgiGuncelle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tcNo;
        private void BilgiGuncelle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tcNo;
            SqlCommand komutHastaBilgileriGetir= new SqlCommand("select * from Tbl_Hastalar where HastaTC=@h1",bgl.baglanti());
            komutHastaBilgileriGetir.Parameters.AddWithValue("@h1",mskTC.Text);
            SqlDataReader drHastaBilgileriniOku = komutHastaBilgileriGetir.ExecuteReader();
            while (drHastaBilgileriniOku.Read())
            {
                txtBoxAd.Text = drHastaBilgileriniOku[1].ToString();
                txtBoxSoyad.Text = drHastaBilgileriniOku[2].ToString();
                mskTelefon.Text= drHastaBilgileriniOku[4].ToString();
                txtBoxSifre.Text= drHastaBilgileriniOku[5].ToString();
                cmbCinsiyet.Text= drHastaBilgileriniOku[6].ToString();
                
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutBilgiGüncelle = new SqlCommand("Update Tbl_Hastalar set HastaAd=@h1,HastaSoyad=@h2,HastaTelefon=@h3,HastaSifre=@h4,HastaCinsiyet=@h5 where HastaTC=@h6;",bgl.baglanti());
            komutBilgiGüncelle.Parameters.AddWithValue("@h1",txtBoxAd.Text);    
            komutBilgiGüncelle.Parameters.AddWithValue("@h2",txtBoxSoyad.Text);    
            komutBilgiGüncelle.Parameters.AddWithValue("@h3",mskTelefon.Text);    
            komutBilgiGüncelle.Parameters.AddWithValue("@h4",txtBoxSifre.Text);    
            komutBilgiGüncelle.Parameters.AddWithValue("@h5",cmbCinsiyet.Text);    
            komutBilgiGüncelle.Parameters.AddWithValue("@h6",mskTC.Text);    
            komutBilgiGüncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
    }
}
