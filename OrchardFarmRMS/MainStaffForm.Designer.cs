namespace OrchardFarmRMS
{
    partial class MainAdminForm
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
            AddResBtn = new Button();
            DelResBtn = new Button();
            EditResBtn = new Button();
            searchBox = new TextBox();
            ReservationLbl = new Label();
            dataGridView1 = new DataGridView();
            tabPage2 = new TabPage();
            DelCustomerBtn = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            label11 = new Label();
            dataGridView2 = new DataGridView();
            tabPage3 = new TabPage();
            cancelBtn = new Button();
            confirmBtn = new Button();
            panel3 = new Panel();
            ResClearBtn = new Button();
            noteBox = new TextBox();
            checkOutDateBox = new DateTimePicker();
            checkInDateBox = new DateTimePicker();
            guestBox = new NumericUpDown();
            packageBox = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            panel2 = new Panel();
            CustomerClearBtn = new Button();
            ContactBox = new TextBox();
            NameBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            guestStayLbl = new Label();
            tabPage5 = new TabPage();
            button4 = new Button();
            button5 = new Button();
            textBox4 = new TextBox();
            label15 = new Label();
            dataGridView3 = new DataGridView();
            tabPage6 = new TabPage();
            button8 = new Button();
            button7 = new Button();
            panel5 = new Panel();
            textBox8 = new TextBox();
            comboBox1 = new ComboBox();
            label16 = new Label();
            label23 = new Label();
            label22 = new Label();
            label24 = new Label();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            pictureBox1 = new PictureBox();
            button6 = new Button();
            dateTimePicker1 = new DateTimePicker();
            label17 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label25 = new Label();
            button11 = new Button();
            panel1 = new Panel();
            tab1Btn = new Button();
            tab2Btn = new Button();
            button10 = new Button();
            pictureBox2 = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage3.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guestBox).BeginInit();
            panel2.SuspendLayout();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabPage6.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
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
            tabPage1.Controls.Add(AddResBtn);
            tabPage1.Controls.Add(DelResBtn);
            tabPage1.Controls.Add(EditResBtn);
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
            tabPage1.Text = "Reservations";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // AddResBtn
            // 
            AddResBtn.BackColor = Color.FromArgb(37, 105, 44);
            AddResBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            AddResBtn.FlatAppearance.BorderSize = 0;
            AddResBtn.FlatStyle = FlatStyle.Flat;
            AddResBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddResBtn.ForeColor = SystemColors.Control;
            AddResBtn.Location = new Point(49, 580);
            AddResBtn.Margin = new Padding(3, 2, 3, 2);
            AddResBtn.Name = "AddResBtn";
            AddResBtn.Size = new Size(407, 38);
            AddResBtn.TabIndex = 7;
            AddResBtn.Text = "Add Reservation";
            AddResBtn.UseVisualStyleBackColor = false;
            AddResBtn.Click += AddResBtn_Click;
            // 
            // DelResBtn
            // 
            DelResBtn.BackColor = Color.FromArgb(154, 35, 58);
            DelResBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            DelResBtn.FlatAppearance.BorderSize = 0;
            DelResBtn.FlatStyle = FlatStyle.Flat;
            DelResBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DelResBtn.ForeColor = SystemColors.ButtonHighlight;
            DelResBtn.Location = new Point(946, 580);
            DelResBtn.Margin = new Padding(3, 2, 3, 2);
            DelResBtn.Name = "DelResBtn";
            DelResBtn.Size = new Size(407, 38);
            DelResBtn.TabIndex = 6;
            DelResBtn.Text = "Delete Reservation";
            DelResBtn.UseVisualStyleBackColor = false;
            // 
            // EditResBtn
            // 
            EditResBtn.BackColor = Color.FromArgb(60, 62, 128);
            EditResBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            EditResBtn.FlatAppearance.BorderSize = 0;
            EditResBtn.FlatStyle = FlatStyle.Flat;
            EditResBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EditResBtn.ForeColor = SystemColors.Control;
            EditResBtn.Location = new Point(496, 580);
            EditResBtn.Margin = new Padding(3, 2, 3, 2);
            EditResBtn.Name = "EditResBtn";
            EditResBtn.Size = new Size(407, 38);
            EditResBtn.TabIndex = 5;
            EditResBtn.Text = "Edit Reservation";
            EditResBtn.UseVisualStyleBackColor = false;
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
            ReservationLbl.Size = new Size(253, 44);
            ReservationLbl.TabIndex = 1;
            ReservationLbl.Text = "Reservations";
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
            // 
            // tabPage2
            // 
            tabPage2.BackgroundImage = Properties.Resources.ReservationsBG;
            tabPage2.BackgroundImageLayout = ImageLayout.Stretch;
            tabPage2.Controls.Add(DelCustomerBtn);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(textBox1);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(1416, 678);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Customers";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // DelCustomerBtn
            // 
            DelCustomerBtn.BackColor = Color.FromArgb(154, 35, 58);
            DelCustomerBtn.FlatAppearance.BorderSize = 0;
            DelCustomerBtn.FlatStyle = FlatStyle.Flat;
            DelCustomerBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DelCustomerBtn.ForeColor = Color.White;
            DelCustomerBtn.Location = new Point(512, 586);
            DelCustomerBtn.Margin = new Padding(3, 2, 3, 2);
            DelCustomerBtn.Name = "DelCustomerBtn";
            DelCustomerBtn.Size = new Size(406, 38);
            DelCustomerBtn.TabIndex = 21;
            DelCustomerBtn.Text = "Delete All Customer Info";
            DelCustomerBtn.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 62, 128);
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(57, 586);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(407, 38);
            button2.TabIndex = 11;
            button2.Text = "View Customer History";
            button2.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(130, 114);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1232, 32);
            textBox1.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(551, 58);
            label11.Name = "label11";
            label11.Size = new Size(213, 44);
            label11.TabIndex = 9;
            label11.Text = "Customers";
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(57, 182);
            dataGridView2.Margin = new Padding(3, 2, 3, 2);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1304, 381);
            dataGridView2.TabIndex = 8;
            // 
            // tabPage3
            // 
            tabPage3.BackgroundImage = Properties.Resources.ReservationsBG;
            tabPage3.BackgroundImageLayout = ImageLayout.Stretch;
            tabPage3.Controls.Add(cancelBtn);
            tabPage3.Controls.Add(confirmBtn);
            tabPage3.Controls.Add(panel3);
            tabPage3.Controls.Add(panel2);
            tabPage3.Controls.Add(guestStayLbl);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Margin = new Padding(3, 2, 3, 2);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1416, 678);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "AddReservation";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = Color.FromArgb(154, 35, 38);
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancelBtn.ForeColor = Color.White;
            cancelBtn.Location = new Point(40, 634);
            cancelBtn.Margin = new Padding(3, 2, 3, 2);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(269, 35);
            cancelBtn.TabIndex = 17;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // confirmBtn
            // 
            confirmBtn.BackColor = Color.FromArgb(39, 114, 47);
            confirmBtn.FlatAppearance.BorderSize = 0;
            confirmBtn.FlatStyle = FlatStyle.Flat;
            confirmBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            confirmBtn.ForeColor = Color.White;
            confirmBtn.Location = new Point(1023, 634);
            confirmBtn.Margin = new Padding(3, 2, 3, 2);
            confirmBtn.Name = "confirmBtn";
            confirmBtn.Size = new Size(269, 35);
            confirmBtn.TabIndex = 16;
            confirmBtn.Text = "Confirm Reservation";
            confirmBtn.UseVisualStyleBackColor = false;
            confirmBtn.Click += confirmBtn_Click_1;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(ResClearBtn);
            panel3.Controls.Add(noteBox);
            panel3.Controls.Add(checkOutDateBox);
            panel3.Controls.Add(checkInDateBox);
            panel3.Controls.Add(guestBox);
            panel3.Controls.Add(packageBox);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label4);
            panel3.ForeColor = SystemColors.ActiveBorder;
            panel3.Location = new Point(40, 325);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1252, 296);
            panel3.TabIndex = 2;
            // 
            // ResClearBtn
            // 
            ResClearBtn.BackColor = Color.FromArgb(158, 226, 167);
            ResClearBtn.FlatAppearance.BorderSize = 0;
            ResClearBtn.FlatStyle = FlatStyle.Flat;
            ResClearBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ResClearBtn.ForeColor = SystemColors.ActiveCaptionText;
            ResClearBtn.Location = new Point(710, 262);
            ResClearBtn.Margin = new Padding(3, 2, 3, 2);
            ResClearBtn.Name = "ResClearBtn";
            ResClearBtn.Size = new Size(128, 28);
            ResClearBtn.TabIndex = 8;
            ResClearBtn.Text = "Clear";
            ResClearBtn.UseVisualStyleBackColor = false;
            ResClearBtn.Click += ResClearBtn_Click;
            // 
            // noteBox
            // 
            noteBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            noteBox.Location = new Point(459, 201);
            noteBox.Margin = new Padding(3, 2, 3, 2);
            noteBox.Multiline = true;
            noteBox.Name = "noteBox";
            noteBox.Size = new Size(378, 57);
            noteBox.TabIndex = 15;
            // 
            // checkOutDateBox
            // 
            checkOutDateBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkOutDateBox.Location = new Point(458, 160);
            checkOutDateBox.Margin = new Padding(3, 2, 3, 2);
            checkOutDateBox.Name = "checkOutDateBox";
            checkOutDateBox.Size = new Size(378, 28);
            checkOutDateBox.TabIndex = 14;
            // 
            // checkInDateBox
            // 
            checkInDateBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkInDateBox.Location = new Point(459, 120);
            checkInDateBox.Margin = new Padding(3, 2, 3, 2);
            checkInDateBox.Name = "checkInDateBox";
            checkInDateBox.Size = new Size(378, 28);
            checkInDateBox.TabIndex = 13;
            // 
            // guestBox
            // 
            guestBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guestBox.Location = new Point(459, 82);
            guestBox.Margin = new Padding(3, 2, 3, 2);
            guestBox.Name = "guestBox";
            guestBox.Size = new Size(378, 28);
            guestBox.TabIndex = 12;
            // 
            // packageBox
            // 
            packageBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            packageBox.FormattingEnabled = true;
            packageBox.Items.AddRange(new object[] { "test package 1", "test package 2" });
            packageBox.Location = new Point(459, 44);
            packageBox.Margin = new Padding(3, 2, 3, 2);
            packageBox.Name = "packageBox";
            packageBox.Size = new Size(378, 30);
            packageBox.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.Location = new Point(24, 5);
            label9.Name = "label9";
            label9.Size = new Size(240, 29);
            label9.TabIndex = 10;
            label9.Text = "Reservation Details";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(32, 201);
            label8.Name = "label8";
            label8.Size = new Size(129, 24);
            label8.TabIndex = 9;
            label8.Text = "Special Note";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.Location = new Point(29, 163);
            label7.Name = "label7";
            label7.Size = new Size(157, 24);
            label7.TabIndex = 8;
            label7.Text = "Check-Out Date";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(31, 124);
            label6.Name = "label6";
            label6.Size = new Size(141, 24);
            label6.TabIndex = 7;
            label6.Text = "Check-In Date";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(32, 86);
            label5.Name = "label5";
            label5.Size = new Size(136, 24);
            label5.TabIndex = 6;
            label5.Text = "No. of Guests";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(32, 48);
            label4.Name = "label4";
            label4.Size = new Size(143, 24);
            label4.TabIndex = 5;
            label4.Text = "Package Type";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(CustomerClearBtn);
            panel2.Controls.Add(ContactBox);
            panel2.Controls.Add(NameBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.ForeColor = SystemColors.ActiveBorder;
            panel2.Location = new Point(40, 148);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1252, 180);
            panel2.TabIndex = 1;
            // 
            // CustomerClearBtn
            // 
            CustomerClearBtn.BackColor = Color.FromArgb(158, 226, 167);
            CustomerClearBtn.FlatAppearance.BorderSize = 0;
            CustomerClearBtn.FlatStyle = FlatStyle.Flat;
            CustomerClearBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CustomerClearBtn.ForeColor = SystemColors.ActiveCaptionText;
            CustomerClearBtn.Location = new Point(710, 134);
            CustomerClearBtn.Margin = new Padding(3, 2, 3, 2);
            CustomerClearBtn.Name = "CustomerClearBtn";
            CustomerClearBtn.Size = new Size(128, 27);
            CustomerClearBtn.TabIndex = 9;
            CustomerClearBtn.Text = "Clear";
            CustomerClearBtn.UseVisualStyleBackColor = false;
            CustomerClearBtn.Click += CustomerClearBtn_Click;
            // 
            // ContactBox
            // 
            ContactBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ContactBox.Location = new Point(459, 89);
            ContactBox.Margin = new Padding(3, 2, 3, 2);
            ContactBox.Name = "ContactBox";
            ContactBox.Size = new Size(378, 28);
            ContactBox.TabIndex = 7;
            // 
            // NameBox
            // 
            NameBox.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameBox.Location = new Point(459, 52);
            NameBox.Margin = new Padding(3, 2, 3, 2);
            NameBox.Name = "NameBox";
            NameBox.Size = new Size(378, 28);
            NameBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(29, 94);
            label3.Name = "label3";
            label3.Size = new Size(170, 24);
            label3.TabIndex = 5;
            label3.Text = "Contact (FB Link)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(29, 57);
            label2.Name = "label2";
            label2.Size = new Size(106, 24);
            label2.TabIndex = 4;
            label2.Text = "Full Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(18, 8);
            label1.Name = "label1";
            label1.Size = new Size(213, 29);
            label1.TabIndex = 3;
            label1.Text = "Customer Details";
            // 
            // guestStayLbl
            // 
            guestStayLbl.AutoSize = true;
            guestStayLbl.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guestStayLbl.Location = new Point(29, 70);
            guestStayLbl.Name = "guestStayLbl";
            guestStayLbl.Size = new Size(365, 37);
            guestStayLbl.TabIndex = 0;
            guestStayLbl.Text = "Guest and Stay Details";
            // 
            // tabPage5
            // 
            tabPage5.BackgroundImage = Properties.Resources.ReservationsBG;
            tabPage5.Controls.Add(button4);
            tabPage5.Controls.Add(button5);
            tabPage5.Controls.Add(textBox4);
            tabPage5.Controls.Add(label15);
            tabPage5.Controls.Add(dataGridView3);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Margin = new Padding(3, 2, 3, 2);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3, 2, 3, 2);
            tabPage5.Size = new Size(1416, 678);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Payments";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(37, 105, 44);
            button4.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = SystemColors.Control;
            button4.Location = new Point(57, 586);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(407, 38);
            button4.TabIndex = 17;
            button4.Text = "Update Payment Record";
            button4.UseVisualStyleBackColor = false;
            button4.Click += UpdatePaymentBtn_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(60, 62, 128);
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = SystemColors.Control;
            button5.Location = new Point(504, 586);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(407, 38);
            button5.TabIndex = 16;
            button5.Text = "Delete Payment Record";
            button5.UseVisualStyleBackColor = false;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox4.Location = new Point(130, 114);
            textBox4.Margin = new Padding(3, 2, 3, 2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(1232, 32);
            textBox4.TabIndex = 15;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Sans Serif", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(551, 58);
            label15.Name = "label15";
            label15.Size = new Size(197, 44);
            label15.TabIndex = 14;
            label15.Text = "Payments";
            // 
            // dataGridView3
            // 
            dataGridView3.BackgroundColor = Color.White;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(57, 182);
            dataGridView3.Margin = new Padding(3, 2, 3, 2);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(1304, 381);
            dataGridView3.TabIndex = 13;
            // 
            // tabPage6
            // 
            tabPage6.BackgroundImage = Properties.Resources.ReservationsBG;
            tabPage6.Controls.Add(button8);
            tabPage6.Controls.Add(button7);
            tabPage6.Controls.Add(panel5);
            tabPage6.Controls.Add(label25);
            tabPage6.Location = new Point(4, 24);
            tabPage6.Margin = new Padding(3, 2, 3, 2);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3, 2, 3, 2);
            tabPage6.Size = new Size(1416, 678);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Payment Details";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(37, 105, 44);
            button8.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.ForeColor = SystemColors.ButtonHighlight;
            button8.Location = new Point(1072, 459);
            button8.Margin = new Padding(3, 2, 3, 2);
            button8.Name = "button8";
            button8.Size = new Size(269, 35);
            button8.TabIndex = 13;
            button8.Text = "Update Payment Info";
            button8.UseVisualStyleBackColor = false;
            button8.Click += SavePaymentFromTab;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(154, 35, 38);
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.White;
            button7.Location = new Point(89, 459);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(269, 35);
            button7.TabIndex = 22;
            button7.Text = "Cancel";
            button7.UseVisualStyleBackColor = false;
            button7.Click += cancelBtn_Click_1;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(textBox8);
            panel5.Controls.Add(comboBox1);
            panel5.Controls.Add(label16);
            panel5.Controls.Add(label23);
            panel5.Controls.Add(label22);
            panel5.Controls.Add(label24);
            panel5.Controls.Add(textBox6);
            panel5.Controls.Add(textBox7);
            panel5.Controls.Add(pictureBox1);
            panel5.Controls.Add(button6);
            panel5.Controls.Add(dateTimePicker1);
            panel5.Controls.Add(label17);
            panel5.Controls.Add(label19);
            panel5.Controls.Add(label20);
            panel5.Controls.Add(label21);
            panel5.ForeColor = SystemColors.ActiveBorder;
            panel5.Location = new Point(89, 108);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(1252, 325);
            panel5.TabIndex = 20;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(457, 180);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(378, 23);
            textBox8.TabIndex = 23;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(457, 220);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(378, 23);
            comboBox1.TabIndex = 22;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label16.ForeColor = SystemColors.ActiveCaptionText;
            label16.Location = new Point(30, 219);
            label16.Name = "label16";
            label16.Size = new Size(144, 24);
            label16.TabIndex = 21;
            label16.Text = "Amount Status";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label23.ForeColor = SystemColors.ActiveCaptionText;
            label23.Location = new Point(30, 134);
            label23.Name = "label23";
            label23.Size = new Size(100, 24);
            label23.TabIndex = 20;
            label23.Text = "Total Due";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label22.ForeColor = SystemColors.ActiveCaptionText;
            label22.Location = new Point(30, 92);
            label22.Name = "label22";
            label22.Size = new Size(156, 24);
            label22.TabIndex = 19;
            label22.Text = "Extension Fees";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label24.ForeColor = SystemColors.ActiveCaptionText;
            label24.Location = new Point(457, 54);
            label24.Name = "label24";
            label24.Size = new Size(145, 24);
            label24.TabIndex = 18;
            label24.Text = "Payment Proof";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(457, 95);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(378, 23);
            textBox6.TabIndex = 17;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(457, 137);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(378, 23);
            textBox7.TabIndex = 16;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(895, 21);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(328, 204);
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(158, 226, 167);
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button6.ForeColor = SystemColors.ActiveCaptionText;
            button6.Location = new Point(1095, 242);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Size = new Size(128, 34);
            button6.TabIndex = 8;
            button6.Text = "Clear";
            button6.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(457, 253);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(378, 28);
            dateTimePicker1.TabIndex = 13;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label17.ForeColor = SystemColors.ActiveCaptionText;
            label17.Location = new Point(907, 247);
            label17.Name = "label17";
            label17.Size = new Size(145, 24);
            label17.TabIndex = 9;
            label17.Text = "Payment Proof";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label19.ForeColor = SystemColors.ActiveCaptionText;
            label19.Location = new Point(30, 258);
            label19.Name = "label19";
            label19.Size = new Size(138, 24);
            label19.TabIndex = 7;
            label19.Text = "Payment Date";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label20.ForeColor = SystemColors.ActiveCaptionText;
            label20.Location = new Point(30, 180);
            label20.Name = "label20";
            label20.Size = new Size(129, 24);
            label20.TabIndex = 6;
            label20.Text = "Amount Paid";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            label21.ForeColor = SystemColors.ActiveCaptionText;
            label21.Location = new Point(30, 45);
            label21.Name = "label21";
            label21.Size = new Size(144, 24);
            label21.TabIndex = 5;
            label21.Text = "Package Price";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label25.Location = new Point(78, 40);
            label25.Name = "label25";
            label25.Size = new Size(263, 37);
            label25.TabIndex = 18;
            label25.Text = "Payment Details";
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(154, 35, 58);
            button11.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button11.ForeColor = SystemColors.ButtonHighlight;
            button11.Location = new Point(10, 57);
            button11.Margin = new Padding(3, 2, 3, 2);
            button11.Name = "button11";
            button11.Size = new Size(219, 38);
            button11.TabIndex = 13;
            button11.Text = "Log Out";
            button11.UseVisualStyleBackColor = false;
            button11.Click += LogoutBtn_Click;
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
            tab1Btn.Size = new Size(182, 38);
            tab1Btn.TabIndex = 3;
            tab1Btn.Text = "Reservations";
            tab1Btn.UseVisualStyleBackColor = false;
            tab1Btn.Click += ResTab_Click;
            // 
            // tab2Btn
            // 
            tab2Btn.BackColor = Color.FromArgb(209, 254, 216);
            tab2Btn.FlatAppearance.BorderSize = 0;
            tab2Btn.FlatStyle = FlatStyle.Flat;
            tab2Btn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tab2Btn.Location = new Point(428, 34);
            tab2Btn.Margin = new Padding(3, 2, 3, 2);
            tab2Btn.Name = "tab2Btn";
            tab2Btn.Size = new Size(182, 38);
            tab2Btn.TabIndex = 4;
            tab2Btn.Text = "Customers";
            tab2Btn.UseVisualStyleBackColor = false;
            tab2Btn.Click += tab2Btn_Click;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(209, 254, 216);
            button10.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 0);
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button10.Location = new Point(615, 34);
            button10.Margin = new Padding(3, 2, 3, 2);
            button10.Name = "button10";
            button10.Size = new Size(182, 38);
            button10.TabIndex = 5;
            button10.Text = "Payments";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Image = Properties.Resources.OrchardLogoResized;
            pictureBox2.Location = new Point(59, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(111, 109);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            // 
            // MainAdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 255, 238);
            ClientSize = new Size(1643, 772);
            Controls.Add(pictureBox2);
            Controls.Add(button10);
            Controls.Add(tab2Btn);
            Controls.Add(tab1Btn);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "The Orchard Farm and Resort Reservation Management System";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guestBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Panel panel1;
        private Button tab1Btn;
        private Button tab2Btn;
        private Label ReservationLbl;
        private DataGridView dataGridView1;
        private TextBox searchBox;
        private Button cancelBtn;
        private Button DelResBtn;
        private Button EditResBtn;
        private Button AddResBtn;
        private TabPage tabPage3;
        private Panel panel2;
        private Label guestStayLbl;
        private Panel panel3;
        private Label label2;
        private Label label1;
        private Label label3;
        private TextBox ContactBox;
        private TextBox NameBox;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox noteBox;
        private DateTimePicker checkOutDateBox;
        private DateTimePicker checkInDateBox;
        private NumericUpDown guestBox;
        private ComboBox packageBox;
        private Button ResClearBtn;
        private Button CustomerClearBtn;
        private Button confirmBtn;
        private Button button2;
        private TextBox textBox1;
        private Label label11;
        private DataGridView dataGridView2;
        private TabPage tabPage5;
        private Button button4;
        private Button button5;
        private TextBox textBox4;
        private Label label15;
        private DataGridView dataGridView3;
        private TabPage tabPage6;
        private Button button7;
        private Button DelCustomerBtn;
        private Panel panel5;
        private Button button6;
        private TextBox textBox5;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Label label17;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label25;
        private PictureBox pictureBox1;
        private Button button10;
        private Button button11;
        private TextBox textBox6;
        private TextBox textBox7;
        private Label label23;
        private Label label22;
        private Label label24;
        private Label label16;
        private ComboBox comboBox1;
        private TextBox textBox8;
        private Button button8;
        private PictureBox pictureBox2;
    }
}
