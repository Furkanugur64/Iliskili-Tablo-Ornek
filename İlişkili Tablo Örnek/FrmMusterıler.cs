using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İlişkili_Tablo_Örnek
{
    public partial class FrmMusterıler : Form
    {
        public FrmMusterıler()
        {
            InitializeComponent();
        }


        SqlConnection baglantı = new SqlConnection("Data Source=Furkan-PC;Initial Catalog=İlişkili_Tablo_Örnek;Integrated Security=True");

        void Musteri_Listesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmMusterıler_Load(object sender, EventArgs e)
        {
            Musteri_Listesi();
        }
        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtSoyad.Text = "";
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD) values (@p1,@p2)", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Müşteri sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Musteri_Listesi();
            temizle();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            TxtAd.Text = dataGridView1.CurrentRow.Cells["AD"].Value.ToString();
            TxtSoyad.Text = dataGridView1.CurrentRow.Cells["SOYAD"].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Müşteri Silmek İstiyor musunuz ? ", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (mesaj == DialogResult.Yes)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("delete from TBL_MUSTERILER where ID=@p1", baglantı);
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                Musteri_Listesi();
                temizle();
                MessageBox.Show("Müşteri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@p1,SOYAD=@p2 where ID=@p3", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Müşteri bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Musteri_Listesi();
            temizle();

        }


    }
}
