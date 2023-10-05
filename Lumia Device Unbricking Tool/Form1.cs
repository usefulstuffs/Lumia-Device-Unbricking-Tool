using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lumia_Device_Unbricking_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog thor2ofd = new OpenFileDialog();
            thor2ofd.CheckFileExists = true;
            thor2ofd.Filter = "THOR2 executable file|thor2.exe";
            thor2ofd.Multiselect = false;
            thor2ofd.Title = "Select thor2.exe file";
            if (thor2ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = thor2ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog hexofd = new OpenFileDialog();
            hexofd.CheckFileExists = true;
            hexofd.Filter = "Hexadecimal recovery file|*.hex";
            hexofd.Multiselect = false;
            hexofd.Title = "Select hexadecimal recovery file";
            if (hexofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = hexofd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog thor2ofd = new OpenFileDialog();
            thor2ofd.CheckFileExists = true;
            thor2ofd.Filter = "msimage.mbn|*.mbn";
            thor2ofd.Multiselect = false;
            thor2ofd.Title = "Select msimage.mbn recovery file";
            if (thor2ofd.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = thor2ofd.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog thor2ofd = new OpenFileDialog();
            thor2ofd.CheckFileExists = true;
            thor2ofd.Filter = "Firmware flash file|*.ffu";
            thor2ofd.Multiselect = false;
            thor2ofd.Title = "Select firmware flash file";
            if (thor2ofd.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = thor2ofd.FileName;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://lumiafirmware.com");
            }
            catch
            {
                MessageBox.Show("We cannot open the browser. To gather firmware files, copy the link below in your browser:\n\nlumiafirmware.com","Lumia Device Unbricking Tool",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            const string quote = "\"";
            if (textBox1.Text == "" || !File.Exists(textBox1.Text))
            {
                MessageBox.Show("The path to thor2.exe field cannot be empty or is invalid.","Lumia Device Unbricking Tool",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "" || !File.Exists(textBox2.Text))
            {
                MessageBox.Show("The hexadecimal recovery file field cannot be empty or is invalid.", "Lumia Device Unbricking Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox3.Text == "" || !File.Exists(textBox3.Text))
            {
                MessageBox.Show("The msimage.mbn file for recovery field cannot be empty or is invalid.", "Lumia Device Unbricking Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox4.Text == "" || !File.Exists(textBox4.Text))
            {
                MessageBox.Show("The firmware flash (.ffu) file field cannot be empty or is invalid.", "Lumia Device Unbricking Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Flashing will start, don't disconnect Lumia during this procedure. Disconnecting may cause the permanent brick of the device.","Lumia Device Unbricking Tool - Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                // For debugging the commands it's going to run:
                // File.WriteAllText(@"C:\LDUT_debug.txt", $@"{quote}{textBox1.Text}{quote} -mode emergency -hexfile {quote}{textBox2.Text}{quote} -mbnfile {quote}{textBox3.Text}{quote} -ffufile {quote}{textBox4.Text}{quote}");
                Process.Start(textBox1.Text,$@"-mode emergency -hexfile {quote}{textBox2.Text}{quote} -mbnfile {quote}{textBox3.Text}{quote} -ffufile {quote}{textBox4.Text}{quote}").WaitForExit();
                MessageBox.Show("Flashing finished. Check now your Lumia's screen.\n\nIf it's in a Green screen, forcedly reboot it by pressing and holding volume down+power button.\n\nIf it's still in a red screen, try again.", "Lumia Device Unbricking Tool - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show($@"How to use this tool:{Environment.NewLine}{Environment.NewLine}Path to thor2.exe - Install Windows Device Recovery Tool and search in its installation folder (usually C:\Program Files (x86)\Microsoft Care Suite\Windows Device Recovery Tool).{Environment.NewLine}{Environment.NewLine}Hexadecimal (.hex) file for recovery - The .hex file you get from lumiafirmware.com under the emergency files section.{Environment.NewLine}{Environment.NewLine}msimage.mbn file for recovery - You get this file from lumiafirmware.com under the emergency files section{Environment.NewLine}{Environment.NewLine}Firmware flash (.ffu) file - This is the firmware file. You can get it from both lumiafirmware.com and Windows Device Recovery Tool cache from a previous update.","Lumia Device Unbricking Tool - Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
