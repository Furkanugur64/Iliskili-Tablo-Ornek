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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=Furkan-PC;Initial Catalog=İlişkili_Tablo_Örnek;Integrated Security=True");

        void temizle()
        {
            Txtid.Text = "";
            TxtFiyat.Text = "";
            Cmbpersonel.SelectedIndex = -1;
            Cmburun.SelectedIndex = -1;
            Cmbmusteri.SelectedIndex = -1;

        }
        void Hareket_Listesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("execute Hareketbilgi", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            //Ürünleri Comboboxa çekme
            SqlDataAdapter da = new SqlDataAdapter("Select * From  TBL_URUNLER ", baglantı);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cmburun.DisplayMember = "URUNAD";
            Cmburun.ValueMember = "ID";
            Cmburun.DataSource = dt;
            Cmburun.SelectedIndex = -1;

            //Müşterileri Comboboxa Çekme
            SqlDataAdapter da2 = new SqlDataAdapter("select * From TBL_MUSTERILER", baglantı);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            Cmbmusteri.DisplayMember = "AD";
            Cmbmusteri.ValueMember = "ID";
            Cmbmusteri.DataSource = dt2;
            Cmbmusteri.SelectedIndex = -1;

            //Personelleri comboboxa çekme
            SqlDataAdapter da3 = new SqlDataAdapter("Select * From  TBL_PERSONELLER", baglantı);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            Cmbpersonel.DisplayMember = "AD";
            Cmbpersonel.ValueMember = "ID";
            Cmbpersonel.DataSource = dt3;
            Cmbpersonel.SelectedIndex = -1;

            Hareket_Listesi();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtFiyat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            Cmbmusteri.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Cmburun.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Cmbpersonel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_HAREKETLER (URUNID,MUSTERIID,PERSONELID,FIYAT) Values (@p1,@p2,@p3,@p4)", baglantı);
            komut.Parameters.AddWithValue("@p1", Cmburun.SelectedValue);
            komut.Parameters.AddWithValue("@p2", Cmbmusteri.SelectedValue);
            komut.Parameters.AddWithValue("@p3", Cmbpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p4", TxtFiyat.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Hareket Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Hareket_Listesi();
            temizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = MessageBox.Show("Hareketi Silmek İstiyor Musunuz ? ", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (mesaj == DialogResult.Yes)
            {


                baglantı.Open();
                SqlCommand komut = new SqlCommand("delete from TBL_HAREKETLER where HAREKETID=@P1", baglantı);
                komut.Parameters.AddWithValue("@P1", Txtid.Text);
                komut.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Hareket Bilgisi Sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hareket_Listesi();
                temizle();
            }

        }




    }
}
