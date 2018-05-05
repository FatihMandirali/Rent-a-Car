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
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void label3_Click(object sender, EventArgs e)
        {
            arabaekle a = new arabaekle();
            a.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Hesapayarları a = new Hesapayarları();
            a.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Çıkış Yapmak İstiyor Musunuz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("update patronlis set aktif='0' where aktif='1'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                this.Close();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Giriş Sayfasına Dönmek İstiyor Musunuz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("update patronlis set aktif='0' where aktif='1'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                Form1 f = new Form1();
                f.Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ArabaKirala g = new ArabaKirala();
            g.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            KiradakiArabalar a = new KiradakiArabalar();
            a.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Malidurum m = new Malidurum();
            m.Show();
            this.Hide();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
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

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }

        private void anasayfa_Load(object sender, EventArgs e)
        {
            int say=0;
            int fark;
            baglan.Open();
            SqlCommand kmt = new SqlCommand("select datediff(day,ctarih,getdate()) from kiraliklis where kira='1'", baglan);
           
                fark = Convert.ToInt32(kmt.ExecuteScalar());
                label7.Text = Convert.ToString(fark);
                if (fark > 0)
                {
                    say++;
                }
            
            baglan.Close();
            if (say > 0)
            {
                listBox1.Items.Add("***Dikkat Ediniz Arabayı Teslim Etmeyen Müşteri Var.");
            }
            label9.Text = Convert.ToString(say);
            
        }
    }
}
