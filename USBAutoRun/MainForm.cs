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
        //bool FirstRun = true;

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
                System.Threading.Thread.Sleep(500); // maybe make this a customizable setting

                var RemovableDrives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
                var NotReadyDrives = DriveInfo.GetDrives().Where(drive => !drive.IsReady && drive.DriveType == DriveType.Removable);
                var NonRemovableDrives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType != DriveType.Removable);
                var NotReadyNonRemovableDrives = DriveInfo.GetDrives().Where(drive => !drive.IsReady && drive.DriveType != DriveType.Removable);

                foreach (var d in RemovableDrives)
                {
                    if (AlreadyReadDrives.ContainsKey(d.Name)) continue;
                    Debug.WriteLine("Adding drive " + d.Name);

                    string[] RowData = { d.VolumeLabel, d.DriveType.ToString(), d.IsReady.ToString(), d.DriveFormat };

                    if (RemovableDrivesList.InvokeRequired)
                    {
                        RemovableDrivesList.Invoke(
                            new Action(() => RemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData))
                        );
                    };

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
                                try {
                                    Process.Start(exe);
                                } catch {
                                    return; // drive wasnt ready, prevent crash
                                }
                            }
                            else if (line.ToLower().Contains("shellexecute=") && !line.ToLower().Contains(";shellexecute="))
                            {
                                string exe = d.RootDirectory + @"\" + line.Split('=')[1];
                                Debug.WriteLine("Starting EXE from shellexecute:" + exe);
                                try {
                                    Process.Start(exe);
                                } catch {
                                    return; // drive wasnt ready, prevent crash
                                }
                            }
                        }
                    }
                    AlreadyReadDrives.Add(d.Name, d.VolumeLabel);
                }

                foreach (var d in NonRemovableDrives)
                {
                    System.Threading.Thread.Sleep(3000); // make this a setting

                    string[] RowData = { d.VolumeLabel, d.DriveType.ToString(), d.IsReady.ToString(), d.DriveFormat };

                    if (NonRemovableDrivesList.InvokeRequired)
                    {
                        NonRemovableDrivesList.Invoke(
                            new Action(() => { if (NonRemovableDrivesList.FindItemWithText(d.Name) == null) NonRemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData); })
                        );
                    };
                }

                // remove all non-ready devices (like an SD card that got removed)
                foreach (var d in NotReadyDrives)
                {
                    if (AlreadyReadDrives.ContainsKey(d.Name))
                    {
                        AlreadyReadDrives.Remove(d.Name);
                        Debug.WriteLine("Removing drive:" + d.Name);
                        if (RemovableDrivesList.InvokeRequired)
                        {
                            RemovableDrivesList.Invoke(
                                new Action(() => {
                                    RemovableDrivesList.FindItemWithText(d.Name).Remove(); 
                                })
                            );
                        }
                    }
                }
                //FirstRun = false;
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
