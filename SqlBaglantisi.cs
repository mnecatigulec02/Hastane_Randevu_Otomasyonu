using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Randevu_Otomasyonu
{
    internal class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-PVEILPL;Initial Catalog=DB_Hastane;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
