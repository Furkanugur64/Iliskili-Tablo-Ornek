using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İlişkili_Tablo_Örnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMusterıler fr = new FrmMusterıler();
            fr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmPersoneller fr = new FrmPersoneller();
            fr.ShowDialog();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmÜrünler fr = new FrmÜrünler();
            fr.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmHareketler fr = new FrmHareketler();
            fr.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       


        
    }
}
