using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace OOP25
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mail.google.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ant04ch");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
