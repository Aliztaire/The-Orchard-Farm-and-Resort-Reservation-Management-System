namespace OrchardFarmRMS
{
    partial class PackageConf
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            AddPackageBtn = new Button();
            DelPackageBtn = new Button();
            EditPackageBtn = new Button();
            searchBox = new TextBox();
            ReservationLbl = new Label();
            dataGridView1 = new DataGridView();
            addPackageTab = new TabPage();
            addHeaderLabel = new Label();
            panelInfo = new Panel();
            panelTitle = new Label();
            lblNameAdd = new Label();
            txtNameAdd = new TextBox();
            lblPriceAdd = new Label();
            txtPriceAdd = new TextBox();
            lblMaxGuestsAdd = new Label();
            numMaxGuestsAdd = new NumericUpDown();
            lblActiveAdd = new Label();
            chkActiveAdd = new CheckBox();
            lblDetailsAdd = new Label();
            txtDetailsAdd = new TextBox();
            btnCancelAdd = new Button();
            btnConfirmAdd = new Button();
            panel1 = new Panel();
            button11 = new Button();
            tab1Btn = new Button();
            pictureBox2 = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            addPackageTab.SuspendLayout();
            panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxGuestsAdd).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(addPackageTab);
            tabControl1.Location = new Point(237, 68);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1424, 706);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackgroundImage = Properties.Resources.ReservationsBG;
            tabPage1.BackgroundImageLayout = ImageLayout.Stretch;
            tabPage1.Controls.Add(AddPackageBtn);
            tabPage1.Controls.Add(DelPackageBtn);
            tabPage1.Controls.Add(EditPackageBtn);
            tabPage1.Controls.Add(searchBox);
            tabPage1.Controls.Add(ReservationLbl);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(1416, 678);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Package Configuration";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // AddPackageBtn
            // 
            AddPackageBtn.BackColor = Color.FromArgb(37, 105, 44);
            AddPackageBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            AddPackageBtn.FlatAppearance.BorderSize = 0;
            AddPackageBtn.FlatStyle = FlatStyle.Flat;
            AddPackageBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddPackageBtn.ForeColor = SystemColors.Control;
            AddPackageBtn.Location = new Point(49, 580);
            AddPackageBtn.Margin = new Padding(3, 2, 3, 2);
            AddPackageBtn.Name = "AddPackageBtn";
            AddPackageBtn.Size = new Size(407, 38);
            AddPackageBtn.TabIndex = 7;
            AddPackageBtn.Text = "Add Package";
            AddPackageBtn.UseVisualStyleBackColor = false;
            AddPackageBtn.Click += AddPackageBtn_Click;
            // 
            // DelPackageBtn
            // 
            DelPackageBtn.BackColor = Color.FromArgb(154, 35, 58);
            DelPackageBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            DelPackageBtn.FlatAppearance.BorderSize = 0;
            DelPackageBtn.FlatStyle = FlatStyle.Flat;
            DelPackageBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DelPackageBtn.ForeColor = SystemColors.ButtonHighlight;
            DelPackageBtn.Location = new Point(946, 580);
            DelPackageBtn.Margin = new Padding(3, 2, 3, 2);
            DelPackageBtn.Name = "DelPackageBtn";
            DelPackageBtn.Size = new Size(407, 38);
            DelPackageBtn.TabIndex = 6;
            DelPackageBtn.Text = "Delete Package";
            DelPackageBtn.UseVisualStyleBackColor = false;
            DelPackageBtn.Click += DelPackageBtn_Click;
            // 
            // EditPackageBtn
            // 
            EditPackageBtn.BackColor = Color.FromArgb(60, 62, 128);
            EditPackageBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            EditPackageBtn.FlatAppearance.BorderSize = 0;
            EditPackageBtn.FlatStyle = FlatStyle.Flat;
            EditPackageBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EditPackageBtn.ForeColor = SystemColors.Control;
            EditPackageBtn.Location = new Point(496, 580);
            EditPackageBtn.Margin = new Padding(3, 2, 3, 2);
            EditPackageBtn.Name = "EditPackageBtn";
            EditPackageBtn.Size = new Size(407, 38);
            EditPackageBtn.TabIndex = 5;
            EditPackageBtn.Text = "Edit Package";
            EditPackageBtn.UseVisualStyleBackColor = false;
            EditPackageBtn.Click += EditPackageBtn_Click;
            // 
            // searchBox
            // 
            searchBox.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchBox.Location = new Point(122, 107);
            searchBox.Margin = new Padding(3, 2, 3, 2);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(1232, 32);
            searchBox.TabIndex = 2;
            // 
            // ReservationLbl
            // 
            ReservationLbl.AutoSize = true;
            ReservationLbl.Font = new Font("Microsoft Sans Serif", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReservationLbl.Location = new Point(543, 51);
            ReservationLbl.Name = "ReservationLbl";
            ReservationLbl.Size = new Size(424, 44);
            ReservationLbl.TabIndex = 1;
            ReservationLbl.Text = "Package Configuration";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(49, 175);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1304, 381);
            dataGridView1.TabIndex = 0;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
            // 
            // addPackageTab
            // 
            addPackageTab.BackgroundImage = Properties.Resources.ReservationsBG;
            addPackageTab.BackgroundImageLayout = ImageLayout.Stretch;
            addPackageTab.Controls.Add(addHeaderLabel);
            addPackageTab.Controls.Add(panelInfo);
            addPackageTab.Controls.Add(btnCancelAdd);
            addPackageTab.Controls.Add(btnConfirmAdd);
            addPackageTab.Location = new Point(4, 24);
            addPackageTab.Margin = new Padding(3, 2, 3, 2);
            addPackageTab.Name = "addPackageTab";
            addPackageTab.Padding = new Padding(3, 2, 3, 2);
            addPackageTab.Size = new Size(1416, 678);
            addPackageTab.TabIndex = 1;
            addPackageTab.Text = "Add Package";
            addPackageTab.UseVisualStyleBackColor = true;
            // 
            // addHeaderLabel
            // 
            addHeaderLabel.AutoSize = true;
            addHeaderLabel.BackColor = Color.Transparent;
            addHeaderLabel.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
            addHeaderLabel.ForeColor = SystemColors.ActiveCaptionText;
            addHeaderLabel.Location = new Point(35, 22);
            addHeaderLabel.Name = "addHeaderLabel";
            addHeaderLabel.Size = new Size(219, 37);
            addHeaderLabel.TabIndex = 0;
            addHeaderLabel.Text = "Add Package";
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.Transparent;
            panelInfo.BorderStyle = BorderStyle.FixedSingle;
            panelInfo.Controls.Add(panelTitle);
            panelInfo.Controls.Add(lblNameAdd);
            panelInfo.Controls.Add(txtNameAdd);
            panelInfo.Controls.Add(lblPriceAdd);
            panelInfo.Controls.Add(txtPriceAdd);
            panelInfo.Controls.Add(lblMaxGuestsAdd);
            panelInfo.Controls.Add(numMaxGuestsAdd);
            panelInfo.Controls.Add(lblActiveAdd);
            panelInfo.Controls.Add(chkActiveAdd);
            panelInfo.Controls.Add(lblDetailsAdd);
            panelInfo.Controls.Add(txtDetailsAdd);
            panelInfo.ForeColor = SystemColors.ActiveCaptionText;
            panelInfo.Location = new Point(40, 112);
            panelInfo.Margin = new Padding(3, 2, 3, 2);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(1252, 270);
            panelInfo.TabIndex = 1;
            // 
            // panelTitle
            // 
            panelTitle.AutoSize = true;
            panelTitle.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            panelTitle.ForeColor = SystemColors.ActiveCaptionText;
            panelTitle.Location = new Point(10, 6);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(202, 29);
            panelTitle.TabIndex = 0;
            panelTitle.Text = "Package Details";
            // 
            // lblNameAdd
            // 
            lblNameAdd.AutoSize = true;
            lblNameAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            lblNameAdd.ForeColor = SystemColors.ActiveCaptionText;
            lblNameAdd.Location = new Point(26, 42);
            lblNameAdd.Name = "lblNameAdd";
            lblNameAdd.Size = new Size(151, 24);
            lblNameAdd.TabIndex = 1;
            lblNameAdd.Text = "Package Name";
            // 
            // txtNameAdd
            // 
            txtNameAdd.Font = new Font("Microsoft Sans Serif", 13.8F);
            txtNameAdd.Location = new Point(459, 39);
            txtNameAdd.Margin = new Padding(3, 2, 3, 2);
            txtNameAdd.Name = "txtNameAdd";
            txtNameAdd.Size = new Size(613, 28);
            txtNameAdd.TabIndex = 2;
            // 
            // lblPriceAdd
            // 
            lblPriceAdd.AutoSize = true;
            lblPriceAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            lblPriceAdd.ForeColor = SystemColors.ActiveCaptionText;
            lblPriceAdd.Location = new Point(26, 80);
            lblPriceAdd.Name = "lblPriceAdd";
            lblPriceAdd.Size = new Size(58, 24);
            lblPriceAdd.TabIndex = 3;
            lblPriceAdd.Text = "Price";
            // 
            // txtPriceAdd
            // 
            txtPriceAdd.Font = new Font("Microsoft Sans Serif", 13.8F);
            txtPriceAdd.Location = new Point(459, 76);
            txtPriceAdd.Margin = new Padding(3, 2, 3, 2);
            txtPriceAdd.Name = "txtPriceAdd";
            txtPriceAdd.Size = new Size(210, 28);
            txtPriceAdd.TabIndex = 4;
            // 
            // lblMaxGuestsAdd
            // 
            lblMaxGuestsAdd.AutoSize = true;
            lblMaxGuestsAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            lblMaxGuestsAdd.ForeColor = SystemColors.ActiveCaptionText;
            lblMaxGuestsAdd.Location = new Point(26, 117);
            lblMaxGuestsAdd.Name = "lblMaxGuestsAdd";
            lblMaxGuestsAdd.Size = new Size(119, 24);
            lblMaxGuestsAdd.TabIndex = 5;
            lblMaxGuestsAdd.Text = "Max Guests";
            // 
            // numMaxGuestsAdd
            // 
            numMaxGuestsAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            numMaxGuestsAdd.Location = new Point(459, 114);
            numMaxGuestsAdd.Margin = new Padding(3, 2, 3, 2);
            numMaxGuestsAdd.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numMaxGuestsAdd.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxGuestsAdd.Name = "numMaxGuestsAdd";
            numMaxGuestsAdd.Size = new Size(105, 28);
            numMaxGuestsAdd.TabIndex = 6;
            numMaxGuestsAdd.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblActiveAdd
            // 
            lblActiveAdd.AutoSize = true;
            lblActiveAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            lblActiveAdd.ForeColor = SystemColors.ActiveCaptionText;
            lblActiveAdd.Location = new Point(26, 154);
            lblActiveAdd.Name = "lblActiveAdd";
            lblActiveAdd.Size = new Size(67, 24);
            lblActiveAdd.TabIndex = 7;
            lblActiveAdd.Text = "Active";
            // 
            // chkActiveAdd
            // 
            chkActiveAdd.Checked = true;
            chkActiveAdd.CheckState = CheckState.Checked;
            chkActiveAdd.Font = new Font("Microsoft Sans Serif", 13.8F);
            chkActiveAdd.Location = new Point(459, 153);
            chkActiveAdd.Margin = new Padding(3, 2, 3, 2);
            chkActiveAdd.Name = "chkActiveAdd";
            chkActiveAdd.Size = new Size(21, 18);
            chkActiveAdd.TabIndex = 8;
            // 
            // lblDetailsAdd
            // 
            lblDetailsAdd.AutoSize = true;
            lblDetailsAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            lblDetailsAdd.ForeColor = SystemColors.ActiveCaptionText;
            lblDetailsAdd.Location = new Point(26, 192);
            lblDetailsAdd.Name = "lblDetailsAdd";
            lblDetailsAdd.Size = new Size(72, 24);
            lblDetailsAdd.TabIndex = 9;
            lblDetailsAdd.Text = "Details";
            // 
            // txtDetailsAdd
            // 
            txtDetailsAdd.Font = new Font("Microsoft Sans Serif", 13.8F);
            txtDetailsAdd.Location = new Point(459, 189);
            txtDetailsAdd.Margin = new Padding(3, 2, 3, 2);
            txtDetailsAdd.Multiline = true;
            txtDetailsAdd.Name = "txtDetailsAdd";
            txtDetailsAdd.ScrollBars = ScrollBars.Vertical;
            txtDetailsAdd.Size = new Size(613, 61);
            txtDetailsAdd.TabIndex = 10;
            // 
            // btnCancelAdd
            // 
            btnCancelAdd.BackColor = Color.FromArgb(154, 35, 38);
            btnCancelAdd.FlatAppearance.BorderSize = 0;
            btnCancelAdd.FlatStyle = FlatStyle.Flat;
            btnCancelAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            btnCancelAdd.ForeColor = Color.White;
            btnCancelAdd.Location = new Point(40, 405);
            btnCancelAdd.Margin = new Padding(3, 2, 3, 2);
            btnCancelAdd.Name = "btnCancelAdd";
            btnCancelAdd.Size = new Size(269, 35);
            btnCancelAdd.TabIndex = 12;
            btnCancelAdd.Text = "Cancel";
            btnCancelAdd.UseVisualStyleBackColor = false;
            btnCancelAdd.Click += BtnCancelAdd_Click;
            // 
            // btnConfirmAdd
            // 
            btnConfirmAdd.BackColor = Color.FromArgb(39, 114, 47);
            btnConfirmAdd.FlatAppearance.BorderSize = 0;
            btnConfirmAdd.FlatStyle = FlatStyle.Flat;
            btnConfirmAdd.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            btnConfirmAdd.ForeColor = Color.White;
            btnConfirmAdd.Location = new Point(1023, 405);
            btnConfirmAdd.Margin = new Padding(3, 2, 3, 2);
            btnConfirmAdd.Name = "btnConfirmAdd";
            btnConfirmAdd.Size = new Size(269, 35);
            btnConfirmAdd.TabIndex = 11;
            btnConfirmAdd.Text = "Save Package";
            btnConfirmAdd.UseVisualStyleBackColor = false;
            btnConfirmAdd.Click += BtnConfirmAdd_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkSeaGreen;
            panel1.Controls.Add(button11);
            panel1.Location = new Point(-2, 73);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(245, 701);
            panel1.TabIndex = 1;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(154, 35, 58);
            button11.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button11.ForeColor = SystemColors.ButtonHighlight;
            button11.Location = new Point(10, 58);
            button11.Margin = new Padding(3, 2, 3, 2);
            button11.Name = "button11";
            button11.Size = new Size(219, 38);
            button11.TabIndex = 15;
            button11.Text = "Log Out";
            button11.UseVisualStyleBackColor = false;
            button11.Click += LogoutBtn_Click;
            // 
            // tab1Btn
            // 
            tab1Btn.BackColor = Color.FromArgb(209, 254, 216);
            tab1Btn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            tab1Btn.FlatAppearance.BorderSize = 0;
            tab1Btn.FlatStyle = FlatStyle.Flat;
            tab1Btn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tab1Btn.Location = new Point(241, 34);
            tab1Btn.Margin = new Padding(3, 2, 3, 2);
            tab1Btn.Name = "tab1Btn";
            tab1Btn.Size = new Size(262, 38);
            tab1Btn.TabIndex = 3;
            tab1Btn.Text = "Package Configuration";
            tab1Btn.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Image = Properties.Resources.OrchardLogoResized;
            pictureBox2.Location = new Point(63, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(111, 109);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 16;
            pictureBox2.TabStop = false;
            // 
            // PackageConf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 255, 238);
            ClientSize = new Size(1643, 772);
            Controls.Add(pictureBox2);
            Controls.Add(tab1Btn);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 2, 3, 2);
            Name = "PackageConf";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "The Orchard Farm and Resort Reservation Management System";
            WindowState = FormWindowState.Maximized;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            addPackageTab.ResumeLayout(false);
            addPackageTab.PerformLayout();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxGuestsAdd).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        private Button tab1Btn;
        private Label ReservationLbl;
        private DataGridView dataGridView1;
        private TextBox searchBox;
        private Button DelPackageBtn;
        private Button EditPackageBtn;
        private Button AddPackageBtn;

        // Add-package tab controls (designer-managed)
        private TabPage addPackageTab;
        private Label addHeaderLabel;
        private Panel panelInfo;
        private Label panelTitle;
        private Label lblNameAdd;
        private TextBox txtNameAdd;
        private Label lblPriceAdd;
        private TextBox txtPriceAdd;
        private Label lblMaxGuestsAdd;
        private NumericUpDown numMaxGuestsAdd;
        private Label lblActiveAdd;
        private CheckBox chkActiveAdd;
        private Label lblDetailsAdd;
        private TextBox txtDetailsAdd;
        private Button btnConfirmAdd;
        private Button btnCancelAdd;
        private Button button11;
        private PictureBox pictureBox2;
    }
}