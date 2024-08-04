using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace HastaneOto
{
    public partial class DoktorSayfasi : Form
    {
        public DoktorSayfasi()
        {
            InitializeComponent();
        }
            SqlConnection baglanti = new SqlConnection("server=.;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");
            SqlCommand komut;
            SqlDataAdapter da;

        void ameliyatListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT hasta_isim as 'Ad Soyad' , hasta_tc as 'T.C.No',tarih as Tarih,a_detay as 'Ameliyat Detayı' from Ameliyatlar where dr_no=@dr_no", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@dr_no", TxtAd.Text.ToString());
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView5.DataSource = tablo;
            baglanti.Close();
        }
        void muayeneListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT Hasta.tc_no AS 'Hasta T.C.No',Hasta.isim AS 'Ad Soyad',klinik AS Klinik,tarih as Tarih FROM Randevu,Hasta WHERE Hasta.tc_no=Randevu.hastanın_tc and doktor=@doktor", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@doktor", TxtSyd.Text.ToString());
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();
        }

        void receteyeNogetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Doktor where dtc_no=@dtc_no", baglanti);
            komut.Parameters.AddWithValue("@dtc_no", TxtYas.Text.ToString());
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox5.Text = dr["d_no"].ToString();
            }
            baglanti.Close();

        }
        void ameliyataNoGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Doktor where dtc_no=@dtc_no", baglanti);
            komut.Parameters.AddWithValue("@dtc_no", TxtYas.Text.ToString());
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr["d_no"].ToString();
            }
            baglanti.Close();
        }

        void doktorGoster()
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Doktor where dtc_no=@dtc_no", baglanti);
            komut.Parameters.AddWithValue("@dtc_no",TxtYas.Text.ToString());
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr["d_no"].ToString();
                TxtSyd.Text = dr["isim"].ToString();
                TxtYas.Text = dr["dtc_no"].ToString();
                CmbBxCnsyt.Text = dr["cinsiyet"].ToString();
                TxtHskd.Text = dr["brans"].ToString();
                MskTxtTelefon.Text = dr["telefon"].ToString();
                TxtMail.Text = dr["mail"].ToString();
                TxtSfre.Text = dr["sifre"].ToString();
            }
            baglanti.Close();
        }
        void receteGetir()
        {
            baglanti.Open();
            da = new SqlDataAdapter("SELECT dr_no as 'Doktor No' ,hasta_tc as 'Hasta T.C.No',hasta_isim as 'Ad Soyad',detay as 'Reçete Detayı' FROM Recete where dr_no=@dr_no", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@dr_no", TxtAd.Text.ToString());
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView4.DataSource = tablo;
            baglanti.Close();
        }
        private void btncikis_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }



        private void DoktorSayfasi_Load(object sender, EventArgs e)
        {
            ameliyatListele();
            doktorGoster();
            muayeneListele();
            receteyeNogetir();
            receteGetir();
            ameliyataNoGetir();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Ameliyatlar(dr_no,hasta_isim,hasta_tc,tarih,a_detay) values('" + textBox1.Text.ToString() + "' ,'" + textBox8.Text.ToString() + "' , '" + textBox6.Text + "', '" + maskedTextBox1.Text.ToString() + "', '" + textBox9.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Ameliyat Eklendi.");
            ameliyatListele();
            dataGridView5.Update();
            dataGridView5.Refresh();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //if (textBox6.Text == "") foreach (Control item in Controls) if (item is TextBox) item.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Hasta where tc_no like '"+textBox6.Text+ "'",baglanti);
            SqlDataReader read =komut.ExecuteReader();

            while (read.Read())
            {
                textBox8.Text = read["isim"].ToString();
            }
            baglanti.Close();
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Doktor SET isim = '" + TxtSyd.Text.ToString() + "' ,dtc_no ='" + TxtYas.Text + "',cinsiyet ='" + CmbBxCnsyt.Text.ToString() + "',brans='" + TxtHskd.Text + "',telefon='" + MskTxtTelefon.Text + "',mail='" + TxtMail.Text + "',sifre='" + TxtSfre.Text + "' WHERE dtc_no=@dtc_no", baglanti);
            komut.Parameters.AddWithValue("@dtc_no", TxtYas.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Bilgileriniz Güncellendi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Recete(dr_no,hasta_tc,hasta_isim,detay) values('" + textBox5.Text.ToString() + "' ,'" + textBox4.Text.ToString() + "' , '" + textBox3.Text + "', '" + textBox10.Text.ToString() + "')", baglanti);
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            receteGetir();
            MessageBox.Show("Reçete Eklendi.");
            dataGridView4.Update();
            dataGridView4.Refresh();

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Ameliyatlar where hasta_tc =@hasta_tc ", baglanti);
            komut.Parameters.AddWithValue("@hasta_tc", textBox6.Text.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            ameliyatListele();
            MessageBox.Show("Ameliyat Silindi.");
            dataGridView5.Update();
            dataGridView5.Refresh();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox6.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            maskedTextBox1.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString();
            textBox9.Text = dataGridView5.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            textBox10.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
        }


        
    }
}
