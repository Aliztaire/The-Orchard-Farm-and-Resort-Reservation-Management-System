namespace OrchardFarmRMS
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            OrchardLogo = new Label();
            BrandName = new Label();
            SubBrandName = new Label();
            logintitle = new Label();
            AdminBtn = new Button();
            StaffBtn = new Button();
            UserLbl = new Label();
            PasswordLbl = new Label();
            LoginBtn = new Button();
            ExitBtn = new Button();
            UserTxtbox = new TextBox();
            PassTxtbox = new TextBox();
            SuspendLayout();
            // 
            // OrchardLogo
            // 
            OrchardLogo.BackColor = Color.Transparent;
            OrchardLogo.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OrchardLogo.Image = Properties.Resources.OrchardLogoResized;
            OrchardLogo.Location = new Point(283, 24);
            OrchardLogo.Name = "OrchardLogo";
            OrchardLogo.Size = new Size(75, 83);
            OrchardLogo.TabIndex = 0;
            // 
            // BrandName
            // 
            BrandName.AutoSize = true;
            BrandName.BackColor = Color.Transparent;
            BrandName.Font = new Font("Bookman Old Style", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BrandName.ForeColor = Color.FromArgb(15, 53, 32);
            BrandName.Location = new Point(363, 36);
            BrandName.Name = "BrandName";
            BrandName.Size = new Size(478, 36);
            BrandName.TabIndex = 1;
            BrandName.Text = "The Orchard Farm and Resort";
            // 
            // SubBrandName
            // 
            SubBrandName.AutoSize = true;
            SubBrandName.BackColor = Color.Transparent;
            SubBrandName.Font = new Font("Bookman Old Style", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SubBrandName.ForeColor = Color.FromArgb(0, 111, 61);
            SubBrandName.Location = new Point(377, 75);
            SubBrandName.Name = "SubBrandName";
            SubBrandName.Size = new Size(449, 29);
            SubBrandName.TabIndex = 2;
            SubBrandName.Text = "Reservation Management System";
            // 
            // logintitle
            // 
            logintitle.AutoSize = true;
            logintitle.BackColor = Color.Transparent;
            logintitle.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logintitle.Location = new Point(530, 156);
            logintitle.Name = "logintitle";
            logintitle.Size = new Size(104, 32);
            logintitle.TabIndex = 3;
            logintitle.Text = "Log in";
            // 
            // AdminBtn
            // 
            AdminBtn.BackColor = Color.SeaGreen;
            AdminBtn.BackgroundImageLayout = ImageLayout.Stretch;
            AdminBtn.Cursor = Cursors.Hand;
            AdminBtn.FlatStyle = FlatStyle.Flat;
            AdminBtn.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AdminBtn.ForeColor = Color.White;
            AdminBtn.Location = new Point(432, 199);
            AdminBtn.Name = "AdminBtn";
            AdminBtn.Size = new Size(135, 35);
            AdminBtn.TabIndex = 4;
            AdminBtn.Text = "Admin";
            AdminBtn.UseVisualStyleBackColor = false;
            AdminBtn.Click += AdminBtn_Click;
            // 
            // StaffBtn
            // 
            StaffBtn.BackColor = Color.SeaGreen;
            StaffBtn.BackgroundImageLayout = ImageLayout.Stretch;
            StaffBtn.Cursor = Cursors.Hand;
            StaffBtn.FlatStyle = FlatStyle.Flat;
            StaffBtn.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StaffBtn.ForeColor = Color.White;
            StaffBtn.Location = new Point(603, 199);
            StaffBtn.Name = "StaffBtn";
            StaffBtn.Size = new Size(135, 35);
            StaffBtn.TabIndex = 5;
            StaffBtn.Text = "Staff";
            StaffBtn.UseVisualStyleBackColor = false;
            StaffBtn.Click += StaffBtn_Click;
            // 
            // UserLbl
            // 
            UserLbl.AutoSize = true;
            UserLbl.BackColor = Color.Transparent;
            UserLbl.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UserLbl.Location = new Point(432, 265);
            UserLbl.Name = "UserLbl";
            UserLbl.Size = new Size(112, 24);
            UserLbl.TabIndex = 6;
            UserLbl.Text = "Username";
            // 
            // PasswordLbl
            // 
            PasswordLbl.AutoSize = true;
            PasswordLbl.BackColor = Color.Transparent;
            PasswordLbl.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PasswordLbl.Location = new Point(432, 362);
            PasswordLbl.Name = "PasswordLbl";
            PasswordLbl.Size = new Size(104, 24);
            PasswordLbl.TabIndex = 8;
            PasswordLbl.Text = "Password";
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.FromArgb(24, 112, 87);
            LoginBtn.BackgroundImageLayout = ImageLayout.Stretch;
            LoginBtn.Cursor = Cursors.Hand;
            LoginBtn.FlatStyle = FlatStyle.Flat;
            LoginBtn.Font = new Font("Cambria", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoginBtn.ForeColor = Color.White;
            LoginBtn.Location = new Point(432, 460);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(306, 42);
            LoginBtn.TabIndex = 10;
            LoginBtn.Text = "Log in";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // ExitBtn
            // 
            ExitBtn.BackColor = Color.Transparent;
            ExitBtn.BackgroundImage = (Image)resources.GetObject("ExitBtn.BackgroundImage");
            ExitBtn.BackgroundImageLayout = ImageLayout.Stretch;
            ExitBtn.FlatAppearance.BorderSize = 0;
            ExitBtn.FlatStyle = FlatStyle.Flat;
            ExitBtn.Location = new Point(863, 1);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.Size = new Size(35, 35);
            ExitBtn.TabIndex = 11;
            ExitBtn.TabStop = false;
            ExitBtn.UseVisualStyleBackColor = false;
            ExitBtn.Click += ExitBtn_Click;
            // 
            // UserTxtbox
            // 
            UserTxtbox.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UserTxtbox.Location = new Point(432, 292);
            UserTxtbox.Name = "UserTxtbox";
            UserTxtbox.Size = new Size(306, 31);
            UserTxtbox.TabIndex = 12;
            // 
            // PassTxtbox
            // 
            PassTxtbox.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PassTxtbox.Location = new Point(432, 389);
            PassTxtbox.Name = "PassTxtbox";
            PassTxtbox.PasswordChar = '*';
            PassTxtbox.Size = new Size(306, 31);
            PassTxtbox.TabIndex = 13;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.LoginBG;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(900, 644);
            Controls.Add(PassTxtbox);
            Controls.Add(UserTxtbox);
            Controls.Add(ExitBtn);
            Controls.Add(LoginBtn);
            Controls.Add(PasswordLbl);
            Controls.Add(UserLbl);
            Controls.Add(StaffBtn);
            Controls.Add(AdminBtn);
            Controls.Add(logintitle);
            Controls.Add(SubBrandName);
            Controls.Add(BrandName);
            Controls.Add(OrchardLogo);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label OrchardLogo;
        private Label BrandName;
        private Label SubBrandName;
        private Label logintitle;
        private Button AdminBtn;
        private Button StaffBtn;
        private Label UserLbl;
        private TextBox textBox1;
        private Label PasswordLbl;
        private TextBox textBox2;
        private Button LoginBtn;
        private Button ExitBtn;
        private TextBox UserTxtbox;
        private TextBox PassTxtbox;
    }
}