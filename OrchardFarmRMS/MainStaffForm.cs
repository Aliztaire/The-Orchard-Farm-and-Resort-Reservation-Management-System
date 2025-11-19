using System;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OrchardFarmRMS
{
    public partial class MainAdminForm : Form
    {
        
        private int currentPackageMaxGuests = int.MaxValue;

        private DataTable reservationsTable;
        private DataTable customersTable;
        private DataTable paymentsTable;

        private int? editingReservationId;
        private int? editingCustomerId;
        private int? editingPaymentId;

        private Button saveCustomerBtn;
        private Button cancelCustomerBtn;

        private readonly string connString;

        private string selectedPaymentProofPath;
        private byte[] selectedPaymentProofBytes;

        public MainAdminForm()
        {
            InitializeComponent();
            ButtonSettings();

            connString = ConfigurationManager.ConnectionStrings["OrchardFarmDB"]?.ConnectionString
                         ?? throw new InvalidOperationException("Connection string 'OrchardFarmDB' not found.");

            WireUpEvents();
            ConfigureGrid(dataGridView1);
            ConfigureGrid(dataGridView2);
            ConfigureGrid(dataGridView3);
            
            this.Shown += MainAdminForm_Shown;
            // set up payment status combo box
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "Pending", "Paid", "Canceled" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.BackColor = Color.White;
            comboBox1.ForeColor = Color.FromArgb(33, 33, 33);

            if (pictureBox1 != null) pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // wire up payment proof browse button
            button6.Click += PaymentProofBrowse_Click;
            if (textBox6 != null)
            {
                textBox6.Leave += CurrencyTextBox_Leave;
                textBox6.KeyPress += CurrencyTextBox_KeyPress;
            }
            if (textBox7 != null)
            {
                textBox7.Leave += CurrencyTextBox_Leave;
                textBox7.KeyPress += CurrencyTextBox_KeyPress;
            }
            if (textBox8 != null)
            {
                textBox8.Leave += CurrencyTextBox_Leave;
                textBox8.KeyPress += CurrencyTextBox_KeyPress;
            }
            if (label22 != null) label22.Text = "Total Due";
            if (label23 != null) label23.Text = "Extension Fee";

            // Ensure CustomerClearBtn enabled by default at startup (defensive)
            if (CustomerClearBtn != null) CustomerClearBtn.Enabled = true;
        }

        private void MainAdminForm_Shown(object? sender, EventArgs e)
        {
            this.Shown -= MainAdminForm_Shown;
            LoadAllGrids();
        }

        //winform did'nt work so wire up events manually
        private void WireUpEvents()
        {
            packageBox.SelectedIndexChanged += PackageBox_SelectedIndexChanged;

            AddResBtn.Click += AddResBtn_Click;
            DelResBtn.Click += DelResBtn_Click;
            EditResBtn.Click += EditResBtn_Click;

            dataGridView1.RowHeaderMouseDoubleClick += (s, e) => EditResBtn_Click(s, EventArgs.Empty);
            dataGridView1.CellDoubleClick += (s, e) => EditResBtn_Click(s, EventArgs.Empty);

            dataGridView2.RowHeaderMouseDoubleClick += (s, e) => EditCustomerBtn_Click(s, EventArgs.Empty);
            dataGridView2.CellDoubleClick += (s, e) => ViewCustomerHistoryBtn_Click(s, EventArgs.Empty);

            dataGridView3.RowHeaderMouseDoubleClick += (s, e) => UpdatePaymentBtn_Click(s, EventArgs.Empty);
            dataGridView3.CellDoubleClick += (s, e) => UpdatePaymentBtn_Click(s, EventArgs.Empty);

            searchBox.TextChanged += ReservationsSearchBox_TextChanged;
            textBox1.TextChanged += CustomersSearchBox_TextChanged;
            textBox4.TextChanged += PaymentsSearchBox_TextChanged;

            button2.Click += ViewCustomerHistoryBtn_Click;

            if (button4 != null)
            {
                button4.Click -= UpdatePaymentBtn_Click;
                button4.Click += UpdatePaymentBtn_Click;
            }

            button5.Click += DelPaymentBtn_Click;

            // Wire delete-customer button safely
            if (DelCustomerBtn != null)
            {
                DelCustomerBtn.Click -= DelCustomerBtn_Click;
                DelCustomerBtn.Click += DelCustomerBtn_Click;
            }
        }

        // configure datagridview appearance and behavior
        private void ConfigureGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoGenerateColumns = true;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Color.FromArgb(220, 220, 220);
            grid.RowTemplate.Height = 36;

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 105, 44);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Bookman Old Style", 14F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersHeight = 48;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.DefaultCellStyle.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular);
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(131, 223, 117);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 244);

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        // configure tab buttons appearance based on selected tab
        private void ButtonSettings()
        {
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(0, 1);

            var selectedColor = Color.FromArgb(131, 223, 117);
            var normalColor = Color.FromArgb(209, 254, 216);

            tab1Btn.BackColor = tabControl1.SelectedIndex == 0 ? selectedColor : normalColor;
            tab2Btn.BackColor = tabControl1.SelectedIndex == 1 ? selectedColor : normalColor;
            button10.BackColor = tabControl1.SelectedIndex == 4 ? selectedColor : normalColor;
        }
     
        private void ResTab_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            ButtonSettings();
        }

        private void tab2Btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            ButtonSettings();
        }

        // prepares the reservation tab for adding a new reservation.
        private void AddResBtn_Click(object sender, EventArgs e)
        {
            try { LoadPackagesIntoCombo(); } catch { /* non-fatal */ }

            editingReservationId = null;
            confirmBtn.Text = "Add Reservation";
            ResClearBtn_Click(null, EventArgs.Empty);
            tabControl1.SelectedIndex = 2;
            ButtonSettings();
        }
        
        private void CustomerClearBtn_Click(object sender, EventArgs e)
        {
            NameBox.Clear();
            ContactBox.Clear();
        }
        
        private void ResClearBtn_Click(object sender, EventArgs e)
        {
            // remember whether we were in edit mode before clearing reservation inputs
            var wasEditing = editingReservationId != null;

            if (packageBox != null) packageBox.SelectedIndex = -1;

            currentPackageMaxGuests = int.MaxValue;
            guestBox.Maximum = 1000;
            guestBox.Value = 0;

            checkInDateBox.Value = DateTime.Now;
            checkOutDateBox.Value = DateTime.Now;
            noteBox.Clear();

            // if we were not editing, reset customer fields to editable and enable Clear button
            if (!wasEditing)
            {     
                if (NameBox != null)
                {
                    NameBox.ReadOnly = false;
                    NameBox.BackColor = SystemColors.Window;
                }
                if (ContactBox != null)
                {
                    ContactBox.ReadOnly = false;
                    ContactBox.BackColor = SystemColors.Window;
                }

                if (CustomerClearBtn != null)
                {
                    CustomerClearBtn.Enabled = true;
                    CustomerClearBtn.BackColor = Color.FromArgb(158, 226, 167);
                }

                editingReservationId = null;
                confirmBtn.Text = "Add Reservation";
            }
            else
            {
                if (CustomerClearBtn != null)
                {
                    CustomerClearBtn.Enabled = false;
                    CustomerClearBtn.BackColor = SystemColors.ControlLight;
                }
            }
        }

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            editingPaymentId = null;

            selectedPaymentProofPath = null;
            selectedPaymentProofBytes = null;
            if (pictureBox1 != null) pictureBox1.Image = null;

            if (textBox6 != null) textBox6.Text = string.Empty;
            if (textBox7 != null) textBox7.Text = string.Empty;
            if (textBox8 != null) textBox8.Text = string.Empty;
            if (comboBox1 != null && comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

            if (DelCustomerBtn != null) DelCustomerBtn.Text = "Confirm Reservation";

            tabControl1.SelectedIndex = 3;
            ButtonSettings();
        }


        private void confirmBtn_Click_1(object sender, EventArgs e)
        {
            var customerName = NameBox.Text.Trim();
            var facebookLink = ContactBox.Text.Trim();
            var packageSelected = packageBox?.Text.Trim() ?? string.Empty;
            var guests = (int)guestBox.Value;
            var checkIn = checkInDateBox.Value.Date;
            var checkOut = checkOutDateBox.Value.Date;
            var notes = noteBox.Text.Trim();

            if (checkOut < checkIn)
            {
                ShowWarning("Check-Out date cannot be earlier than Check-In date.");
                if (checkOutDateBox != null) checkOutDateBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(customerName))
            {
                ShowWarning("Customer name is required.");
                NameBox.Focus();
                return;
            }

            if (editingReservationId == null)
            {
                if (string.IsNullOrWhiteSpace(packageSelected) || packageBox?.SelectedIndex == -1)
                {
                    ShowWarning("Select a package before adding a reservation.");
                    if (packageBox != null) packageBox.Focus();
                    return;
                }
            }

            if (editingReservationId == null)
            {
                try
                {
                    using var conn = CreateConnection();
                    conn.Open();
                    using var tx = conn.BeginTransaction();
                    try
                    {
                        var customerID = FindOrCreateCustomer(conn, tx, customerName, facebookLink);
                        var reservationID = InsertReservation(conn, tx, customerID, packageSelected, guests, checkIn, checkOut, notes);

                        object packagePriceObj = DBNull.Value;
                        if (packageBox?.SelectedItem is DataRowView drv && drv.Row.Table.Columns.Contains("PackagePrice"))
                            packagePriceObj = drv["PackagePrice"] == DBNull.Value ? (object)DBNull.Value : drv["PackagePrice"];

                        const string insertPaymentSql = @"
    INSERT INTO Payment (ReservationID, PackagePrice, PaymentDate, TotalDue, ExtensionFee, AmountPaid, PaymentProof, PaymentStatus)
    VALUES (@reservationID, @packagePrice, NULL, NULL, NULL, NULL, NULL, @paymentStatus);";

                        using (var payCmd = new SqlCommand(insertPaymentSql, conn, tx))
                        {
                            payCmd.Parameters.AddWithValue("@reservationID", reservationID);
                            payCmd.Parameters.AddWithValue("@packagePrice", packagePriceObj ?? DBNull.Value);
                            payCmd.Parameters.AddWithValue("@paymentStatus", "Pending");
                            payCmd.ExecuteNonQuery();
                        }

                        tx.Commit();

                        ShowInfo("Reservation saved.");
                        LoadReservations();
                        LoadPayments();
                        LoadCustomers();
                        ResClearBtn_Click(null, EventArgs.Empty);
                        tabControl1.SelectedIndex = 0;
                        ButtonSettings();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error saving reservation: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    using var conn = CreateConnection();
                    using var cmdFind = new SqlCommand("SELECT CustomerID FROM Reservation WHERE ReservationID = @id", conn);
                    cmdFind.Parameters.AddWithValue("@id", editingReservationId.Value);
                    conn.Open();
                    var cidObj = cmdFind.ExecuteScalar();
                    if (cidObj == null || cidObj == DBNull.Value)
                    {
                        ShowError("Associated reservation/customer not found.");
                        return;
                    }

                    var customerID = Convert.ToInt32(cidObj);

                    using var tx = conn.BeginTransaction();
                    try
                    {
                        using (var updateCust = new SqlCommand(@"
    UPDATE Customers
    SET fullName = @name,
        facebookLink = @link
    WHERE customerID = @cid;", conn, tx))
                        {
                            updateCust.Parameters.AddWithValue("@name", customerName);
                            updateCust.Parameters.AddWithValue("@link", string.IsNullOrWhiteSpace(facebookLink) ? (object)DBNull.Value : facebookLink);
                            updateCust.Parameters.AddWithValue("@cid", customerID);
                            updateCust.ExecuteNonQuery();
                        }

                        using (var updateRes = new SqlCommand(@"
    UPDATE Reservation
    SET PackageName = @package,
        NumGuests = @guests,
        CheckInDate = @ci,
        CheckOutDate = @co,
        SpecialNote = @notes,
        DateModified = SYSUTCDATETIME()
    WHERE ReservationID = @id;", conn, tx))
                        {
                            updateRes.Parameters.AddWithValue("@package", packageSelected);
                            updateRes.Parameters.AddWithValue("@guests", guests);
                            updateRes.Parameters.AddWithValue("@ci", checkIn);
                            updateRes.Parameters.AddWithValue("@co", checkOut);
                            updateRes.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);
                            updateRes.Parameters.AddWithValue("@id", editingReservationId.Value);
                            updateRes.ExecuteNonQuery();
                        }

                        tx.Commit();

                        ShowInfo("Reservation updated.");
                        LoadReservations();
                        LoadPayments();
                        LoadCustomers();

                        if (NameBox != null)
                        {
                            NameBox.ReadOnly = false;
                            NameBox.BackColor = SystemColors.Window;
                        }
                        if (ContactBox != null)
                        {
                            ContactBox.ReadOnly = false;
                            ContactBox.BackColor = SystemColors.Window;
                        }
                        if (CustomerClearBtn != null)
                        {
                            CustomerClearBtn.Enabled = true;
                            CustomerClearBtn.BackColor = Color.FromArgb(158, 226, 167);
                        }

                        editingReservationId = null;
                        ResClearBtn_Click(null, EventArgs.Empty);
                        tabControl1.SelectedIndex = 0;
                        ButtonSettings();
                        confirmBtn.Text = "Add Reservation";
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error updating reservation: " + ex.Message);
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            editingReservationId = null;

            if (NameBox != null)
            {
                NameBox.ReadOnly = false;
                NameBox.BackColor = SystemColors.Window;
            }
            if (ContactBox != null)
            {
                ContactBox.ReadOnly = false;
                ContactBox.BackColor = SystemColors.Window;
            }
            if (CustomerClearBtn != null)
            {
                CustomerClearBtn.Enabled = true;
                CustomerClearBtn.BackColor = Color.FromArgb(158, 226, 167);
            }

            tabControl1.SelectedIndex = 0;
            ButtonSettings();
            confirmBtn.Text = "Add Reservation";
        }

        private void EditResBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                ShowInfo("Select a reservation to edit.");
                return;
            }

            if (!dataGridView1.Columns.Contains("ReservationID"))
            {
                ShowError("ReservationID column not found.");
                return;
            }

            var idObj = dataGridView1.CurrentRow.Cells["ReservationID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var id = Convert.ToInt32(idObj);

            try
            {
                try { LoadPackagesIntoCombo(); } catch { /* non-fatal */ }

                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT R.PackageName, R.NumGuests, R.CheckInDate, R.CheckOutDate, R.SpecialNote,
           C.fullName, C.facebookLink
    FROM Reservation R
    LEFT JOIN Customers C ON R.CustomerID = C.customerID
    WHERE R.ReservationID = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NameBox.Text = rdr["fullName"] != DBNull.Value ? rdr["fullName"].ToString() : string.Empty;
                    // fixed column name usage (was mixing case "facebooklink")
                    ContactBox.Text = rdr["facebookLink"] != DBNull.Value ? rdr["facebookLink"].ToString() : string.Empty;

                    var pkg = rdr["PackageName"] != DBNull.Value ? rdr["PackageName"].ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(pkg) && packageBox?.Items != null)
                    {
                        for (int i = 0; i < packageBox.Items.Count; i++)
                        {
                            if (packageBox.Items[i] is DataRowView drv && drv["PackageName"].ToString() == pkg)
                            {
                                packageBox.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        packageBox.SelectedIndex = -1;
                    }

                    guestBox.Value = rdr["NumGuests"] != DBNull.Value ? Convert.ToDecimal(rdr["NumGuests"]) : 1;
                    checkInDateBox.Value = rdr["CheckInDate"] != DBNull.Value ? Convert.ToDateTime(rdr["CheckInDate"]) : DateTime.Now;
                    checkOutDateBox.Value = rdr["CheckOutDate"] != DBNull.Value ? Convert.ToDateTime(rdr["CheckOutDate"]) : DateTime.Now;
                    noteBox.Text = rdr["SpecialNote"] != DBNull.Value ? rdr["SpecialNote"].ToString() : string.Empty;
                }
                else
                {
                    ShowWarning("Reservation not found.");
                    return;
                }

                if (NameBox != null)
                {
                    NameBox.ReadOnly = true;
                    NameBox.BackColor = SystemColors.ControlLight;
                }
                if (ContactBox != null)
                {
                    ContactBox.ReadOnly = true;
                    ContactBox.BackColor = SystemColors.ControlLight;
                }


                if (CustomerClearBtn != null)
                {
                    CustomerClearBtn.Enabled = false;
                    CustomerClearBtn.BackColor = SystemColors.ControlLight;
                }

                editingReservationId = id;
                confirmBtn.Text = "Update Reservation";
                tabControl1.SelectedIndex = 2;
                ButtonSettings();
            }
            catch (Exception ex)
            {
                ShowError("Error preparing reservation edit: " + ex.Message);
            }
        }

        private void EditCustomerBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                ShowInfo("Select a customer to edit.");
                return;
            }

            if (!dataGridView2.Columns.Contains("customerID"))
            {
                ShowError("CustomerID column not found.");
                return;
            }

            var idObj = dataGridView2.CurrentRow.Cells["customerID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var id = Convert.ToInt32(idObj);

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand("SELECT fullName, facebookLink FROM Customers WHERE customerID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using var rdr = cmd.ExecuteReader();


                editingCustomerId = id;
                SetCustomerButtonsMode(isEdit: true);
                tabControl1.SelectedIndex = 3;
                ButtonSettings();
            }
            catch (Exception ex)
            {
                ShowError("Error preparing customer edit: " + ex.Message);
            }
        }

        // saves the customer data from the customer tab (add or edit).
        private void SaveCustomerFromTab(object sender, EventArgs e)
        {
            var name = NameBox?.Text.Trim() ?? string.Empty;
            var link = ContactBox?.Text.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowWarning("Customer name is required.");
                NameBox?.Focus();
                return;
            }

            try
            {
                using var conn = CreateConnection();
                conn.Open();

                if (editingCustomerId == null)
                {
                    using var cmd = new SqlCommand(@"
    INSERT INTO Customers (fullName, facebookLink, CreatedAt)
    VALUES (@name, @link, SYSUTCDATETIME());", conn);

                    cmd.Parameters.AddWithValue("@name", name);
                    var p = cmd.Parameters.Add("@link", SqlDbType.NVarChar, 500);
                    p.Value = string.IsNullOrWhiteSpace(link) ? (object)DBNull.Value : link;

                    var rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        ShowInfo("Customer added.");
                        LoadCustomers();
                        SetCustomerButtonsMode(isEdit: false);
                        tabControl1.SelectedIndex = 1;
                        ButtonSettings();
                    }
                    else
                    {
                        ShowWarning("Customer not added.");
                    }
                }
                else
                {
                    using var cmd = new SqlCommand(@"
    UPDATE Customers
    SET fullName = @name, facebookLink = @link
    WHERE customerID = @id;", conn);

                    cmd.Parameters.AddWithValue("@name", name);
                    var p = cmd.Parameters.Add("@link", SqlDbType.NVarChar, 500);
                    p.Value = string.IsNullOrWhiteSpace(link) ? (object)DBNull.Value : link;
                    cmd.Parameters.AddWithValue("@id", editingCustomerId.Value);

                    var rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        ShowInfo("Customer updated.");
                        LoadCustomers();
                        editingCustomerId = null;
                        SetCustomerButtonsMode(isEdit: false);
                        tabControl1.SelectedIndex = 1;
                        ButtonSettings();
                    }
                    else
                    {
                        ShowWarning("Customer not updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error saving customer: " + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            ButtonSettings();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Prevent navigation to the Payment Update tab when no payment is selected.
            if (dataGridView3 == null || dataGridView3.CurrentRow == null)
            {
                ShowInfo("Select a payment to update.");
                return;
            }

            if (!dataGridView3.Columns.Contains("PaymentID"))
            {
                ShowError("PaymentID column not found.");
                return;
            }

            var idObj = dataGridView3.CurrentRow.Cells["PaymentID"].Value;
            if (idObj == null || idObj == DBNull.Value)
            {
                ShowInfo("Select a payment to update.");
                return;
            }

            // Delegate to the full preparation routine (reuse existing logic).
            UpdatePaymentBtn_Click(sender, e);
        }

        private void LoadAllGrids()
        {
            LoadReservations();
            LoadCustomers();
            LoadPayments();
        }

        private void LoadReservations()
        {
            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT 
        R.ReservationID, 
        C.fullName AS CustomerName,
        C.facebookLink AS FacebookLink,
        R.PackageName, 
        R.NumGuests, 
        R.CheckInDate, 
        R.CheckOutDate, 
        R.SpecialNote,
        R.DateCreated
    FROM Reservation R
    JOIN Customers C ON R.CustomerID = C.customerID
    ORDER BY R.DateCreated DESC;", conn);

                reservationsTable = FillTable(cmd);
                dataGridView1.DataSource = reservationsTable;

                if (dataGridView1.Columns.Contains("ReservationID"))
                    dataGridView1.Columns["ReservationID"].HeaderText = "Reservation ID";
                if (dataGridView1.Columns.Contains("CustomerName"))
                    dataGridView1.Columns["CustomerName"].HeaderText = "Customer";
                if (dataGridView1.Columns.Contains("FacebookLink"))
                {
                    dataGridView1.Columns["FacebookLink"].HeaderText = "Facebook Link";
                    dataGridView1.Columns["FacebookLink"].Width = 250;
                    dataGridView1.Columns["FacebookLink"].DisplayIndex = dataGridView1.Columns.Contains("CustomerName")
                        ? dataGridView1.Columns["CustomerName"].Index + 1
                        : 2;
                }
                if (dataGridView1.Columns.Contains("PackageName"))
                    dataGridView1.Columns["PackageName"].HeaderText = "Package";
                if (dataGridView1.Columns.Contains("NumGuests"))
                    dataGridView1.Columns["NumGuests"].HeaderText = "Guests";
                if (dataGridView1.Columns.Contains("CheckInDate"))
                {
                    dataGridView1.Columns["CheckInDate"].HeaderText = "Check-In";
                    dataGridView1.Columns["CheckInDate"].DefaultCellStyle.Format = "d";
                }
                if (dataGridView1.Columns.Contains("CheckOutDate"))
                {
                    dataGridView1.Columns["CheckOutDate"].HeaderText = "Check-Out";
                    dataGridView1.Columns["CheckOutDate"].DefaultCellStyle.Format = "d";
                }
                if (dataGridView1.Columns.Contains("SpecialNote"))
                {
                    dataGridView1.Columns["SpecialNote"].HeaderText = "Special Note";
                    dataGridView1.Columns["SpecialNote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dataGridView1.Columns.Contains("DateCreated"))
                {
                    dataGridView1.Columns["DateCreated"].HeaderText = "Created";
                    dataGridView1.Columns["DateCreated"].DefaultCellStyle.Format = "g";
                }

                ConfigureGrid(dataGridView1);
            }
            catch (Exception ex)
            {
                ShowError("Error loading reservations: " + ex.Message);
            }
        }

        //loads customers into the customers grid and configures column headers and widths.
        private void LoadCustomers()
        {
            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
SELECT customerID, fullName, facebookLink
FROM Customers
ORDER BY fullName;", conn);

                customersTable = FillTable(cmd);
                dataGridView2.DataSource = customersTable;

                if (dataGridView2.Columns.Contains("customerID"))
                {
                    dataGridView2.Columns["customerID"].HeaderText = "Customer ID";
                    dataGridView2.Columns["customerID"].Width = 90;
                }

                if (dataGridView2.Columns.Contains("fullName"))
                {
                    dataGridView2.Columns["fullName"].HeaderText = "Full Name";
                    dataGridView2.Columns["fullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dataGridView2.Columns.Contains("facebookLink"))
                {
                    dataGridView2.Columns["facebookLink"].HeaderText = "Facebook Link";
                    dataGridView2.Columns["facebookLink"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                ConfigureGrid(dataGridView2);

                // Ensure UI updates immediately after refresh
                dataGridView2.Refresh();

                // Guard BeginInvoke: only call it when the handle is created
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((Action)(() => dataGridView2.Refresh()));
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading customers: " + ex.Message);
            }
        }

        // loads payments into the payments grid and configures column headers, formats, and widths.
        private void LoadPayments()
        {
            try
            {
                const string imgColName = "ProofImg";
                if (dataGridView3.Columns.Contains(imgColName))
                    dataGridView3.Columns.Remove(imgColName);


                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT 
        PaymentID, 
        ReservationID, 
        PackagePrice, 
        PaymentDate, 
        TotalDue, 
        ExtensionFee, 
        AmountPaid, 
        PaymentProof, 
        PaymentStatus
    FROM Payment
    ORDER BY PaymentDate DESC;", conn);

                paymentsTable = FillTable(cmd);
                dataGridView3.DataSource = paymentsTable;

                if (dataGridView3.Columns.Contains("PaymentID"))
                {
                    dataGridView3.Columns["PaymentID"].HeaderText = "Payment ID";
                    dataGridView3.Columns["PaymentID"].DisplayIndex = 0;
                    dataGridView3.Columns["PaymentID"].Width = 90;
                }

                if (dataGridView3.Columns.Contains("ReservationID"))
                {
                    dataGridView3.Columns["ReservationID"].HeaderText = "Reservation ID";
                    dataGridView3.Columns["ReservationID"].DisplayIndex = 1;
                    dataGridView3.Columns["ReservationID"].Width = 110;
                }

                if (dataGridView3.Columns.Contains("PackagePrice"))
                {
                    dataGridView3.Columns["PackagePrice"].HeaderText = "Package Price";
                    // use "php" prefix instead of currency symbol
                    dataGridView3.Columns["PackagePrice"].DefaultCellStyle.Format = "'php' #,##0.00";
                }

                if (dataGridView3.Columns.Contains("TotalDue"))
                {
                    dataGridView3.Columns["TotalDue"].HeaderText = "Total Due";
                    dataGridView3.Columns["TotalDue"].DefaultCellStyle.Format = "'php' #,##0.00";
                }

                if (dataGridView3.Columns.Contains("ExtensionFee"))
                {
                    dataGridView3.Columns["ExtensionFee"].HeaderText = "Extension Fee";
                    dataGridView3.Columns["ExtensionFee"].DefaultCellStyle.Format = "'php' #,##0.00";
                }

                if (dataGridView3.Columns.Contains("AmountPaid"))
                {
                    dataGridView3.Columns["AmountPaid"].HeaderText = "Amount Paid";
                    dataGridView3.Columns["AmountPaid"].DefaultCellStyle.Format = "'php' #,##0.00";
                }

                if (dataGridView3.Columns.Contains("PaymentDate"))
                {
                    dataGridView3.Columns["PaymentDate"].HeaderText = "Payment Date";
                    dataGridView3.Columns["PaymentDate"].DefaultCellStyle.Format = "g";
                }

                if (dataGridView3.Columns.Contains("PaymentStatus"))
                {
                    dataGridView3.Columns["PaymentStatus"].HeaderText = "Status";
                    dataGridView3.Columns["PaymentStatus"].Width = 150;
                    dataGridView3.Columns["PaymentStatus"].DefaultCellStyle.BackColor = Color.White;
                    dataGridView3.Columns["PaymentStatus"].DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
                }

                if (dataGridView3.Columns.Contains("PaymentProof"))
                {
                    dataGridView3.Columns["PaymentProof"].Width = 110;
                    var proofCol = dataGridView3.Columns["PaymentProof"];

                }


            }
            catch (Exception ex)
            {
                ShowError("Error loading payments: " + ex.Message);
            }
        }

        // It applies filtering to the reservations, customers, or payments grid based on the respective search box input.
        private void ReservationsSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (reservationsTable == null) return;
            var filter = searchBox.Text.Trim().Replace("'", "''");
            reservationsTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"CustomerName LIKE '%{filter}%' OR PackageName LIKE '%{filter}%' OR FacebookLink LIKE '%{filter}%'";

        }

        private void CustomersSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (customersTable == null) return;
            var filter = textBox1.Text.Trim().Replace("'", "''");
            customersTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"fullName LIKE '%{filter}%' OR facebooklink LIKE '%{filter}%'";
        }

        private void PaymentsSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (paymentsTable == null) return;
            var filter = textBox4.Text.Trim().Replace("'", "''");
            paymentsTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"Convert(ReservationID, 'System.String') LIKE '%{filter}%'";
        }

        private void DelResBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                ShowInfo("Select a reservation to delete.");
                return;
            }

            if (!dataGridView1.Columns.Contains("ReservationID"))
            {
                ShowError("ReservationID column not found.");
                return;
            }

            var idObj = dataGridView1.CurrentRow.Cells["ReservationID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var id = Convert.ToInt32(idObj);
            var name = dataGridView1.CurrentRow.Cells["CustomerName"].Value?.ToString() ?? id.ToString();

            var confirm = MessageBox.Show($"Delete reservation for \"{name}\" (ID {id})? This will also delete associated payment records.", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = CreateConnection();
                conn.Open();
                using var tx = conn.BeginTransaction();
                try
                {
                    using (var delPay = new SqlCommand("DELETE FROM Payment WHERE ReservationID = @id", conn, tx))
                    {
                        delPay.Parameters.AddWithValue("@id", id);
                        delPay.ExecuteNonQuery();
                    }

                    using (var delRes = new SqlCommand("DELETE FROM Reservation WHERE ReservationID = @id", conn, tx))
                    {
                        delRes.Parameters.AddWithValue("@id", id);
                        var rows = delRes.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            tx.Commit();
                            ShowInfo("Reservation and related payments deleted.");
                            LoadReservations();
                            LoadPayments();
                        }
                        else
                        {
                            tx.Rollback();
                            ShowWarning("Reservation not found or not deleted.");
                        }
                    }
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting reservation: " + ex.Message);
            }
        }

        
        private void DelPaymentBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null)
            {
                ShowInfo("Select a payment to delete.");
                return;
            }

            if (!dataGridView3.Columns.Contains("PaymentID"))
            {
                ShowError("PaymentID column not found.");
                return;
            }

            var idObj = dataGridView3.CurrentRow.Cells["PaymentID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var paymentId = Convert.ToInt32(idObj);

            // Try to obtain associated ReservationID if present
            int? reservationId = null;
            if (dataGridView3.Columns.Contains("ReservationID"))
            {
                var ridObj = dataGridView3.CurrentRow.Cells["ReservationID"].Value;
                if (ridObj != null && ridObj != DBNull.Value)
                {
                    reservationId = Convert.ToInt32(ridObj);
                }
            }

            var confirm = MessageBox.Show($"Delete payment record (ID {paymentId})?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            // if there is an associated reservation, ask whether to delete it as well
            var deleteReservation = false;
            if (reservationId.HasValue)
            {
                var ask = MessageBox.Show($"Also delete the associated reservation (ID {reservationId.Value})?\n\n" +
                                          "Choose Yes to delete both payment and reservation, No to delete payment only.",
                                          "Delete associated reservation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                deleteReservation = (ask == DialogResult.Yes);
            }

            try
            {
                using var conn = CreateConnection();
                conn.Open();
                using var tx = conn.BeginTransaction();
                try
                {
                    // Delete the payment first
                    using (var delPay = new SqlCommand("DELETE FROM Payment WHERE PaymentID = @id", conn, tx))
                    {
                        delPay.Parameters.AddWithValue("@id", paymentId);
                        var payRows = delPay.ExecuteNonQuery();
                        if (payRows == 0)
                        {
                            tx.Rollback();
                            ShowWarning("Payment not found or not deleted.");
                            return;
                        }
                    }

                    // Optionally delete the reservation that this payment belonged to
                    if (deleteReservation && reservationId.HasValue)
                    {
                        using var delRes = new SqlCommand("DELETE FROM Reservation WHERE ReservationID = @rid", conn, tx);
                        delRes.Parameters.AddWithValue("@rid", reservationId.Value);
                        var resRows = delRes.ExecuteNonQuery();
                        if (resRows == 0)
                        {
                            // reservation not found but still commits payment deletion but inform the user
                            tx.Commit();
                            ShowWarning("Payment deleted but associated reservation was not found.");
                            LoadPayments();
                            LoadReservations();
                            return;
                        }

                        tx.Commit();
                        ShowInfo("Payment and associated reservation deleted.");
                        LoadPayments();
                        LoadReservations();
                        return;
                    }

                    
                    tx.Commit();
                    ShowInfo("Payment deleted.");
                    LoadPayments();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting payment: " + ex.Message);
            }
        }

        private void UpdatePaymentBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null)
            {
                ShowInfo("Select a payment to update.");
                return;
            }

            if (!dataGridView3.Columns.Contains("PaymentID"))
            {
                ShowError("PaymentID column not found.");
                return;
            }

            var idObj = dataGridView3.CurrentRow.Cells["PaymentID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var id = Convert.ToInt32(idObj);

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
SELECT PaymentID, ReservationID, PackagePrice, AmountPaid, PaymentDate, PaymentProof, PaymentStatus, TotalDue, ExtensionFee
FROM Payment
WHERE PaymentID = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    var amount = rdr["AmountPaid"] != DBNull.Value ? Convert.ToDecimal(rdr["AmountPaid"]) : 0m;
                    var totalDue = rdr["TotalDue"] != DBNull.Value ? Convert.ToDecimal(rdr["TotalDue"]) : 0m;
                    var extFee = rdr["ExtensionFee"] != DBNull.Value ? Convert.ToDecimal(rdr["ExtensionFee"]) : 0m;
                    if (textBox6 != null) textBox6.Text = totalDue != 0m ? FormatCurrencyString(totalDue) : string.Empty;
                    if (textBox7 != null) textBox7.Text = extFee != 0m ? FormatCurrencyString(extFee) : string.Empty;
                    if (textBox8 != null) textBox8.Text = amount != 0m ? FormatCurrencyString(amount) : string.Empty;

                    var packagePrice = rdr["PackagePrice"] != DBNull.Value ? Convert.ToDecimal(rdr["PackagePrice"]) : 0m;
                    if (label24 != null) label24.Text = packagePrice != 0m ? FormatCurrencyString(packagePrice) : string.Empty;

                    if (dateTimePicker1 != null)
                        dateTimePicker1.Value = rdr["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(rdr["PaymentDate"]) : DateTime.Now;

                    var status = rdr["PaymentStatus"] != DBNull.Value ? rdr["PaymentStatus"].ToString() : "Pending";
                    if (comboBox1 != null)
                    {
                        if (comboBox1.Items.Contains(status))
                            comboBox1.SelectedItem = status;
                        else
                            comboBox1.SelectedIndex = 0;
                    }

                    selectedPaymentProofPath = null;
                    selectedPaymentProofBytes = null;
                    if (pictureBox1 != null) pictureBox1.Image = null;

                    var proofObj = rdr["PaymentProof"];
                    LoadImageFromObject(proofObj, ref selectedPaymentProofPath, ref selectedPaymentProofBytes, pictureBox1);

                    if (pictureBox1 != null) pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    if (button6 != null) button6.Text = "Browse...";
                }
                else
                {
                    ShowWarning("Payment not found.");
                    return;
                }

                editingPaymentId = id;
                DelCustomerBtn.Text = "Update Payment";
                tabControl1.SelectedIndex = 4;
                ButtonSettings();
            }
            catch (Exception ex)
            {
                ShowError("Error preparing payment edit: " + ex.Message);
            }
        }

        // it saves the payment data from the payment update tab.
        private void SavePaymentFromTab(object sender, EventArgs e)
        {
            if (editingPaymentId == null)
            {
                ShowWarning("No payment selected for update.");
                return;
            }

            var amount = ParseCurrencyString(textBox8?.Text);
            var payDate = dateTimePicker1.Value;
            var status = comboBox1?.SelectedItem?.ToString() ?? "Pending";
            var totalDue = ParseCurrencyString(textBox6?.Text);
            var extensionFee = ParseCurrencyString(textBox7?.Text);

            if (totalDue > 0m && amount > totalDue)
            {
                var message = $"Amount paid ({FormatCurrencyString(amount)}) is greater than Total Due ({FormatCurrencyString(totalDue)}).\n\n" +
                              "Choose Yes to Continue (save) or No to Go Back and change the values.";
                var choice = MessageBox.Show(message, "Confirm overpayment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (choice != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                using var conn = CreateConnection();
                conn.Open();
                using var tx = conn.BeginTransaction();
                try
                {
                    using var cmd = new SqlCommand(@"
UPDATE Payment
SET AmountPaid = @amount,
    PaymentDate = @pdate,
    PaymentStatus = @status,
    TotalDue = @totalDue,
    ExtensionFee = @extFee,
    PaymentProof = @proof
WHERE PaymentID = @id;", conn, tx);

                    var pAmount = cmd.Parameters.Add("@amount", System.Data.SqlDbType.Decimal);
                    pAmount.Precision = 18;
                    pAmount.Scale = 2;
                    pAmount.Value = amount;

                    cmd.Parameters.Add("@pdate", System.Data.SqlDbType.DateTime2).Value = payDate;
                    cmd.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 50).Value = status;

                    if (totalDue == 0m)
                        cmd.Parameters.Add("@totalDue", System.Data.SqlDbType.Decimal).Value = DBNull.Value;
                    else
                    {
                        var pTotal = cmd.Parameters.Add("@totalDue", System.Data.SqlDbType.Decimal);
                        pTotal.Precision = 18;
                        pTotal.Scale = 2;
                        pTotal.Value = totalDue;
                    }

                    if (extensionFee == 0m)
                        cmd.Parameters.Add("@extFee", System.Data.SqlDbType.Decimal).Value = DBNull.Value;
                    else
                    {
                        var pExt = cmd.Parameters.Add("@extFee", System.Data.SqlDbType.Decimal);
                        pExt.Precision = 18;
                        pExt.Scale = 2;
                        pExt.Value = extensionFee;
                    }

                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = editingPaymentId.Value;

                    byte[] proofBytes = null;
                    if (selectedPaymentProofBytes != null && selectedPaymentProofBytes.Length > 0)
                        proofBytes = selectedPaymentProofBytes;
                    else if (!string.IsNullOrEmpty(selectedPaymentProofPath) && System.IO.File.Exists(selectedPaymentProofPath))
                        proofBytes = System.IO.File.ReadAllBytes(selectedPaymentProofPath);

                    var proofParam = cmd.Parameters.Add("@proof", System.Data.SqlDbType.VarBinary, -1);
                    proofParam.Value = (proofBytes != null && proofBytes.Length > 0) ? (object)proofBytes : DBNull.Value;

                    var rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        tx.Commit();
                        ShowInfo("Payment updated.");
                        LoadPayments();

                        editingPaymentId = null;
                        tabControl1.SelectedIndex = 3;
                    }
                    else
                    {
                        tx.Rollback();
                        ShowWarning("Payment not updated. No rows affected.");
                    }
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error saving payment: " + ex.Message);
            }
        }

        // it loads active packages into the packageBox ComboBox.
        private void LoadPackagesIntoCombo()
        {
            if (packageBox == null) return;

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT PackageID, PackageName, PackagePrice, MaxGuests, Active, Details
    FROM Packages
    WHERE Active = 1
    ORDER BY PackageName;", conn);

                var dt = FillTable(cmd);

                packageBox.DisplayMember = "PackageName";
                packageBox.ValueMember = "PackageID";
                packageBox.DataSource = dt;

                // make it selection-only
                packageBox.SelectedIndex = -1;
                packageBox.DropDownStyle = ComboBoxStyle.DropDownList;
                packageBox.FlatStyle = FlatStyle.Flat;
                packageBox.BackColor = Color.White;
                packageBox.ForeColor = Color.FromArgb(33, 33, 33);

                currentPackageMaxGuests = int.MaxValue;
                guestBox.Maximum = 1000;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load packages: " + ex.Message, ex);
            }
        }

        // it updates the guestBox maximum based on the selected package's MaxGuests value.
        private void PackageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (packageBox?.SelectedItem is DataRowView drv)
                {
                    var maxObj = drv.Row.Table.Columns.Contains("MaxGuests") ? drv["MaxGuests"] : null;
                    if (maxObj != null && maxObj != DBNull.Value && int.TryParse(maxObj.ToString(), out var max) && max > 0)
                    {
                        currentPackageMaxGuests = max;
                        guestBox.Maximum = Math.Min(max, 1000);
                        if (guestBox.Value > guestBox.Maximum)
                            guestBox.Value = guestBox.Maximum;
                    }
                    else
                    {
                        currentPackageMaxGuests = int.MaxValue;
                        guestBox.Maximum = 1000;
                    }
                }
                else
                {
                    currentPackageMaxGuests = int.MaxValue;
                    guestBox.Maximum = 1000;
                }
            }
            catch
            {
                currentPackageMaxGuests = int.MaxValue;
                guestBox.Maximum = 1000;
            }
        }

        // it allows browsing and selecting an image file as payment proof.
        private void PaymentProofBrowse_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog()
            {
                Title = "Select payment proof image",
                Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*",
                Multiselect = false
            };

            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var path = ofd.FileName;
                var bytes = File.ReadAllBytes(path);
                selectedPaymentProofPath = path;
                selectedPaymentProofBytes = bytes;

                using var ms = new MemoryStream(bytes);
                using var tmp = Image.FromStream(ms);
                pictureBox1.Image = new Bitmap(tmp);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                ShowError("Failed to load image: " + ex.Message);
            }
        }

        // it loads an image from either a byte array or a file path stored in the database.
        private void LoadImageFromObject(object proofObj, ref string pathCache, ref byte[] bytesCache, PictureBox target)
        {
            try
            {
                pathCache = null;
                bytesCache = null;
                if (target != null) target.Image = null;

                if (proofObj == DBNull.Value || proofObj == null) return;

                if (proofObj is byte[] b && b.Length > 0)
                {
                    bytesCache = b;
                    using var ms = new MemoryStream(b);
                    using var tmp = Image.FromStream(ms);
                    if (target != null) target.Image = new Bitmap(tmp);
                }
                else
                {
                    var path = proofObj.ToString();
                    pathCache = path;
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        var bytes = File.ReadAllBytes(path);
                        bytesCache = bytes;
                        using var ms2 = new MemoryStream(bytes);
                        using var tmp2 = Image.FromStream(ms2);
                        if (target != null) target.Image = new Bitmap(tmp2);
                    }
                }
            }
            catch
            {
                if (target != null) target.Image = null;
            }
        }
        //it formats the currency text box on leave event.
        private void CurrencyTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                var d = ParseCurrencyString(tb.Text);
                tb.Text = d != 0m ? FormatCurrencyString(d) : string.Empty;
            }
        }

        // it restricts input to digits, decimal separator, and group separator.
        private void CurrencyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            var decimalSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var groupSep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

            if (char.IsDigit(e.KeyChar) ||
                e.KeyChar.ToString() == decimalSep ||
                e.KeyChar.ToString() == groupSep)
            {
                return;
            }
            e.Handled = true;
        }
        // it formats a decimal value into a currency string with "php" prefix.
        private string FormatCurrencyString(decimal value)
        {
            // Use "php" prefix with current-culture formatting for separators
            if (value == Math.Truncate(value))
                return $"php {value.ToString("N0", CultureInfo.CurrentCulture)}";
            return $"php {value.ToString("N2", CultureInfo.CurrentCulture)}";
        }

        // it parses a currency string (with or without "php" prefix) into a decimal value.
        private decimal ParseCurrencyString(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;

            
            var cleaned = text.Replace("php", "", StringComparison.OrdinalIgnoreCase).Trim();

            if (decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var v))
                return v;

            var fallback = "";
            foreach (var ch in cleaned)
                if (char.IsDigit(ch) || ch == '.' || ch == '-' || ch == ',') fallback += ch;

            if (decimal.TryParse(fallback, NumberStyles.Number, CultureInfo.InvariantCulture, out v))
                return v;

            if (decimal.TryParse(fallback, NumberStyles.Number, CultureInfo.CurrentCulture, out v))
                return v;

            return 0m;
        }

        // sets up and returns a new SQL connection using the stored connection string.
        private SqlConnection CreateConnection() => new SqlConnection(connString);
        // it fills a DataTable using the provided SqlCommand.
        private DataTable FillTable(SqlCommand cmd)
        {
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        //finds an existing customer by full name or Facebook link, or creates a new customer if not found.
        private int FindOrCreateCustomer(SqlConnection conn, SqlTransaction tx, string fullName, string facebookLink)
        {
            using (var checkByName = new SqlCommand("SELECT customerID FROM Customers WHERE fullName = @name", conn, tx))
            {
                checkByName.Parameters.AddWithValue("@name", fullName);
                var idObj = checkByName.ExecuteScalar();
                if (idObj != null && idObj != DBNull.Value)
                {
                    return Convert.ToInt32(idObj);
                }
            }

            // if a nonempty facebookLink was supplied, try to find by it
            if (!string.IsNullOrWhiteSpace(facebookLink))
            {
                using (var checkByLink = new SqlCommand("SELECT customerID FROM Customers WHERE facebookLink = @link", conn, tx))
                {
                    checkByLink.Parameters.AddWithValue("@link", facebookLink);
                    var idObj2 = checkByLink.ExecuteScalar();
                    if (idObj2 != null && idObj2 != DBNull.Value)
                    {
                        return Convert.ToInt32(idObj2);
                    }
                }
            }

            const string insertCustomerSql = @"
    INSERT INTO Customers (fullName, facebookLink, CreatedAt)
    VALUES (@name, @link, SYSUTCDATETIME());
    SELECT SCOPE_IDENTITY();";
            using var insertCmd = new SqlCommand(insertCustomerSql, conn, tx);
            insertCmd.Parameters.AddWithValue("@name", fullName);
            var p = insertCmd.Parameters.Add("@link", SqlDbType.NVarChar, 500);
            p.Value = string.IsNullOrWhiteSpace(facebookLink) ? (object)DBNull.Value : facebookLink;
            var newIdObj = insertCmd.ExecuteScalar();
            return Convert.ToInt32(newIdObj);
        }

        private int InsertReservation(SqlConnection conn, SqlTransaction tx, int customerID, string packageSelected, int guests, DateTime checkIn, DateTime checkOut, string notes)
        {
            const string insertReservationSql = @"
    INSERT INTO Reservation
        (CustomerID, PackageName, NumGuests, CheckInDate, CheckOutDate, SpecialNote, BookingStatus, DateCreated, DateModified)
    VALUES
        (@customerID, @package, @guests, @ci, @co, @notes, @bStatus, SYSUTCDATETIME(), SYSUTCDATETIME());
    SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(insertReservationSql, conn, tx);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@package", packageSelected);
            cmd.Parameters.AddWithValue("@guests", guests);
            cmd.Parameters.AddWithValue("@ci", checkIn);
            cmd.Parameters.AddWithValue("@co", checkOut);
            cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);
            cmd.Parameters.AddWithValue("@bStatus", "New");

            var resIdObj = cmd.ExecuteScalar();
            return Convert.ToInt32(resIdObj);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // displays the selected customer's reservation and payment history in a new dialog.
        private void ViewCustomerHistoryBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                ShowInfo("Select a customer to view history.");
                return;
            }

            if (!dataGridView2.Columns.Contains("customerID"))
            {
                ShowError("CustomerID column not found.");
                return;
            }

            var idObj = dataGridView2.CurrentRow.Cells["customerID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var customerId = Convert.ToInt32(idObj);

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT 
        R.ReservationID,
        R.PackageName,
        R.CheckInDate,
        R.CheckOutDate,
        R.NumGuests,
        R.BookingStatus,
        P.PaymentStatus,
        P.AmountPaid,
        P.PaymentDate
    FROM Reservation R
    LEFT JOIN Payment P ON R.ReservationID = P.ReservationID
    WHERE R.CustomerID = @cid
    ORDER BY R.CheckInDate DESC;", conn);
                cmd.Parameters.AddWithValue("@cid", customerId);

                var dt = FillTable(cmd);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowInfo("No reservations found for this customer.");
                    return;
                }

                using var frm = new Form
                {
                    Text = $"Customer History - {dataGridView2.CurrentRow.Cells["fullName"]?.Value?.ToString() ?? "Customer"}",
                    Size = new Size(900, 600),
                    StartPosition = FormStartPosition.CenterParent
                };

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    DataSource = dt,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false
                };

                frm.Controls.Add(dgv);
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ShowError("Error loading customer history: " + ex.Message);
            }
        }

        private void SetCustomerButtonsMode(bool isEdit)
        {
            if (saveCustomerBtn != null)
            {
                saveCustomerBtn.Text = isEdit ? "Update" : "Save";
                saveCustomerBtn.BackColor = isEdit ? Color.FromArgb(60, 62, 128) : Color.FromArgb(37, 105, 44);
                saveCustomerBtn.ForeColor = Color.White;
            }
            if (cancelCustomerBtn != null)
            {
                cancelCustomerBtn.Text = "Cancel";
                cancelCustomerBtn.BackColor = Color.FromArgb(154, 35, 38);
                cancelCustomerBtn.ForeColor = Color.White;
            }
        }

        private void OrchardLogo_Click(object sender, EventArgs e) { }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to log out?", "Confirm log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                // Open a fresh login form and close this main form
                var login = new LoginForm();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open login form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.Close();
        }

        private void DelCustomerBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView2 == null || dataGridView2.CurrentRow == null)
            {
                ShowInfo("Select a customer to delete.");
                return;
            }

            if (!dataGridView2.Columns.Contains("customerID"))
            {
                ShowError("CustomerID column not found.");
                return;
            }

            var idObj = dataGridView2.CurrentRow.Cells["customerID"].Value;
            if (idObj == null || idObj == DBNull.Value) return;
            var id = Convert.ToInt32(idObj);
            var name = dataGridView2.CurrentRow.Cells["fullName"].Value?.ToString() ?? id.ToString();

            var confirm = MessageBox.Show($"Delete customer \"{name}\" (ID {id})? This may also remove related reservations/payments depending on DB constraints.", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand("DELETE FROM Customers WHERE customerID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ShowInfo("Customer deleted.");
                    LoadCustomers();
                    LoadReservations();
                    LoadPayments();
                }
                else
                {
                    ShowWarning("Customer not found or not deleted.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting customer: " + ex.Message);
            }
        }

    }
}