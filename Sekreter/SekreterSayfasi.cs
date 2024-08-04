using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneOto
{
    public partial class SekreterSayfasi : Form
    {
        public SekreterSayfasi()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        SqlDataAdapter da;
        SqlConnection baglanti = new SqlConnection("server=.;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");

        void hastaGetir()
        {
            baglanti.Open();
            da = new SqlDataAdapter("Select isim AS 'Ad Soyad',tc_no as 'T.C.No',telefon as Telefon,cinsiyet as Cinsiyet,dog_tar as 'Doğum Tarihi',adres as Adres from Hasta", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();

        }
        void RandevuGetir()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT hastanın_tc as 'Hasta T.C.No',hasta as 'Hasta Ad Soyad',klinik as Klinik ,doktor as Doktor,tarih as Tarih FROM Randevu", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        void muayeneListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT Hasta.tc_no AS 'Hasta T.C.No',Hasta.isim AS 'Ad Soyad',klinik AS Klinik,doktor AS Doktor,tarih as Tarih FROM Randevu,Hasta WHERE Hasta.tc_no=Randevu.hastanın_tc", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();
            dataGridView3.Update();
            dataGridView3.Refresh();
        }
        void receteListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT dr_no AS 'Doktor No',Doktor.isim As 'Doktor',hasta_tc AS 'Hasta T.C.No',hasta_isim AS 'Hasta Ad Soyad',detay AS Detay FROM Recete,Doktor WHERE Recete.dr_no=Doktor.d_no",baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView4.DataSource = tablo;
            baglanti.Close();
        }
        void ameliyatListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT Doktor.isim as 'Doktor',hasta_tc AS 'Hasta T.C.No',hasta_isim AS 'Hasta Ad Soyad',tarih AS Tarih,a_detay AS Detay FROM Ameliyatlar,Doktor WHERE Ameliyatlar.dr_no=Doktor.d_no", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView5.DataSource = tablo;
            baglanti.Close();
        }
        //void doktorBrans()
        //{
        //    baglanti.Open();
        //    SqlCommand komut = new SqlCommand("SELECT * FROM Doktor where brans=@brans", baglanti);
        //    komut.Parameters.AddWithValue("@brans", comboBox2.SelectedText);
        //    SqlDataReader dr;
        //    dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        comboBox3.Items.Add(dr["isim"]);
        //    }
        //    baglanti.Close();
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Hasta(isim,tc_no,telefon,cinsiyet,dog_tar,adres) values('" + textBox1.Text.ToString() + "' , '" + textBox3.Text + "', '" + maskedTextBox2.Text.ToString() + "', '" + comboBox1.Text + "', '" + maskedTextBox1.Text + "', '" + textBox4.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            hastaGetir();
            MessageBox.Show("Hasta Eklendi.");
            dataGridView2.Update();
            dataGridView2.Refresh();
        }
        private void SekreterSayfasi_Load(object sender, EventArgs e)
        {
            
            hastaGetir();
            RandevuGetir();
            muayeneListele();
            receteListele();
            ameliyatListele();
            textBox3.MaxLength = 11;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Randevu (hasta,klinik,doktor,tarih,hastanın_tc) VALUES ('" + textBox6.Text.ToString() + "' , '" + comboBox2.Text.ToString() + "', '" + comboBox3.Text + "', '" + maskedTextBox3.Text.ToString() + "','" + textBox2.Text +"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            RandevuGetir();
            MessageBox.Show("Randevu Oluşturuldu.");
            dataGridView1.Update();
            dataGridView1.Refresh();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Hasta SET isim = '" + textBox1.Text.ToString() + "' ,tc_no ='" + textBox3.Text + "',telefon ='" + maskedTextBox2.Text.ToString() + "',cinsiyet='" + comboBox1.Text + "',dog_tar='" + maskedTextBox1.Text + "',adres='" + textBox4.Text + "' WHERE tc_no=@tc_no", baglanti);
            komut.Parameters.AddWithValue("@tc_no", textBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            hastaGetir();
            MessageBox.Show("Hasta Güncellendi.");
            dataGridView2.Update();
            dataGridView2.Refresh();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            maskedTextBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }
        

      
        private void button4_Click(object sender, EventArgs e)
        {  
            baglanti.Open();
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (dataGridView1.CurrentRow.Index == 0)
                {
                    int sira;
                    sira = dataGridView1.SelectedRows[0].Index + 2;
                    SqlCommand komut = new SqlCommand("UPDATE Randevu SET hasta = '" + textBox6.Text.ToString() + "' ,klinik ='" + comboBox2.Text.ToString() + "',doktor ='" + comboBox3.Text + "',tarih='" + maskedTextBox3.Text + "' WHERE r_id=" + sira, baglanti);
                    komut.ExecuteNonQuery();
                }
                else
                {
                    int sira;
                    sira = dataGridView1.SelectedRows[0].Index + 2;
                    SqlCommand komut = new SqlCommand("UPDATE Randevu SET hasta = '" + textBox6.Text.ToString() + "' ,klinik ='" + comboBox2.Text.ToString() + "',doktor ='" + comboBox3.Text + "',tarih='" + maskedTextBox3.Text + "' WHERE r_id=" + sira, baglanti);
                    komut.ExecuteNonQuery();
                }

            }
            baglanti.Close();
            RandevuGetir();

            MessageBox.Show("Hasta Güncellendi.");
            dataGridView2.Update();
            dataGridView2.Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            comboBox3.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT * FROM Doktor where brans=@brans", baglanti);
            komut.Parameters.AddWithValue("@brans", comboBox2.Text);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["isim"]);
            }
            baglanti.Close();
        }
    }
    }

