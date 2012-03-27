namespace CardPrint
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
            this.DBName = new System.Windows.Forms.TextBox();
            this.DBBrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DBSelect = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.destinationFolder = new System.Windows.Forms.TextBox();
            this.destinationFolderBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.thumbImageFolder = new System.Windows.Forms.TextBox();
            this.thumbImageFolderBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.faceImageFolder = new System.Windows.Forms.TextBox();
            this.faceImageFolderBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.readDBButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SchoolMonogramImageTextBox = new System.Windows.Forms.TextBox();
            this.SchoolMonogramBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DBName
            // 
            this.DBName.Location = new System.Drawing.Point(9, 42);
            this.DBName.Name = "DBName";
            this.DBName.ReadOnly = true;
            this.DBName.Size = new System.Drawing.Size(280, 20);
            this.DBName.TabIndex = 0;
            this.DBName.TextChanged += new System.EventHandler(this.DBName_TextChanged);
            // 
            // DBBrowseButton
            // 
            this.DBBrowseButton.Location = new System.Drawing.Point(295, 40);
            this.DBBrowseButton.Name = "DBBrowseButton";
            this.DBBrowseButton.Size = new System.Drawing.Size(53, 23);
            this.DBBrowseButton.TabIndex = 1;
            this.DBBrowseButton.Text = "Browse";
            this.DBBrowseButton.UseVisualStyleBackColor = true;
            this.DBBrowseButton.Click += new System.EventHandler(this.DBBrowseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DataBase";
            // 
            // DBSelect
            // 
            this.DBSelect.Filter = "Acces Database File (*.mdb) | *.mdb";
            this.DBSelect.Title = "Select Database";
            this.DBSelect.FileOk += new System.ComponentModel.CancelEventHandler(this.DBSelect_FileOk);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SchoolMonogramImageTextBox);
            this.groupBox1.Controls.Add(this.SchoolMonogramBrowse);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.destinationFolder);
            this.groupBox1.Controls.Add(this.destinationFolderBrowse);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.thumbImageFolder);
            this.groupBox1.Controls.Add(this.thumbImageFolderBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.faceImageFolder);
            this.groupBox1.Controls.Add(this.faceImageFolderBrowse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DBName);
            this.groupBox1.Controls.Add(this.DBBrowseButton);
            this.groupBox1.Location = new System.Drawing.Point(23, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 295);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files and Folders";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Destination Folder";
            // 
            // destinationFolder
            // 
            this.destinationFolder.Location = new System.Drawing.Point(9, 191);
            this.destinationFolder.Name = "destinationFolder";
            this.destinationFolder.ReadOnly = true;
            this.destinationFolder.Size = new System.Drawing.Size(280, 20);
            this.destinationFolder.TabIndex = 9;
            // 
            // destinationFolderBrowse
            // 
            this.destinationFolderBrowse.Location = new System.Drawing.Point(295, 189);
            this.destinationFolderBrowse.Name = "destinationFolderBrowse";
            this.destinationFolderBrowse.Size = new System.Drawing.Size(53, 23);
            this.destinationFolderBrowse.TabIndex = 10;
            this.destinationFolderBrowse.Text = "Browse";
            this.destinationFolderBrowse.UseVisualStyleBackColor = true;
            this.destinationFolderBrowse.Click += new System.EventHandler(this.destinationFolderBrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Thumb Image Folder";
            // 
            // thumbImageFolder
            // 
            this.thumbImageFolder.Location = new System.Drawing.Point(9, 140);
            this.thumbImageFolder.Name = "thumbImageFolder";
            this.thumbImageFolder.ReadOnly = true;
            this.thumbImageFolder.Size = new System.Drawing.Size(280, 20);
            this.thumbImageFolder.TabIndex = 6;
            // 
            // thumbImageFolderBrowse
            // 
            this.thumbImageFolderBrowse.Location = new System.Drawing.Point(295, 138);
            this.thumbImageFolderBrowse.Name = "thumbImageFolderBrowse";
            this.thumbImageFolderBrowse.Size = new System.Drawing.Size(53, 23);
            this.thumbImageFolderBrowse.TabIndex = 7;
            this.thumbImageFolderBrowse.Text = "Browse";
            this.thumbImageFolderBrowse.UseVisualStyleBackColor = true;
            this.thumbImageFolderBrowse.Click += new System.EventHandler(this.thumbImageFolderBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Face Image Folder";
            // 
            // faceImageFolder
            // 
            this.faceImageFolder.Location = new System.Drawing.Point(9, 92);
            this.faceImageFolder.Name = "faceImageFolder";
            this.faceImageFolder.ReadOnly = true;
            this.faceImageFolder.Size = new System.Drawing.Size(280, 20);
            this.faceImageFolder.TabIndex = 3;
            this.faceImageFolder.TextChanged += new System.EventHandler(this.faceImageFolder_TextChanged);
            // 
            // faceImageFolderBrowse
            // 
            this.faceImageFolderBrowse.Location = new System.Drawing.Point(295, 90);
            this.faceImageFolderBrowse.Name = "faceImageFolderBrowse";
            this.faceImageFolderBrowse.Size = new System.Drawing.Size(53, 23);
            this.faceImageFolderBrowse.TabIndex = 4;
            this.faceImageFolderBrowse.Text = "Browse";
            this.faceImageFolderBrowse.UseVisualStyleBackColor = true;
            this.faceImageFolderBrowse.Click += new System.EventHandler(this.faceImageFolderBrowse_Click);
            // 
            // readDBButton
            // 
            this.readDBButton.Location = new System.Drawing.Point(23, 347);
            this.readDBButton.Name = "readDBButton";
            this.readDBButton.Size = new System.Drawing.Size(136, 23);
            this.readDBButton.TabIndex = 4;
            this.readDBButton.Text = "Read Database";
            this.readDBButton.UseVisualStyleBackColor = true;
            this.readDBButton.Click += new System.EventHandler(this.readDBButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "School Monogram Image";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // SchoolMonogramImageTextBox
            // 
            this.SchoolMonogramImageTextBox.Location = new System.Drawing.Point(9, 241);
            this.SchoolMonogramImageTextBox.Name = "SchoolMonogramImageTextBox";
            this.SchoolMonogramImageTextBox.ReadOnly = true;
            this.SchoolMonogramImageTextBox.Size = new System.Drawing.Size(280, 20);
            this.SchoolMonogramImageTextBox.TabIndex = 12;
            // 
            // SchoolMonogramBrowse
            // 
            this.SchoolMonogramBrowse.Location = new System.Drawing.Point(295, 239);
            this.SchoolMonogramBrowse.Name = "SchoolMonogramBrowse";
            this.SchoolMonogramBrowse.Size = new System.Drawing.Size(53, 23);
            this.SchoolMonogramBrowse.TabIndex = 13;
            this.SchoolMonogramBrowse.Text = "Browse";
            this.SchoolMonogramBrowse.UseVisualStyleBackColor = true;
            this.SchoolMonogramBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            this.openFileDialog1.Title = "Select Database";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 424);
            this.Controls.Add(this.readDBButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox DBName;
        private System.Windows.Forms.Button DBBrowseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog DBSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox thumbImageFolder;
        private System.Windows.Forms.Button thumbImageFolderBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox faceImageFolder;
        private System.Windows.Forms.Button faceImageFolderBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox destinationFolder;
        private System.Windows.Forms.Button destinationFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button readDBButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SchoolMonogramImageTextBox;
        private System.Windows.Forms.Button SchoolMonogramBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

