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
    public partial class DoktorKayit : Form
    {
        public DoktorKayit()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        SqlDataAdapter da;
        SqlConnection baglanti = new SqlConnection("server=.;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            DoktorGiris obj = new DoktorGiris();
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            if(TxtAd.Text == " " || TxtTc.Text == ""|| CmbBxCnsyt.Text== "" || comboBox1.Text == "" || MskTxtTelefon.Text == ""|| TxtMail.Text=="" || TxtSfre.Text== "")
            {
                MessageBox.Show("Kayıt olmak için boş alanları doldurunuz !");
            }
            else
            {
                
                SqlCommand komut = new SqlCommand("INSERT INTO Doktor (isim,dtc_no,cinsiyet,brans,telefon,mail,sifre) VALUES ('" + TxtAd.Text.ToString() + "' , '" + TxtTc.Text.ToString() + "', '" + CmbBxCnsyt.Text + "', '" + comboBox1.Text.ToString() + "','" + MskTxtTelefon.Text + "','" + TxtMail.Text + "','" + TxtSfre.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kaydınız başarıyla oluşturulmuştur.");
            }
            baglanti.Close();
        }

        private void TxtSfre_TextChanged(object sender, EventArgs e)
        {
            TxtSfre.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                TxtSfre.PasswordChar = '\0';
            }
            else
            {
                TxtSfre.PasswordChar = '*';
            }

        }

        private void DoktorKayit_Load(object sender, EventArgs e)
        {
            TxtTc.MaxLength = 11;
        }
    }
}
