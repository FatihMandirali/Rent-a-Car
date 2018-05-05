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
    public partial class KiradakiArabalar : Form
    {
        public KiradakiArabalar()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=PC\\SQLEXPRESS;Initial Catalog=RentACar;Integrated Security=True");
        private void günceltarih()
        {
            baglan.Open();
            SqlCommand komut1 = new SqlCommand("select gunluk from arabaeklelis where marka='" + textBox3.Text.ToString() + "' and kira='1'", baglan);
            SqlDataReader oku = komut1.ExecuteReader();
            while (oku.Read())
            {
                label11.Text = oku["gunluk"].ToString();
            }
            
            baglan.Close();


            DateTime kck1 = Convert.ToDateTime(dateTimePicker3.Text);
            DateTime byk1 = Convert.ToDateTime(dateTimePicker2.Text);
            DateTime enkck = Convert.ToDateTime(dateTimePicker1.Text);


            TimeSpan sonuc1;
            sonuc1 = byk1 - enkck;
            label12.Text = sonuc1.TotalDays.ToString();

            int ücret;
            ücret = Convert.ToInt32(label12.Text) * Convert.ToInt32(label11.Text);
            textBox8.Text = ücret.ToString();

            int uzatmaücret;
            uzatmaücret = Convert.ToInt32(label11.Text) * Convert.ToInt32(textBox5.Text);
            textBox7.Text = uzatmaücret.ToString();
        }

        private void kalan()
        {
            DateTime kck = Convert.ToDateTime(dateTimePicker3.Text);
            DateTime byk = Convert.ToDateTime(dateTimePicker2.Text);

            TimeSpan sonuc;
            sonuc = byk - kck;
            textBox5.Text = sonuc.TotalDays.ToString();
            
        }
        private void goster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from kiraliklis where kira='1'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["soyad"].ToString());
                ekle.SubItems.Add(oku["tc"].ToString());
                ekle.SubItems.Add(oku["marka"].ToString());
                ekle.SubItems.Add(oku["model"].ToString());
                ekle.SubItems.Add(oku["gtarih"].ToString());
                ekle.SubItems.Add(oku["ctarih"].ToString());
                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }
        private void KiradakiArabalar_Load(object sender, EventArgs e)
        {
            goster();
           // kalan();
        }
        int id = 0;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;
            //textBox5.Text = listView1.SelectedItems[0].SubItems[8].Text;
            kalan();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
         /*   if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen İşlem Yapmak İstediğiniz Arabanın ID'sini Seçin.","Uyarı!");
            }
            else
            {*/
                DialogResult a;
                a = MessageBox.Show("Arabayı Teslim Almayı Onaylıyor Musunuz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (a == DialogResult.Yes)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("update kiraliklis set kira='0' where ad='" + textBox1.Text.ToString() + "' and tc='" + textBox4.Text.ToString() + "'", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    goster();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    dateTimePicker1.Text = "";
                    dateTimePicker2.Text = "";
                    textBox6.Clear();
               // }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          /*  if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen İşlem Yapmak İstediğiniz Arabanın ID'sini Seçin.","Uyarı!");
            }
            else
            {*/

                
                    DialogResult b;
                    b = MessageBox.Show("Kiradaki Arabanın Gününü Uzatmak İstiyor Musunuz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (b == DialogResult.Yes)
                    {
                        
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("update kiraliklis set ctarih='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "',para='" + textBox8.Text.ToString() + "' where ad='" + textBox1.Text.ToString() + "' and soyad='" + textBox2.Text.ToString() + "'", baglan);
                        komut.ExecuteNonQuery();
                        baglan.Close();
                        goster();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    dateTimePicker1.Text = "";
                    dateTimePicker2.Text = "";
                    textBox6.Clear();

              //  }

                
            }
        
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
           /* if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen İşlem Yapmak İstediğiniz Arabanın ID'sini Seçin.", "Uyarı!");
                dateTimePicker2.Text = "";
            }
            else
            {*/
                kalan();
                günceltarih();
                if (Convert.ToInt32(textBox5.Text) < 0)
                {
                    MessageBox.Show("Lütfen Tarihi Gelecek Tarihlerden Seçin.", "Uyarı !");
                 
              //  }
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult bb;
            bb = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz ?", "Dikkat !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (bb == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            anasayfa a = new anasayfa();
            a.Show();
            this.Hide();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
