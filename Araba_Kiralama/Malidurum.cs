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

namespace Araba_Kiralama
{
    public partial class Malidurum : Form
    {
        public Malidurum()
        {
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void toplamaraba()
        {
            int toplam;
            baglan.Open();
            SqlCommand komut = new SqlCommand("select count(marka) from arabaeklelis ", baglan);
            toplam = Convert.ToInt32(komut.ExecuteScalar());
            label1.Text = Convert.ToString(toplam);
            baglan.Close();
        }
        private void kiradakiaraba()
        {
            int paralar;
            baglan.Open();
            SqlCommand komut1 = new SqlCommand("select sum(para) from kiraliklis", baglan);
            paralar = Convert.ToInt32(komut1.ExecuteScalar());
            label5.Text = Convert.ToString(paralar);
            baglan.Close();
        }
        private void kira()
        {
            int kira;
            baglan.Open();
            SqlCommand komut = new SqlCommand("select count(kira) from kiraliklis where kira='1'", baglan);
            kira = Convert.ToInt32(komut.ExecuteScalar());
            label4.Text = Convert.ToString(kira);
            baglan.Close();
        }


        private void Malidurum_Load(object sender, EventArgs e)
        {
            toplamaraba();
            kiradakiaraba();
            kira();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            anasayfa a = new anasayfa();
            a.Show();
            this.Hide();
        }
    }
}
