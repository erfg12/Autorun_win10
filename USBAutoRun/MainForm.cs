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

namespace USBAutoRun
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Dictionary<string, string> AlreadyReadDrives = new Dictionary<string, string>();
        bool FirstRun = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("bg worker started");
            while (true)
            {
                var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
                var NotReadyDrives = DriveInfo.GetDrives().Where(drive => !drive.IsReady && drive.DriveType == DriveType.Removable);
                foreach (var d in drives)
                {
                    // if we already read the drive, skip it.
                    if (AlreadyReadDrives.ContainsKey(d.Name)) continue;

                    Debug.WriteLine("Adding drive:" + d.Name);
                    AlreadyReadDrives.Add(d.Name, d.VolumeLabel);

                    // prevent exe launch on first run
                    if (FirstRun) { Debug.WriteLine("FirstRun"); continue; }

                    // read and execute Autorun.info files
                    foreach (var file in d.RootDirectory.GetFiles("Autorun.inf"))
                    {
                        Debug.WriteLine("Found Autorun.info at:" + file.FullName);
                        string[] lines = File.ReadAllLines(file.FullName);
                        foreach (string line in lines)
                        {
                            if (line.ToLower().Contains("open=") && !line.ToLower().Contains(";open="))
                            {
                                string exe = d.RootDirectory + @"\" + line.Split('=')[1];
                                Debug.WriteLine("Starting EXE from open:" + exe);
                                if (d.IsReady)
                                    Process.Start(exe);
                            }
                            else if (line.ToLower().Contains("shellexecute=") && !line.ToLower().Contains(";shellexecute="))
                            {
                                string exe = d.RootDirectory + @"\" + line.Split('=')[1];
                                Debug.WriteLine("Starting EXE from shellexecute:" + exe);
                                if (d.IsReady)
                                    Process.Start(exe);
                            }
                        }
                    }
                }

                // remove all non-ready devices (like an SD card that got removed)
                foreach (var d in NotReadyDrives)
                {
                    if (AlreadyReadDrives.ContainsKey(d.Name))
                    {
                        AlreadyReadDrives.Remove(d.Name);
                        Debug.WriteLine("Removing drive:" + d.Name);
                    }
                }
                FirstRun = false;
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}
