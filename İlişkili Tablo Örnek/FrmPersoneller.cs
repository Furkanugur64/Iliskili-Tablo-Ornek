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

namespace İlişkili_Tablo_Örnek
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=Furkan-PC;Initial Catalog=İlişkili_Tablo_Örnek;Integrated Security=True");

        void Personel_Listesi()
        {

            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            TxtSoyad.Text = "";
            Txtid.Text = "";
            TxtAd.Text = "";
        }
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            Personel_Listesi();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells["AD"].Value.ToString();
            TxtSoyad.Text = dataGridView1.CurrentRow.Cells["SOYAD"].Value.ToString();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD) values (@p1,@p2)", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Personel Sisteme Kaydedildi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Personel_Listesi();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Personeli Silmek İstiyor musunuz ? ", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (mesaj == DialogResult.Yes)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("delete from TBL_PERSONELLER where ID=@p1", baglantı);
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Personel Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Personel_Listesi();
                temizle();
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("Update TBL_PERSONELLER set AD=@p1,SOYAD=@p2 where ID=@p3", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Personel Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Personel_Listesi();
            temizle();
        }
    }
}
