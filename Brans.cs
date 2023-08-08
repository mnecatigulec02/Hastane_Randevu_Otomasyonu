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
    public partial class Brans : Form
    {
        public Brans()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void Brans_Load(object sender, EventArgs e)
        {
            DataTable dtBrans = new DataTable();
            SqlDataAdapter daBrans = new SqlDataAdapter("select * from Tbl_Branslar",bgl.baglanti());
            daBrans.Fill(dtBrans);
            dataGridView1.DataSource= dtBrans;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komutBransEkle = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1);",bgl.baglanti());
            komutBransEkle.Parameters.AddWithValue("@b1",txtBransAd.Text);
            komutBransEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutBransSil = new SqlCommand("delete from Tbl_Branslar where Bransid=@b1;", bgl.baglanti());
            komutBransSil.Parameters.AddWithValue("@b1",txtID.Text);
            komutBransSil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Silindi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutBransGuncelle = new SqlCommand("Update Tbl_Branslar set BransAd=@b1 where Bransid=@b2;", bgl.baglanti());
            komutBransGuncelle.Parameters.AddWithValue("@b1",txtBransAd.Text);
            komutBransGuncelle.Parameters.AddWithValue("@b2",txtID.Text);
            komutBransGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Adı Güncellendi.");
        }
    }
}
