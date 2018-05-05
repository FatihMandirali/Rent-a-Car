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
    public partial class ArabaKirala : Form
    {
        public ArabaKirala()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void göster()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from arabaeklelis where kira='0'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["marka"].ToString());
                ekle.SubItems.Add(oku["model"].ToString());
                ekle.SubItems.Add(oku["hacim"].ToString());
                ekle.SubItems.Add(oku["km"].ToString());
                ekle.SubItems.Add(oku["hasar"].ToString());
                ekle.SubItems.Add(oku["resim"].ToString());
                listView1.Items.Add(ekle);

            }
            baglan.Close();

        }
        private void ArabaKirala_Load(object sender, EventArgs e)
        {
            göster();
        }
        int id = 0;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox8.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox7.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox9.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;
            pictureBox1.ImageLocation = listView1.SelectedItems[0].SubItems[6].Text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||textBox2.Text==""|| textBox3.Text==""|| textBox4.Text == "" || textBox6.Text == "" || maskedTextBox1.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("Lütfen Formu Düzgün Doldurun.", "UYARI !");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                dateTimePicker1.Text = "";
                dateTimePicker2.Text = "";
                maskedTextBox1.Clear();
            }
            else
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("insert into kiraliklis(ad,soyad,tc,tel,mail,marka,model,hacim,km,hasar,gtarih,ctarih,para) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + maskedTextBox1.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox8.Text.ToString() + "','" + textBox7.Text.ToString() + "','" + textBox9.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','" + textBox10.Text.ToString() + "')", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Veriler Başarılı Bir Şekilde Kaydedildi.", "Başarılı.!");
              
                baglan.Open();
                SqlCommand komut1 = new SqlCommand("update kiraliklis set kira='1' where tc='" + textBox3.Text.ToString() + "'", baglan);
                komut1.ExecuteNonQuery();
                baglan.Close();

                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update arabaeklelis set kira='1' where model='"+textBox7.Text.ToString()+"' and marka='"+textBox8.Text.ToString()+"' and hacim='"+textBox9.Text.ToString()+"'",baglan);
                komut2.ExecuteNonQuery();
                baglan.Close();

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                dateTimePicker1.Text = "";
                dateTimePicker2.Text = "";
                pictureBox1.Image = null;
                maskedTextBox1.Clear();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut5 = new SqlCommand("select gunluk from arabaeklelis where marka='" + textBox8.Text.ToString() + "'", baglan);
            SqlDataReader oku = komut5.ExecuteReader();
            while (oku.Read())
            {
                label12.Text = oku["gunluk"].ToString();
            }
            baglan.Close();
            int ücret;
            DateTime kck = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime byk = Convert.ToDateTime(dateTimePicker2.Text);
            TimeSpan sonuc;
            sonuc = byk - kck;

            label11.Text = sonuc.TotalDays.ToString();
            ücret = Convert.ToInt32(label11.Text) *Convert.ToInt32(label12.Text);
            textBox10.Text = ücret.ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult a;
            a = MessageBox.Show("Anasayfaya Dönmek İstediğinize Emin Misiniz ?", "UYARI !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (a == DialogResult.Yes)
            {
                anasayfa an = new anasayfa();
                an.Show();
                this.Hide();
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult ab;
            ab = MessageBox.Show("Çıkış Yapmak İstediğinize Emin Misiniz ?", "UYARI !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (ab == DialogResult.Yes)
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
