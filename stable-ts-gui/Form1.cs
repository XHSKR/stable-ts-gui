using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stable_ts_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "Japanese";
            comboBox2.Text = "medium"; this.AllowDrop = true;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Resize += new EventHandler(Form1_Resize);
        }

        List<string> filePaths = new List<string>();
        string directoryPath, baseFileName;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string language = "";
            if (comboBox1.Text == "Japanese") language = "ja";
            if (comboBox1.Text == "Korean") language = "ko";
            if (comboBox1.Text == "English") language = "en";

            foreach (string filePath in filePaths)
            {
                directoryPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileName(filePath);
                baseFileName = Path.GetFileNameWithoutExtension(fileName);

                // Check if .srt file exists
                string srtFilePath = Path.Combine(directoryPath, $"{baseFileName}.srt");
                if (File.Exists(srtFilePath))
                {
                    // Prompt the user to choose whether to override or cancel
                    DialogResult result = MessageBox.Show("The .srt file already exists. Do you want to override it?", "File Exists", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Delete existing .srt file
                        File.Delete(srtFilePath);
                    }
                    else
                    {
                        // User chose to cancel the task or not override the file
                        continue;
                    }
                }

                string changeDirectoryCommand = $"cd /d \"{directoryPath}\"";
                string stableTsCommand;
                if (comboBox1.Text == "Japanese")
                 stableTsCommand = $"stable-ts --model {comboBox2.Text} --language {language} \"{fileName}\" -o \"{baseFileName}.srt\" --task translate";
                else
                    stableTsCommand = $"stable-ts --model {comboBox2.Text} --language {language} \"{fileName}\" -o \"{baseFileName}.srt\"";

                // Create a new ProcessStartInfo object
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c \"" + changeDirectoryCommand + " && " + stableTsCommand + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                // Create a new process and start it
                Process process = new Process
                {
                    StartInfo = processStartInfo
                };
                process.Start();

                // Wait for the process to finish
                process.WaitForExit();

                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear the file paths list
            filePaths.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePaths.AddRange(openFileDialog.FileNames);
                directoryPath = Path.GetDirectoryName(filePaths[0]);
                baseFileName = Path.GetFileNameWithoutExtension(filePaths[0]);

                label3.Text = $"Selected files: {filePaths.Count}";

                // Enable button2 when files are selected
                button2.Enabled = true;
            }
        }
    }
}
