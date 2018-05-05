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
using System.Data.Sql;

namespace Araba_Kiralama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += 4;
            if(pictureBox1.Location.X == 16)
            {
                label4.Text = "Hoşgeldiniz.";
                label4.ForeColor = Color.OliveDrab;
            }
           else if (pictureBox1.Location.X == 60)
            {
                label4.Text = "Araba Damalı Bayrağa Gitmeden Giriş Yapınız.";
                label4.ForeColor = Color.Coral;
            }
            else if(pictureBox1.Location.X == 100)
            {
                label4.Text = "Araba Damalı Bayrağa Yaklaşıyor Acele Ediniz.";
                label4.ForeColor = Color.Red;
            }
          else   if (pictureBox1.Location.X == 180)
            {
                label4.Text = "Giriş Yapmanız Bekleniyor.";
                label4.ForeColor = Color.DarkRed;
            }

            if (pictureBox1.Location.X == 196)
            {
                timer1.Enabled = false;
                DialogResult cik;
                cik = MessageBox.Show("Giriş Yapmak İçin Süreniz Doldu ! Yeniden Giriş İçin Evet'e Çıkmak İçin Hayır'a Basın !", "UYARI !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (cik == DialogResult.Yes)
                {
                    Point a = new Point();
                    a.X=12;
                    a.Y = 196;
                    pictureBox1.Location = a;
                    timer1.Enabled = true;
                    label4.Text = "Hoşgeldinz.";
                    label4.ForeColor = Color.OliveDrab;
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Sifremi_Unuttum f = new Sifremi_Unuttum();
            f.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

                baglan.Open();
                string sql = "select * from patronlis where kadi=@kadi and sifre=@sifre";
                SqlParameter prm1 = new SqlParameter("kadi", textBox1.Text.Trim());
                SqlParameter prm2 = new SqlParameter("sifre", textBox3.Text.Trim());
                SqlCommand komut = new SqlCommand(sql, baglan);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    baglan.Close();
                    baglan.Open();

                    anasayfa a = new anasayfa();
                    a.Show();
                    this.Hide();
                    timer1.Enabled = false;
                    SqlCommand komuy = new SqlCommand("update patronlis set  aktif='"+label5.Text.ToString()+"' where kadi='"+textBox1.Text.ToString()+"' ", baglan);
                    komuy.ExecuteNonQuery();
                    baglan.Close();
                }
                else
                {
                    MessageBox.Show("Hata Oluştu.","Dikkat!");
                }
               
            
           
            baglan.Close();
        }
    }

}
