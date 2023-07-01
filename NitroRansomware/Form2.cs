using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        private Timer timer;

        public Form2()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            richTextBox1.Dock = DockStyle.Fill;
            this.TopMost = true;

            timer = new Timer();
            timer.Interval = 5000; // 5 seconds in milliseconds
            timer.Tick += TimerTick; // Fix the event handler name
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop(); // Stop the timer if you don't want it to repeat

            this.TopMost = false;
            Form3 form3 = new Form3();
            form3.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
