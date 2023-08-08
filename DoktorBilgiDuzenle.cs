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
    public partial class DoktorBilgiDuzenle : Form
    {
        public DoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tcNo;
        private void DoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tcNo;

            SqlCommand komutDoktorGetir = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTc='"+mskTC.Text+"'", bgl.baglanti());
            SqlDataReader drDoktorGetir = komutDoktorGetir.ExecuteReader();
            while (drDoktorGetir.Read())
            {
                txtBoxAd.Text = drDoktorGetir[1].ToString();
                txtBoxSoyad.Text = drDoktorGetir[2].ToString();
                cmbBrans.Text= drDoktorGetir[3].ToString();
                txtBoxSifre.Text= drDoktorGetir[5].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutBilgiGüncelle = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d4 where DoktorTC=@d5;", bgl.baglanti());
            komutBilgiGüncelle.Parameters.AddWithValue("@d1", txtBoxAd.Text);
            komutBilgiGüncelle.Parameters.AddWithValue("@d2", txtBoxSoyad.Text);
            komutBilgiGüncelle.Parameters.AddWithValue("@d3", cmbBrans.Text);
            komutBilgiGüncelle.Parameters.AddWithValue("@d4", txtBoxSifre.Text);
            komutBilgiGüncelle.Parameters.AddWithValue("@d5", mskTC.Text);            
            komutBilgiGüncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
