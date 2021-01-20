namespace USBAutoRun
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.RemovableDrivesList = new System.Windows.Forms.ListView();
            this.DriveName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Format = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NonRemovableDrivesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nonremovabletoo = new System.Windows.Forms.CheckBox();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.ReDetectDrives = new System.Windows.Forms.Button();
            this.Inf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // RemovableDrivesList
            // 
            this.RemovableDrivesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DriveName,
            this.Label,
            this.Type,
            this.Format,
            this.Inf});
            this.RemovableDrivesList.HideSelection = false;
            this.RemovableDrivesList.Location = new System.Drawing.Point(8, 19);
            this.RemovableDrivesList.Name = "RemovableDrivesList";
            this.RemovableDrivesList.Size = new System.Drawing.Size(382, 113);
            this.RemovableDrivesList.TabIndex = 0;
            this.RemovableDrivesList.UseCompatibleStateImageBehavior = false;
            this.RemovableDrivesList.View = System.Windows.Forms.View.Details;
            // 
            // DriveName
            // 
            this.DriveName.Text = "Name";
            // 
            // Label
            // 
            this.Label.Text = "Label";
            this.Label.Width = 131;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // Format
            // 
            this.Format.Text = "Format";
            // 
            // NonRemovableDrivesList
            // 
            this.NonRemovableDrivesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.NonRemovableDrivesList.HideSelection = false;
            this.NonRemovableDrivesList.Location = new System.Drawing.Point(6, 19);
            this.NonRemovableDrivesList.Name = "NonRemovableDrivesList";
            this.NonRemovableDrivesList.Size = new System.Drawing.Size(384, 113);
            this.NonRemovableDrivesList.TabIndex = 1;
            this.NonRemovableDrivesList.UseCompatibleStateImageBehavior = false;
            this.NonRemovableDrivesList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Label";
            this.columnHeader2.Width = 137;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Format";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RemovableDrivesList);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Removable Drives";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NonRemovableDrivesList);
            this.groupBox2.Location = new System.Drawing.Point(12, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 141);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Non-Removable Drives";
            // 
            // nonremovabletoo
            // 
            this.nonremovabletoo.AutoSize = true;
            this.nonremovabletoo.Location = new System.Drawing.Point(20, 13);
            this.nonremovabletoo.Name = "nonremovabletoo";
            this.nonremovabletoo.Size = new System.Drawing.Size(205, 17);
            this.nonremovabletoo.TabIndex = 4;
            this.nonremovabletoo.Text = "Detect In Non-Removable Drives Too";
            this.nonremovabletoo.UseVisualStyleBackColor = true;
            this.nonremovabletoo.CheckedChanged += new System.EventHandler(this.nonremovabletoo_CheckedChanged);
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(13, 329);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(397, 118);
            this.LogBox.TabIndex = 5;
            // 
            // ReDetectDrives
            // 
            this.ReDetectDrives.Location = new System.Drawing.Point(310, 9);
            this.ReDetectDrives.Name = "ReDetectDrives";
            this.ReDetectDrives.Size = new System.Drawing.Size(100, 23);
            this.ReDetectDrives.TabIndex = 6;
            this.ReDetectDrives.Text = "Re-Detect Drives";
            this.ReDetectDrives.UseVisualStyleBackColor = true;
            this.ReDetectDrives.Click += new System.EventHandler(this.ReDetectDrives_Click);
            // 
            // Inf
            // 
            this.Inf.Text = "Inf";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Inf";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 459);
            this.Controls.Add(this.ReDetectDrives);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.nonremovabletoo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Autorun";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ListView RemovableDrivesList;
        private System.Windows.Forms.ColumnHeader DriveName;
        private System.Windows.Forms.ColumnHeader Label;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Format;
        private System.Windows.Forms.ListView NonRemovableDrivesList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox nonremovabletoo;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.Button ReDetectDrives;
        private System.Windows.Forms.ColumnHeader Inf;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

