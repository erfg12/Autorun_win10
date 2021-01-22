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
        List<string> NotReadyList = new List<string>();
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

                var RemovableDrives = DriveInfo.GetDrives().Where(drive => drive.DriveType == DriveType.Removable);
                var NonRemovableDrives = DriveInfo.GetDrives().Where(drive => drive.DriveType != DriveType.Removable);
                List<string> DrivesByName = new List<string>();

                foreach (var d in RemovableDrives)
                {
                    if (!DriveReady(d))
                        continue;

                    RemovableDrivesList.Invoke(
                        new Action(() =>
                        {
                            ListViewItem lvi = RemovableDrivesList.FindItemWithText(d.Name);
                            if (lvi == null)
                            {
                                bool FindINF = false;
                                if (d.RootDirectory.GetFiles("Autorun.inf").Count() > 0) FindINF = true;

                                string[] RowData = { d.VolumeLabel, d.DriveType.ToString(), d.DriveFormat, FindINF.ToString() };
                                RemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData);
                            }
                            DrivesByName.Add(d.Name); // used to detect if drive was removed
                        }
                    ));

                    if (!AlreadyReadDrives.ContainsKey(d.Name))
                    {
                        ReadAutoInfo(d.RootDirectory);
                        AlreadyReadDrives.Add(d.Name, d.VolumeLabel);
                    }
                }

                // remove drives from list that have been removed physically
                RemovableDrivesList.Invoke(
                        new Action(() =>
                        {
                            List<ListViewItem> Tmplvi = new List<ListViewItem>();
                            foreach (ListViewItem lvi in RemovableDrivesList.Items)
                            {
                                Tmplvi.Add(lvi);
                            }
                            foreach (ListViewItem lvi in Tmplvi)
                            {
                                if (!DrivesByName.Contains(lvi.Text)) // if our listview item is no longer connected, remove it
                                {
                                    AlreadyReadDrives.Remove(lvi.Text);
                                    RemovableDrivesList.FindItemWithText(lvi.Text).Remove();
                                }
                            }
                        })
                );

                foreach (var d in NonRemovableDrives)
                {
                    if (!DriveReady(d))
                        continue;

                    try
                    {
                        bool FindINF = false;
                        if (d.RootDirectory.GetFiles("Autorun.inf").Count() > 0) FindINF = true;

                        string[] RowData = { d.VolumeLabel, d.DriveType.ToString(), d.DriveFormat, FindINF.ToString() };

                        NonRemovableDrivesList.Invoke(
                            new Action(() => { if (NonRemovableDrivesList.FindItemWithText(d.Name) == null) NonRemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData); })
                        );
                    }
                    catch (Exception err)
                    {
                        //MessageBox.Show(err.ToString());
                        LogBox.Invoke(new Action(() => LogBox.Text += "Drive " + d.Name + err.ToString() + Environment.NewLine));
                    }

                    if (Properties.Settings.Default != null)
                    {
                        if (Properties.Settings.Default.NonRemovableToo)
                        {
                            if (!AlreadyReadDrives.ContainsKey(d.Name))
                            {
                                ReadAutoInfo(d.RootDirectory);
                                AlreadyReadDrives.Add(d.Name, d.VolumeLabel);
                            }
                        }
                    }
                }
                //FirstRun = false;
            }
        }

        private bool DriveReady(DriveInfo d)
        {
            if (!d.IsReady)
            {
                if (!NotReadyList.Contains(d.Name))
                {
                    LogBox.Invoke(new Action(() => LogBox.Text += "Drive " + d.Name + " is not ready" + Environment.NewLine));
                    NotReadyList.Add(d.Name);
                    AlreadyReadDrives.Remove(d.Name);

                    RemovableDrivesList.Invoke(
                            new Action(() =>
                            {
                                ListViewItem lvi = RemovableDrivesList.FindItemWithText(d.Name);
                                if (lvi != null)
                                    lvi.Remove();
                            })
                    );
                    NonRemovableDrivesList.Invoke(
                            new Action(() =>
                            {
                                ListViewItem lvi = NonRemovableDrivesList.FindItemWithText(d.Name);
                                if (lvi != null)
                                    lvi.Remove();
                            })
                    );
                }
                return false;
            }
            else
            {
                if (NotReadyList.Contains(d.Name)) NotReadyList.Remove(d.Name);
            }
            return true;
        }

        private void ReadAutoInfo(DirectoryInfo di)
        {
            // read and execute Autorun.info files
            foreach (var file in di.GetFiles("Autorun.inf"))
            {
                Debug.WriteLine("Found Autorun.info at:" + file.FullName);
                string[] lines = File.ReadAllLines(file.FullName);
                foreach (string line in lines)
                {
                    if (line.ToLower().Contains("open=") && !line.ToLower().Contains(";open="))
                    {
                        string exe = di + @"\" + line.Split('=')[1];
                        Debug.WriteLine("Starting EXE from open:" + exe);
                        try
                        {
                            Process.Start(exe);
                        }
                        catch
                        {
                            return; // drive wasnt ready, prevent crash
                        }
                    }
                    else if (line.ToLower().Contains("shellexecute=") && !line.ToLower().Contains(";shellexecute="))
                    {
                        string exe = di + @"\" + line.Split('=')[1];
                        Debug.WriteLine("Starting EXE from shellexecute:" + exe);
                        try
                        {
                            Process.Start(exe);
                        }
                        catch
                        {
                            return; // drive wasnt ready, prevent crash
                        }
                    }
                }
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default != null)
                nonremovabletoo.Checked = Properties.Settings.Default.NonRemovableToo;
        }

        private void nonremovabletoo_CheckedChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default != null)
            {
                Properties.Settings.Default.NonRemovableToo = nonremovabletoo.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void ReDetectDrives_Click(object sender, EventArgs e)
        {
            LogBox.Text += "Re-detecting drives." + Environment.NewLine;
            RemovableDrivesList.Items.Clear();
            NonRemovableDrivesList.Items.Clear();
            NotReadyList.Clear();
            AlreadyReadDrives.Clear();
        }
    }
}
