using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneOto.Doktor
{
    public partial class DoktorSifreUnuttum : Form
    {
        public DoktorSifreUnuttum()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        SqlDataAdapter da;
        SqlConnection baglanti = new SqlConnection("server=.;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");

        private void girisBtn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (textBox5.Text == "" || textBox4.Text == "" || textBox3.Text == "" ) {
                MessageBox.Show("Lütfen boş alanları doldurunuz !");
            }
            else
            {
                SqlCommand komut = new SqlCommand("UPDATE Doktor SET sifre = '" + textBox3.Text.ToString() + "' WHERE dtc_no=@dtc_no", baglanti);
                komut.Parameters.AddWithValue("@dtc_no", textBox5.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Şifreniz Yenilendi.");
            }
            baglanti.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoktorGiris obj = new DoktorGiris();
            obj.Show();
            this.Hide();
        }

        private void DoktorSifreUnuttum_Load(object sender, EventArgs e)
        {
            textBox5.MaxLength = 11;
        }
    }
}
