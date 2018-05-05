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
    public partial class Hesapayarları : Form
    {
        public Hesapayarları()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="" || textBox2.Text=="" || textBox3.Text==""|| textBox4.Text=="" || textBox5.Text=="" || textBox6.Text=="" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen Boşlukları Düzgün Doldurun.");

            }
            else
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("insert into patronlis(ad,soyad,kadi,sifre,tc,tel,mail) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + maskedTextBox1.Text.ToString() + "','" + textBox5.Text.ToString() + "')", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı Bir Şekilde Onaylandı.");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                maskedTextBox1.Clear();
                baglan.Close();

            }
        }

        private void Hesapayarları_Load(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from patronlis where aktif='1'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textBox9.Text = oku["ad"].ToString();
                textBox10.Text = oku["soyad"].ToString();
                textBox12.Text = oku["kadi"].ToString();
                textBox7.Text = oku["sifre"].ToString();
                textBox11.Text = oku["tc"].ToString();
                maskedTextBox2.Text = oku["tel"].ToString();
                textBox8.Text = oku["mail"].ToString();
                
            }
            baglan.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Formda Boş Yer Bırakmayın.");
            }
            else
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("update patronlis set ad='" + textBox9.Text.ToString() + "',soyad='" + textBox10.Text.ToString() + "',kadi='" + textBox12.Text.ToString() + "',sifre='" + textBox7.Text.ToString() + "',tc='" + textBox11.Text.ToString() + "',tel='" + maskedTextBox2.Text.ToString() + "',mail='" + textBox7.Text.ToString() + "'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Verileriniz Başarılı Bir Şekilde Güncellendi.");
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox4.Visible = true;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox1.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Çıkmak İstediğinize Emin Misiz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("update patronlis set aktif='0' where aktif='1'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
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
