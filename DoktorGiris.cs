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
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komutDoktorGiris = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC=@d1 and DoktorSifre=@d2;",bgl.baglanti());
            komutDoktorGiris.Parameters.AddWithValue("@d1", mskTC.Text);
            komutDoktorGiris.Parameters.AddWithValue("@d2", txtSifre.Text);
            SqlDataReader drDoktorGiris = komutDoktorGiris.ExecuteReader();
            if (drDoktorGiris.Read())
            {
                DoktorDetay drDetay = new DoktorDetay();
                drDetay.TCno= mskTC.Text;
                drDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC ve ya Şifre!");
            }
        }
    }
}
