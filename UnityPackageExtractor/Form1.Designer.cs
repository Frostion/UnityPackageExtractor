
namespace UnityPackageExtractor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenPackage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExtractSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExtractAll = new System.Windows.Forms.ToolStripButton();
            this.treeView = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialogUnityPackage = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pictureBoxAssetThumbnail = new System.Windows.Forms.PictureBox();
            this.labelSelectedItem = new System.Windows.Forms.Label();
            this.backgroundWorkerExtractPackage = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAssetThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenPackage,
            this.toolStripSeparator1,
            this.toolStripButtonExtractSelected,
            this.toolStripButtonExtractAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(421, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpenPackage
            // 
            this.toolStripButtonOpenPackage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenPackage.Image")));
            this.toolStripButtonOpenPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenPackage.Name = "toolStripButtonOpenPackage";
            this.toolStripButtonOpenPackage.Size = new System.Drawing.Size(106, 22);
            this.toolStripButtonOpenPackage.Text = "Open Package";
            this.toolStripButtonOpenPackage.ToolTipText = "Select a Unity package to extract";
            this.toolStripButtonOpenPackage.Click += new System.EventHandler(this.toolStripButtonOpenPackage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonExtractSelected
            // 
            this.toolStripButtonExtractSelected.Enabled = false;
            this.toolStripButtonExtractSelected.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExtractSelected.Image")));
            this.toolStripButtonExtractSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExtractSelected.Name = "toolStripButtonExtractSelected";
            this.toolStripButtonExtractSelected.Size = new System.Drawing.Size(118, 22);
            this.toolStripButtonExtractSelected.Text = "Extract Selected";
            this.toolStripButtonExtractSelected.ToolTipText = "Extract just the selected asset or folder";
            this.toolStripButtonExtractSelected.Click += new System.EventHandler(this.toolStripButtonExtractSelected_Click);
            // 
            // toolStripButtonExtractAll
            // 
            this.toolStripButtonExtractAll.Enabled = false;
            this.toolStripButtonExtractAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExtractAll.Image")));
            this.toolStripButtonExtractAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExtractAll.Name = "toolStripButtonExtractAll";
            this.toolStripButtonExtractAll.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonExtractAll.Text = "Extract All";
            this.toolStripButtonExtractAll.ToolTipText = "Extract all assets to a folder";
            this.toolStripButtonExtractAll.Click += new System.EventHandler(this.toolStripButtonExtractAll_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(12, 28);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(397, 357);
            this.treeView.TabIndex = 1;
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(421, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(406, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "Drag a Unity package onto this window or click \"Open Package\"";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialogUnityPackage
            // 
            this.openFileDialogUnityPackage.Filter = "Unity packages (*.unitypackage)|*.unitypackage";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Select location to extract assets to";
            // 
            // pictureBoxAssetThumbnail
            // 
            this.pictureBoxAssetThumbnail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxAssetThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAssetThumbnail.Location = new System.Drawing.Point(12, 391);
            this.pictureBoxAssetThumbnail.Name = "pictureBoxAssetThumbnail";
            this.pictureBoxAssetThumbnail.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxAssetThumbnail.TabIndex = 3;
            this.pictureBoxAssetThumbnail.TabStop = false;
            this.pictureBoxAssetThumbnail.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxAssetThumbnail_Paint);
            // 
            // labelSelectedItem
            // 
            this.labelSelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSelectedItem.Location = new System.Drawing.Point(82, 391);
            this.labelSelectedItem.Name = "labelSelectedItem";
            this.labelSelectedItem.Size = new System.Drawing.Size(327, 64);
            this.labelSelectedItem.TabIndex = 4;
            this.labelSelectedItem.Text = "Unity Package Extractor\r\nby Frost Sheridan • frostion.github.io";
            this.labelSelectedItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundWorkerExtractPackage
            // 
            this.backgroundWorkerExtractPackage.WorkerReportsProgress = true;
            this.backgroundWorkerExtractPackage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExtractPackage_DoWork);
            this.backgroundWorkerExtractPackage.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerExtractPackage_ProgressChanged);
            this.backgroundWorkerExtractPackage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerExtractPackage_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 480);
            this.Controls.Add(this.labelSelectedItem);
            this.Controls.Add(this.pictureBoxAssetThumbnail);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(429, 517);
            this.Name = "Form1";
            this.Text = "Unity Package Extractor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAssetThumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenPackage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExtractSelected;
        private System.Windows.Forms.ToolStripButton toolStripButtonExtractAll;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialogUnityPackage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox pictureBoxAssetThumbnail;
        private System.Windows.Forms.Label labelSelectedItem;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExtractPackage;
    }
}

