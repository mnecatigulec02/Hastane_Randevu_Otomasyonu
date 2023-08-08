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
    public partial class SekreterGiris : Form
    {
        public SekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komutSekreterGirisOnayla = new SqlCommand("select * from Tbl_Sekreter where SekreterTC=@tc and SekreterSifre=@sifre",bgl.baglanti());
            komutSekreterGirisOnayla.Parameters.AddWithValue("@tc",mskTC.Text);   
            komutSekreterGirisOnayla.Parameters.AddWithValue("@sifre",txtSifre.Text);   
            SqlDataReader drSekreterGiris = komutSekreterGirisOnayla.ExecuteReader();
            if (drSekreterGiris.Read())
            {
                SekreterDetay skrtrDetay = new SekreterDetay();
                skrtrDetay.tcNo=mskTC.Text;
                skrtrDetay.Show();
            }
            else
            {
                MessageBox.Show("Hatalı TC ve ya Şifre");
            }
            
            
        }
    }
}
