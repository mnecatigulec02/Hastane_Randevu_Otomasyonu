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
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tcNo;
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tcNo;

            //Ad Soyad 
            SqlCommand komutAdSoyadGetir = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC='" + lblTC.Text + "'", bgl.baglanti());
            SqlDataReader drAdSoyadGetir = komutAdSoyadGetir.ExecuteReader();
            while (drAdSoyadGetir.Read())
            {
                lblAdSoyad.Text = drAdSoyadGetir[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları DataGrid'e aktarma
            DataTable bransTablosuGetir = new DataTable();
            SqlDataAdapter bransAdapter = new SqlDataAdapter("select * from Tbl_Branslar;", bgl.baglanti());
            bransAdapter.Fill(bransTablosuGetir);
            dataGridView1.DataSource = bransTablosuGetir;

            //Doktorları DataGrid'e aktarma
            DataTable dtDoktorGetir = new DataTable();
            SqlDataAdapter daDoktorGetir = new SqlDataAdapter("select Doktorid,(DoktorAd+' '+ DoktorSoyad) as 'Doktor AD Soyad',DoktorBrans from Tbl_Doktorlar;", bgl.baglanti());
            daDoktorGetir.Fill(dtDoktorGetir);
            dataGridView2.DataSource = dtDoktorGetir;

            //Doktorları DataGrid'e aktarma
            SqlCommand komutBransGetir = new SqlCommand("select BransAd from Tbl_Branslar;", bgl.baglanti());
            SqlDataReader drBransGetir = komutBransGetir.ExecuteReader();
            while (drBransGetir.Read())
            {
                cmbBrans.Items.Add(drBransGetir[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutRandevuKaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuDoktor,RandevuBrans,RandevuDurum) values (@rTarih,@rSaat,@rDoktor,@rBrans,@rDurum);", bgl.baglanti());
            komutRandevuKaydet.Parameters.AddWithValue("@rTarih", mskTarih.Text);
            komutRandevuKaydet.Parameters.AddWithValue("@rSaat", mskSaat.Text);
            komutRandevuKaydet.Parameters.AddWithValue("@rDoktor", cmbDoktor.Text);
            komutRandevuKaydet.Parameters.AddWithValue("@rBrans", cmbBrans.Text);
            komutRandevuKaydet.Parameters.AddWithValue("@rDurum", label11.Text);
            komutRandevuKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.");
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            cmbDoktor.Text = "";
            SqlCommand komutDoktorGetir = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans='" + cmbBrans.Text + "'", bgl.baglanti());
            SqlDataReader drDoktorGetir = komutDoktorGetir.ExecuteReader();
            while (drDoktorGetir.Read())
            {
                cmbDoktor.Items.Add(drDoktorGetir[0] + " " + drDoktorGetir[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komutDuyuruOlustur = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1);", bgl.baglanti());
            komutDuyuruOlustur.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komutDuyuruOlustur.ExecuteNonQuery();
            MessageBox.Show("Duyuru Oluşturuldu.");
            bgl.baglanti().Close();
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            DoktorPaneli dctrPaneliGiris = new DoktorPaneli();
            dctrPaneliGiris.Show();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            Brans bransPaneli = new Brans();
            bransPaneli.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            RandevuListesi rndListesi = new RandevuListesi();
            rndListesi.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void btnDuyuruGetir_Click(object sender, EventArgs e)
        {
            Duyurular dyr = new Duyurular();
            dyr.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void chkDurum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDurum.Checked)
            {
                label11.Text = "true";
            }
            else { label11.Text = "false"; }
        }
    }
}
