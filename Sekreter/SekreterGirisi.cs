
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HastaneOto
{
    public partial class SekreterGirisi : Form
    {
        public SekreterGirisi()
        {
            InitializeComponent();
        }

        private void girisBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Eksik Veri");
            }
            else if (textBox1.Text == "admin" || textBox2.Text == "123")
            {
                SekreterSayfasi Obj = new SekreterSayfasi();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya şifre");
            }
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

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
