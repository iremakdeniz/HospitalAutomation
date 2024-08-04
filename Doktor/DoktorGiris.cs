using HastaneOto.Doktor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HastaneOto
{
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");
        SqlCommand komut;
        SqlDataReader dr;

        private void girisBtn_Click(object sender, EventArgs e)
        {
            string tc = textBox1.Text;
            string sifre = textBox2.Text;
            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM Doktor WHERE dtc_no='" + tc + "' AND sifre='" + sifre + "'";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                DoktorSayfasi fr = new DoktorSayfasi();
                fr.TxtYas.Text = textBox1.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya şifre");

            }

            baglanti.Close();
 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoktorKayit obj = new DoktorKayit();
            obj.Show();
            this.Hide();
                

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoktorSifreUnuttum obj = new DoktorSifreUnuttum();
            obj.Show();
            this.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void DoktorGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
