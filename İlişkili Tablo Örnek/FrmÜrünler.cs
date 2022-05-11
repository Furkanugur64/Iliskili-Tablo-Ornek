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
    public partial class FrmÜrünler : Form
    {
        public FrmÜrünler()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=Furkan-PC;Initial Catalog=İlişkili_Tablo_Örnek;Integrated Security=True");

        void temizle()
        {
            Txtalısfıyat.Text = "";
            Txtid.Text = "";
            Txtsatısfiyat.Text = "";
            Txtstok.Text = "";
            TxtUrunad.Text = "";
        }
        void Ürün_Listesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_URUNLER", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmÜrünler_Load(object sender, EventArgs e)
        {
            Ürün_Listesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_URUNLER (URUNAD,STOK,ALISFIYAT,SATISFIYAT) values (@p1,@p2,@p3,@p4) ", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtUrunad.Text);
            komut.Parameters.AddWithValue("@p2", Txtstok.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(Txtalısfıyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(Txtsatısfiyat.Text));
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Ürün sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Ürün_Listesi();
            temizle();
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            TxtUrunad.Text = dataGridView1.CurrentRow.Cells["URUNAD"].Value.ToString();
            Txtstok.Text = dataGridView1.CurrentRow.Cells["STOK"].Value.ToString();
            Txtalısfıyat.Text = dataGridView1.CurrentRow.Cells["ALISFIYAT"].Value.ToString();
            Txtsatısfiyat.Text = dataGridView1.CurrentRow.Cells["SATISFIYAT"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Ürünü Silmek İstiyor musunuz ? ", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (mesaj == DialogResult.Yes)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("Delete from TBL_URUNLER where ID=@p1", baglantı);
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Ürün sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Ürün_Listesi();
                temizle();
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("update TBL_URUNLER set URUNAD=@P1,STOK=@P2,ALISFIYAT=@P3,SATISFIYAT=@P4 where ID=@p5", baglantı);
            komut.Parameters.AddWithValue("@p1", TxtUrunad.Text);
            komut.Parameters.AddWithValue("@p2", Txtstok.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(Txtalısfıyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(Txtsatısfiyat.Text));
            komut.Parameters.AddWithValue("@p5", Txtid.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Ürün Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Ürün_Listesi();
            temizle();
        }

       
    }
}
