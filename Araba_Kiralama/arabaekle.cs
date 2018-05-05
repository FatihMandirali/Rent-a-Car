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
using System.IO;

namespace Araba_Kiralama
{
    public partial class arabaekle : Form
    {
        public arabaekle()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void göster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from arabaeklelis", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["marka"].ToString());
                ekle.SubItems.Add(oku["model"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["km"].ToString());
                ekle.SubItems.Add(oku["hacim"].ToString());
                ekle.SubItems.Add(oku["hasar"].ToString());
                ekle.SubItems.Add(oku["resim"].ToString());
                ekle.SubItems.Add(oku["gunluk"].ToString());
                listView1.Items.Add(ekle);

            }
            baglan.Close();
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "Seçiniz" || comboBox2.Text == "Seçiniz"|| textBox6.Text=="")
            {
                MessageBox.Show("Lütfen Formu Düzgün Doldurun.","UYARI");
            }
            else
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("insert into arabaeklelis(marka,model,tarih,km,hacim,hasar,resim,gunluk,kira) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox3.Text.ToString() + "','" + comboBox1.Text.ToString() + "','" + comboBox2.Text.ToString() + "','" + label8.Text.ToString() + "','"+textBox6.Text.ToString()+"','0')", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox6.Clear();
                comboBox1.Text = "Seçiniz";
                comboBox2.Text = "Seçiniz";
                pictureBox1.Image = null;
                dateTimePicker1.Text = "";
                openFileDialog1.Filter = "Resim Dosyası | *.jpg | Video | *.avi | Tüm Dosyalar | *.* ";
                göster();
                File.Copy(openFileDialog1.FileName, @"F:\Dersler\c# uygulama\Ozeller\Araba_Kiralama\Araba_Kiralama\bin\Debug\resim\" + label12.Text+".jpg");
            }
        }
        int id = 0;
        private void simpleButton2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("delete from arabaeklelis where id=(" + id + ")", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            listView1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox6.Clear();
            pictureBox1.Image = null;
            comboBox1.Text = "Seçiniz";
            comboBox2.Text = "Seçiniz";
            dateTimePicker1.Text = "";
            göster();
        }

        private void arabaekle_Load(object sender, EventArgs e)
        {
           
            göster();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[5].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[6].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[7].Text;
            pictureBox1.ImageLocation = listView1.SelectedItems[0].SubItems[7].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[8].Text;
           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Random rastgele = new Random();
                String kelime = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz123456789";
                String uret = "";
                for (int i = 0; i <= 10; i++)
                {
                    uret += kelime[rastgele.Next(kelime.Length)];
                    label12.Text = uret;
                }
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);               
               

            }
            label8.Text = @"F:\Dersler\c# uygulama\Ozeller\Araba_Kiralama\Araba_Kiralama\bin\Debug\resim\" + label12.Text+".jpg";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_MouseEnter(object sender, EventArgs e)
        {
           /* if (textBox4.Text != "")
            {
                simpleButton5.Enabled = true;
                
            }*/
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox4.Visible = true;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox4.Visible = false;
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("update arabaeklelis set marka='" + textBox1.Text.ToString() + "',model='" + textBox2.Text.ToString() + "',tarih='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',km='" + textBox3.Text.ToString() + "',hacim='" + comboBox1.Text.ToString() + "',hasar='" + comboBox2.Text.ToString() + "',gunluk='"+textBox6.Text.ToString()+"' where id=" + id + "", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            göster();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from arabaeklelis where marka like '%"+textBox5.Text.ToString()+"%'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["marka"].ToString());
                ekle.SubItems.Add(oku["model"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["km"].ToString());
                ekle.SubItems.Add(oku["hacim"].ToString());
                ekle.SubItems.Add(oku["hasar"].ToString());
                ekle.SubItems.Add(oku["resim"].ToString());
                listView1.Items.Add(ekle);

            }
            baglan.Close();
            textBox5.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = "Seçiniz.";
            comboBox2.Text = "Seçiniz.";
            dateTimePicker1.Text = "";
            pictureBox1.Image = null;
            textBox6.Clear();
            pictureBox1.Image = null;

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            anasayfa a = new anasayfa();
            a.Show();
            this.Close();
        }
    }
}
