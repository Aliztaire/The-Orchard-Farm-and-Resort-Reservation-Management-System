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
            // Add-package tab (designer-created)
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
            btnConfirmAdd = new Button();
            btnCancelAdd = new Button();
            panel1 = new Panel();
            OrchardLogo = new Label();
            tab1Btn = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            // add-package tab children init
            panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxGuestsAdd).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(addPackageTab);
            tabControl1.Location = new Point(271, 91);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1628, 941);
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
            tabPage1.Font = new Font("Bookman Old Style", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1620, 908);
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
            AddPackageBtn.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddPackageBtn.ForeColor = SystemColors.Control;
            AddPackageBtn.Location = new Point(56, 773);
            AddPackageBtn.Name = "AddPackageBtn";
            AddPackageBtn.Size = new Size(465, 50);
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
            DelPackageBtn.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DelPackageBtn.ForeColor = SystemColors.ButtonHighlight;
            DelPackageBtn.Location = new Point(1081, 773);
            DelPackageBtn.Name = "DelPackageBtn";
            DelPackageBtn.Size = new Size(465, 50);
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
            EditPackageBtn.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EditPackageBtn.ForeColor = SystemColors.Control;
            EditPackageBtn.Location = new Point(567, 773);
            EditPackageBtn.Name = "EditPackageBtn";
            EditPackageBtn.Size = new Size(465, 50);
            EditPackageBtn.TabIndex = 5;
            EditPackageBtn.Text = "Edit Package";
            EditPackageBtn.UseVisualStyleBackColor = false;
            EditPackageBtn.Click += EditPackageBtn_Click;
            // 
            // searchBox
            // 
            searchBox.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchBox.Location = new Point(139, 143);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(1407, 39);
            searchBox.TabIndex = 2;
            // 
            // ReservationLbl
            // 
            ReservationLbl.AutoSize = true;
            ReservationLbl.Font = new Font("Bookman Old Style", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReservationLbl.Location = new Point(621, 68);
            ReservationLbl.Name = "ReservationLbl";
            ReservationLbl.Size = new Size(540, 56);
            ReservationLbl.TabIndex = 1;
            ReservationLbl.Text = "Package Configuration";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(56, 233);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1490, 508);
            dataGridView1.TabIndex = 0;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
            // 
            // addPackageTab
            // 
            addPackageTab.BackgroundImage = Properties.Resources.ReservationsBG;
            addPackageTab.BackgroundImageLayout = ImageLayout.Stretch;
            addPackageTab.Location = new Point(4, 29);
            addPackageTab.Name = "addPackageTab";
            addPackageTab.Padding = new Padding(3);
            addPackageTab.Size = new Size(1620, 908);
            addPackageTab.TabIndex = 1;
            addPackageTab.Text = "Add Package";
            addPackageTab.UseVisualStyleBackColor = true;
            // 
            // addHeaderLabel
            // 
            addHeaderLabel.AutoSize = true;
            addHeaderLabel.Font = new Font("Bookman Old Style", 24F, FontStyle.Bold, GraphicsUnit.Point);
            addHeaderLabel.Location = new Point(40, 30);
            addHeaderLabel.Name = "addHeaderLabel";
            addHeaderLabel.Size = new Size(280, 46);
            addHeaderLabel.TabIndex = 0;
            addHeaderLabel.Text = "Add Package";
            addHeaderLabel.BackColor = Color.Transparent;
            addHeaderLabel.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // panelInfo
            // 
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
            panelInfo.Location = new Point(46, 150);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(1430, 360);
            panelInfo.TabIndex = 1;
            panelInfo.BackColor = Color.Transparent;
            // 
            // panelTitle
            // 
            panelTitle.AutoSize = true;
            panelTitle.Font = new Font("Bookman Old Style", 18F, FontStyle.Bold, GraphicsUnit.Point);
            panelTitle.Location = new Point(12, 8);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(203, 36);
            panelTitle.TabIndex = 0;
            panelTitle.Text = "Package Details";
            panelTitle.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // lblNameAdd
            // 
            lblNameAdd.AutoSize = true;
            lblNameAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            lblNameAdd.Location = new Point(30, 56);
            lblNameAdd.Name = "lblNameAdd";
            lblNameAdd.Size = new Size(174, 28);
            lblNameAdd.TabIndex = 1;
            lblNameAdd.Text = "Package Name";
            lblNameAdd.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // txtNameAdd
            // 
            txtNameAdd.Font = new Font("Bookman Old Style", 13.8F);
            txtNameAdd.Location = new Point(525, 52);
            txtNameAdd.Name = "txtNameAdd";
            txtNameAdd.Size = new Size(700, 34);
            txtNameAdd.TabIndex = 2;
            // 
            // lblPriceAdd
            // 
            lblPriceAdd.AutoSize = true;
            lblPriceAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            lblPriceAdd.Location = new Point(30, 106);
            lblPriceAdd.Name = "lblPriceAdd";
            lblPriceAdd.Size = new Size(66, 28);
            lblPriceAdd.TabIndex = 3;
            lblPriceAdd.Text = "Price";
            lblPriceAdd.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // txtPriceAdd
            // 
            txtPriceAdd.Font = new Font("Bookman Old Style", 13.8F);
            txtPriceAdd.Location = new Point(525, 102);
            txtPriceAdd.Name = "txtPriceAdd";
            txtPriceAdd.Size = new Size(240, 34);
            txtPriceAdd.TabIndex = 4;
            // 
            // lblMaxGuestsAdd
            // 
            lblMaxGuestsAdd.AutoSize = true;
            lblMaxGuestsAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            lblMaxGuestsAdd.Location = new Point(30, 156);
            lblMaxGuestsAdd.Name = "lblMaxGuestsAdd";
            lblMaxGuestsAdd.Size = new Size(172, 28);
            lblMaxGuestsAdd.TabIndex = 5;
            lblMaxGuestsAdd.Text = "Max Guests";
            lblMaxGuestsAdd.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // numMaxGuestsAdd
            // 
            numMaxGuestsAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            numMaxGuestsAdd.Location = new Point(525, 152);
            numMaxGuestsAdd.Name = "numMaxGuestsAdd";
            numMaxGuestsAdd.Size = new Size(120, 34);
            numMaxGuestsAdd.TabIndex = 6;
            numMaxGuestsAdd.Minimum = 1;
            numMaxGuestsAdd.Maximum = 1000;
            numMaxGuestsAdd.Value = 1;
            // 
            // lblActiveAdd
            // 
            lblActiveAdd.AutoSize = true;
            lblActiveAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            lblActiveAdd.Location = new Point(30, 206);
            lblActiveAdd.Name = "lblActiveAdd";
            lblActiveAdd.Size = new Size(78, 28);
            lblActiveAdd.TabIndex = 7;
            lblActiveAdd.Text = "Active";
            lblActiveAdd.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // chkActiveAdd
            // 
            chkActiveAdd.Font = new Font("Bookman Old Style", 13.8F);
            chkActiveAdd.Location = new Point(525, 204);
            chkActiveAdd.Name = "chkActiveAdd";
            chkActiveAdd.Size = new Size(24, 24);
            chkActiveAdd.TabIndex = 8;
            chkActiveAdd.Checked = true;
            // 
            // lblDetailsAdd
            // 
            lblDetailsAdd.AutoSize = true;
            lblDetailsAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold);
            lblDetailsAdd.Location = new Point(30, 256);
            lblDetailsAdd.Name = "lblDetailsAdd";
            lblDetailsAdd.Size = new Size(79, 28);
            lblDetailsAdd.TabIndex = 9;
            lblDetailsAdd.Text = "Details";
            lblDetailsAdd.ForeColor = SystemColors.ActiveCaptionText;
            // 
            // txtDetailsAdd
            // 
            txtDetailsAdd.Font = new Font("Bookman Old Style", 13.8F);
            txtDetailsAdd.Location = new Point(525, 252);
            txtDetailsAdd.Name = "txtDetailsAdd";
            txtDetailsAdd.Size = new Size(700, 80);
            txtDetailsAdd.Multiline = true;
            txtDetailsAdd.ScrollBars = ScrollBars.Vertical;
            txtDetailsAdd.TabIndex = 10;
            // 
            // btnConfirmAdd
            // 
            btnConfirmAdd.BackColor = Color.FromArgb(39, 114, 47);
            btnConfirmAdd.FlatAppearance.BorderSize = 0;
            btnConfirmAdd.FlatStyle = FlatStyle.Flat;
            btnConfirmAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnConfirmAdd.ForeColor = Color.White;
            btnConfirmAdd.Location = new Point(1169, 540);
            btnConfirmAdd.Name = "btnConfirmAdd";
            btnConfirmAdd.Size = new Size(307, 47);
            btnConfirmAdd.TabIndex = 11;
            btnConfirmAdd.Text = "Save Package";
            btnConfirmAdd.UseVisualStyleBackColor = false;
            btnConfirmAdd.Click += BtnConfirmAdd_Click;
            // 
            // btnCancelAdd
            // 
            btnCancelAdd.BackColor = Color.FromArgb(154, 35, 38);
            btnCancelAdd.FlatAppearance.BorderSize = 0;
            btnCancelAdd.FlatStyle = FlatStyle.Flat;
            btnCancelAdd.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancelAdd.ForeColor = Color.White;
            btnCancelAdd.Location = new Point(46, 540);
            btnCancelAdd.Name = "btnCancelAdd";
            btnCancelAdd.Size = new Size(307, 47);
            btnCancelAdd.TabIndex = 12;
            btnCancelAdd.Text = "Cancel";
            btnCancelAdd.UseVisualStyleBackColor = false;
            btnCancelAdd.Click += BtnCancelAdd_Click;
            // 
            // addPackageTab - assemble child controls
            // 
            addPackageTab.Controls.Add(addHeaderLabel);
            addPackageTab.Controls.Add(panelInfo);
            addPackageTab.Controls.Add(btnCancelAdd);
            addPackageTab.Controls.Add(btnConfirmAdd);
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkSeaGreen;
            panel1.Location = new Point(-2, 97);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 935);
            panel1.TabIndex = 1;
            // 
            // OrchardLogo
            // 
            OrchardLogo.BackColor = Color.Transparent;
            OrchardLogo.Image = Properties.Resources.OrchardLogoResized;
            OrchardLogo.Location = new Point(89, 9);
            OrchardLogo.Name = "OrchardLogo";
            OrchardLogo.Size = new Size(82, 78);
            OrchardLogo.TabIndex = 2;
            // 
            // tab1Btn
            // 
            tab1Btn.BackColor = Color.FromArgb(209, 254, 216);
            tab1Btn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            tab1Btn.FlatAppearance.BorderSize = 0;
            tab1Btn.FlatStyle = FlatStyle.Flat;
            tab1Btn.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tab1Btn.Location = new Point(275, 46);
            tab1Btn.Name = "tab1Btn";
            tab1Btn.Size = new Size(300, 50);
            tab1Btn.TabIndex = 3;
            tab1Btn.Text = "Package Configuration";
            tab1Btn.UseVisualStyleBackColor = false;
            // 
            // PackageConf
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 255, 238);
            ClientSize = new Size(1878, 1029);
            Controls.Add(tab1Btn);
            Controls.Add(OrchardLogo);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "PackageConf";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "The Orchard Farm and Resort Reservation Management System";
            WindowState = FormWindowState.Maximized;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxGuestsAdd).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        private Label OrchardLogo;
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
    }
}