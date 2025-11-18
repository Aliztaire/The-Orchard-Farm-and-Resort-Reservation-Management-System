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
        // track current package max guests (default large)
        private int currentPackageMaxGuests = int.MaxValue;

        // in-memory caches (DataTables) bound to grids
        private DataTable reservationsTable;
        private DataTable customersTable;
        private DataTable paymentsTable;

        // editing state for tabs (null means Add mode)
        private int? editingReservationId;
        private int? editingCustomerId;
        private int? editingPaymentId;

        // runtime-created buttons for customer tab (designer didn't include save/cancel)
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

            // wire events and configure visuals/behaviour
            WireUpEvents();
            ConfigureGrid(dataGridView1);
            ConfigureGrid(dataGridView2);
            ConfigureGrid(dataGridView3);

            // runtime only buttons for the customer details panel (kept minimal here;
            // designer file contains all static design)
            CreateCustomerTabButtons();

            // load data
            LoadAllGrids();

            // initialize payment status choices
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "Pending", "Paid", "Canceled" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.BackColor = Color.White;
            comboBox1.ForeColor = Color.FromArgb(33, 33, 33);

            if (pictureBox1 != null) pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // wire a small set of runtime handlers that are not part of designer
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

            // small text updates kept here so designer remains pure layout
            if (label22 != null) label22.Text = "Total Due";
            if (label23 != null) label23.Text = "Extension Fee";
        }

        /// <summary>
        /// Wire runtime events that attach UI actions to handlers.
        /// Designer defines controls; this method only attaches behaviour.
        /// </summary>
        private void WireUpEvents()
        {
            packageBox.SelectedIndexChanged += PackageBox_SelectedIndexChanged;

            AddResBtn.Click += AddResBtn_Click;
            DelResBtn.Click += DelResBtn_Click;
            EditResBtn.Click += EditResBtn_Click;

            dataGridView1.RowHeaderMouseDoubleClick += (s, e) => EditResBtn_Click(s, EventArgs.Empty);
            dataGridView1.CellDoubleClick += (s, e) => EditResBtn_Click(s, EventArgs.Empty);

            dataGridView2.RowHeaderMouseDoubleClick += (s, e) => EditCustomerBtn_Click(s, EventArgs.Empty);
            dataGridView2.CellDoubleClick += (s, e) => EditCustomerBtn_Click(s, EventArgs.Empty);

            dataGridView3.RowHeaderMouseDoubleClick += (s, e) => UpdatePaymentBtn_Click(s, EventArgs.Empty);
            dataGridView3.CellDoubleClick += (s, e) => UpdatePaymentBtn_Click(s, EventArgs.Empty);

            searchBox.TextChanged += ReservationsSearchBox_TextChanged;
            textBox1.TextChanged += CustomersSearchBox_TextChanged;
            textBox4.TextChanged += PaymentsSearchBox_TextChanged;

            // buttons declared in designer - attach handlers here
            button1.Click += EditCustomerBtn_Click;
            button2.Click += ViewCustomerHistoryBtn_Click;
            button4.Click += UpdatePaymentBtn_Click;
            button5.Click += DelPaymentBtn_Click;

            button8.Click += SavePaymentFromTab;
            button7.Click += (s, e) => { editingPaymentId = null; tabControl1.SelectedIndex = 4; ButtonSettings(); };
        }

        /// <summary>
        /// Create the small runtime-only Save / Cancel buttons used on the customer details tab.
        /// These are not designer-managed so they are created here.
        /// </summary>
        private void CreateCustomerTabButtons()
        {
            // Save
            saveCustomerBtn = new Button
            {
                Name = "saveCustomerBtn",
                Text = "Save",
                Width = 120,
                Height = 34,
                Left = 950,
                Top = 178,
                BackColor = Color.FromArgb(37, 105, 44),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Visible = true
            };
            saveCustomerBtn.FlatAppearance.BorderSize = 0;
            saveCustomerBtn.Click += SaveCustomerFromTab;

            // Cancel
            cancelCustomerBtn = new Button
            {
                Name = "cancelCustomerBtn",
                Text = "Cancel",
                Width = 120,
                Height = 34,
                Left = 1080,
                Top = 178,
                BackColor = Color.FromArgb(154, 35, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Visible = true
            };
            cancelCustomerBtn.FlatAppearance.BorderSize = 0;
            cancelCustomerBtn.Click += (s, e) =>
            {
                editingCustomerId = null;
                SetCustomerButtonsMode(isEdit: false);
                ClearCustomerFormInputs();
                tabControl1.SelectedIndex = 1;
                ButtonSettings();
            };

            if (panel4 != null)
            {
                if (!panel4.Controls.Contains(saveCustomerBtn)) panel4.Controls.Add(saveCustomerBtn);
                if (!panel4.Controls.Contains(cancelCustomerBtn)) panel4.Controls.Add(cancelCustomerBtn);
                saveCustomerBtn.BringToFront();
                cancelCustomerBtn.BringToFront();
            }
            else
            {
                if (!Controls.Contains(saveCustomerBtn)) Controls.Add(saveCustomerBtn);
                if (!Controls.Contains(cancelCustomerBtn)) Controls.Add(cancelCustomerBtn);
                saveCustomerBtn.BringToFront();
                cancelCustomerBtn.BringToFront();
            }
        }

        /// <summary>
        /// Apply consistent appearance and behaviour to a DataGridView.
        /// Designer supplies control instance; method configures runtime behavior.
        /// </summary>
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

        /// <summary>
        /// Update tab button colours to reflect current selected tab.
        /// </summary>
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

        private void MainAdminForm_Load(object sender, EventArgs e) { }

        /// <summary>
        /// Open the Add Reservation tab in Add mode.
        /// </summary>
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
            if (packageBox != null) packageBox.SelectedIndex = -1;

            currentPackageMaxGuests = int.MaxValue;
            guestBox.Maximum = 1000;
            guestBox.Value = 0;

            checkInDateBox.Value = DateTime.Now;
            checkOutDateBox.Value = DateTime.Now;
            noteBox.Clear();

            // Clear customer fields as well when opening Add Reservation
            if (NameBox != null) NameBox.Clear();
            if (ContactBox != null) ContactBox.Clear();

            editingReservationId = null;
            confirmBtn.Text = "Add Reservation";
        }

        private void cancelBtn_Click_1(object sender, EventArgs e) { }

        /// <summary>
        /// Handles both Add and Update for reservations depending on editingReservationId.
        /// Validates input, uses transactions for multi-table updates and reloads relevant grids.
        /// </summary>
        private void confirmBtn_Click_1(object sender, EventArgs e)
        {
            var customerName = NameBox.Text.Trim();
            var facebookLink = ContactBox.Text.Trim();
            var packageSelected = packageBox?.Text.Trim() ?? string.Empty;
            var guests = (int)guestBox.Value;
            var checkIn = checkInDateBox.Value.Date;
            var checkOut = checkOutDateBox.Value.Date;
            var notes = noteBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                ShowWarning("Customer name is required.");
                NameBox.Focus();
                return;
            }

            // Require a package to be selected for new reservations
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
                // Add flow
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
    VALUES (@reservationID, @packagePrice, SYSUTCDATETIME(), NULL, NULL, NULL, NULL, @paymentStatus);";

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
                        // Ensure customers grid is updated after adding a reservation (new customer may have been created)
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
                // Update flow
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
                        // refresh customers grid because we updated the customer record above
                        LoadCustomers();
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
            tabControl1.SelectedIndex = 0;
            ButtonSettings();
            confirmBtn.Text = "Add Reservation";
        }

        /// <summary>
        /// Load reservation into the reservation edit tab so the user can update it.
        /// </summary>
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

        /// <summary>
        /// Prepare customer details tab for editing the selected customer.
        /// Populates fields and changes runtime Save/Cancel button appearance.
        /// </summary>
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
                if (rdr.Read())
                {
                    textBox2.Text = rdr["fullName"] != DBNull.Value ? rdr["fullName"].ToString() : string.Empty;
                    textBox3.Text = rdr["facebookLink"] != DBNull.Value ? rdr["facebookLink"].ToString().Trim() : string.Empty;
                }
                else
                {
                    ShowWarning("Customer not found.");
                    return;
                }

                editingCustomerId = id;
                SetCustomerButtonsMode(isEdit: true);
                tabControl1.SelectedIndex = 3;
                ButtonSettings();
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                ShowError("Error preparing customer edit: " + ex.Message);
            }
        }

        /// <summary>
        /// Add or update a customer depending on editingCustomerId.
        /// Updates the customers grid and resets runtime state.
        /// </summary>
        private void SaveCustomerFromTab(object sender, EventArgs e)
        {
            var name = textBox2.Text.Trim();
            var link = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowWarning("Customer name is required.");
                textBox2.Focus();
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
                        ClearCustomerFormInputs();
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
                        ClearCustomerFormInputs();
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
            tabControl1.SelectedIndex = 4;
            ButtonSettings();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }

        /// <summary>
        /// Load all three grids (reservations, customers, payments).
        /// </summary>
        private void LoadAllGrids()
        {
            LoadReservations();
            LoadCustomers();
            LoadPayments();
        }

        /// <summary>
        /// Load reservations into the reservations grid and set column formatting/headers.
        /// </summary>
        private void LoadReservations()
        {
            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
    SELECT 
        R.ReservationID, 
        C.fullName AS CustomerName,
        R.PackageName, 
        R.PaymentStatus, 
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
                if (dataGridView1.Columns.Contains("PackageName"))
                    dataGridView1.Columns["PackageName"].HeaderText = "Package";
                if (dataGridView1.Columns.Contains("PaymentStatus"))
                    dataGridView1.Columns["PaymentStatus"].HeaderText = "Payment Status";
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

        /// <summary>
        /// Load customers into the customers grid and set column formatting.
        /// </summary>
        private void LoadCustomers()
        {
            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand(@"
SELECT customerID, fullName, facebookLink, CreatedAt
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
                    dataGridView2.Columns["facebookLink"].Width = 300;
                }

                if (dataGridView2.Columns.Contains("CreatedAt"))
                {
                    dataGridView2.Columns["CreatedAt"].HeaderText = "Created";
                    dataGridView2.Columns["CreatedAt"].DefaultCellStyle.Format = "g";
                    dataGridView2.Columns["CreatedAt"].Width = 140;
                }

                ConfigureGrid(dataGridView2);

                // Ensure UI updates immediately after refresh to avoid perceived delay
                dataGridView2.Refresh();

                // Only call BeginInvoke if the form's handle is created
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

        /// <summary>
        /// Load payments into the payments grid, hide raw proof column and prepare image thumbnails.
        /// </summary>
        private void LoadPayments()
        {
            try
            {
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

                // hide raw storage column (if auto-created)
                if (dataGridView3.Columns.Contains("PaymentProof"))
                {
                    var proofCol = dataGridView3.Columns["PaymentProof"];
                    if (proofCol is DataGridViewImageColumn)
                        dataGridView3.Columns.Remove(proofCol);
                    else
                        proofCol.Visible = false;
                }

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
                    dataGridView3.Columns["PaymentStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView3.Columns["PaymentStatus"].DefaultCellStyle.BackColor = Color.White;
                    dataGridView3.Columns["PaymentStatus"].DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
                }

                ConfigureGrid(dataGridView3);

                PopulatePaymentProofImages();
            }
            catch (Exception ex)
            {
                ShowError("Error loading payments: " + ex.Message);
            }
        }

        // Search/filter handlers (client-side filter via DataView)
        private void ReservationsSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (reservationsTable == null) return;
            var filter = searchBox.Text.Trim().Replace("'", "''");
            reservationsTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"CustomerName LIKE '%{filter}%' OR PackageName LIKE '%{filter}%'";
        }

        private void CustomersSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (customersTable == null) return;
            var filter = textBox1.Text.Trim().Replace("'", "''");
            customersTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"fullName LIKE '%{filter}%' OR facebookLink LIKE '%{filter}%'";
        }

        private void PaymentsSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (paymentsTable == null) return;
            var filter = textBox4.Text.Trim().Replace("'", "''");
            paymentsTable.DefaultView.RowFilter = string.IsNullOrEmpty(filter)
                ? string.Empty
                : $"Convert(ReservationID, 'System.String') LIKE '%{filter}%'";
        }

        /// <summary>
        /// Delete the selected reservation (with confirmation).
        /// </summary>
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

            var confirm = MessageBox.Show($"Delete reservation for \"{name}\" (ID {id})?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand("DELETE FROM Reservation WHERE ReservationID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ShowInfo("Reservation deleted.");
                    LoadReservations();
                }
                else
                {
                    ShowWarning("Reservation not found or not deleted.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting reservation: " + ex.Message);
            }
        }

        /// <summary>
        /// Delete the selected payment (with confirmation).
        /// </summary>
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
            var id = Convert.ToInt32(idObj);

            var confirm = MessageBox.Show($"Delete payment record (ID {id})?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = CreateConnection();
                using var cmd = new SqlCommand("DELETE FROM Payment WHERE PaymentID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ShowInfo("Payment deleted.");
                    LoadPayments();
                }
                else
                {
                    ShowWarning("Payment not found or not deleted.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting payment: " + ex.Message);
            }
        }

        /// <summary>
        /// Open payment edit tab and populate fields for the selected payment.
        /// </summary>
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
    SELECT PaymentID, ReservationID, AmountPaid, PaymentDate, PaymentProof, PaymentStatus, TotalDue, ExtensionFee
    FROM Payment
    WHERE PaymentID = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    var totalDue = rdr["TotalDue"] != DBNull.Value ? Convert.ToDecimal(rdr["TotalDue"]) : 0m;
                    var extFee = rdr["ExtensionFee"] != DBNull.Value ? Convert.ToDecimal(rdr["ExtensionFee"]) : 0m;
                    if (textBox6 != null) textBox6.Text = totalDue != 0m ? FormatCurrencyString(totalDue) : string.Empty;
                    if (textBox7 != null) textBox7.Text = extFee != 0m ? FormatCurrencyString(extFee) : string.Empty;

                    var amount = rdr["AmountPaid"] != DBNull.Value ? Convert.ToDecimal(rdr["AmountPaid"]) : 0m;
                    if (numericUpDown1 != null)
                    {
                        numericUpDown1.DecimalPlaces = 2;
                        numericUpDown1.Minimum = 0m;
                        numericUpDown1.Maximum = Math.Max(amount, 100000m);
                        numericUpDown1.Value = Math.Min(Math.Max(amount, numericUpDown1.Minimum), numericUpDown1.Maximum);
                    }

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
                button8.Text = "Update Payment";
                tabControl1.SelectedIndex = 5;
                ButtonSettings();
            }
            catch (Exception ex)
            {
                ShowError("Error preparing payment edit: " + ex.Message);
            }
        }

        /// <summary>
        /// Save payment changes made in the payment tab.
        /// Uses a transaction and writes binary PaymentProof if provided.
        /// </summary>
        private void SavePaymentFromTab(object sender, EventArgs e)
        {
            if (editingPaymentId == null)
            {
                ShowWarning("No payment selected for update.");
                return;
            }

            var amount = numericUpDown1.Value;
            var payDate = dateTimePicker1.Value;
            var status = comboBox1.SelectedItem?.ToString() ?? "Pending";

            decimal totalDue = 0m;
            decimal extensionFee = 0m;
            if (textBox6 != null) totalDue = ParseCurrencyString(textBox6.Text);
            if (textBox7 != null) extensionFee = ParseCurrencyString(textBox7.Text);

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
                    {
                        proofBytes = selectedPaymentProofBytes;
                    }
                    else if (!string.IsNullOrEmpty(selectedPaymentProofPath) && File.Exists(selectedPaymentProofPath))
                    {
                        proofBytes = File.ReadAllBytes(selectedPaymentProofPath);
                    }

                    var proofParam = cmd.Parameters.Add("@proof", System.Data.SqlDbType.VarBinary, -1);
                    proofParam.Value = (proofBytes != null && proofBytes.Length > 0) ? (object)proofBytes : DBNull.Value;

                    var rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        tx.Commit();
                        ShowInfo("Payment updated.");

                        LoadPayments();

                        editingPaymentId = null;
                        tabControl1.SelectedIndex = 4;
                        ButtonSettings();
                        button8.Text = "Confirm Reservation";
                    }
                    else
                    {
                        tx.Rollback();
                        ShowWarning("Payment not updated. No rows affected.");
                    }
                }
                catch (Exception ex)
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

        /// <summary>
        /// Loads active packages from the database and binds them to the packageBox combobox.
        /// </summary>
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

        /// <summary>
        /// When package selection changes, update guestBox.Maximum using the MaxGuests value.
        /// </summary>
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

        /// <summary>
        /// Browse and load a payment proof image into memory and the preview PictureBox.
        /// </summary>
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

        /// <summary>
        /// Helper which tries to load an image whether stored as binary or as a file path.
        /// Returns a cloned image in the target PictureBox and caches bytes/path.
        /// </summary>
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

        /// <summary>
        /// Add an Image column to the payments grid and populate thumbnails from PaymentProof values.
        /// </summary>
        private void PopulatePaymentProofImages()
        {
            try
            {
                const string imgColName = "ProofImg";

                if (dataGridView3.Columns.Contains(imgColName))
                    dataGridView3.Columns.Remove(imgColName);

                var imgCol = new DataGridViewImageColumn
                {
                    Name = imgColName,
                    HeaderText = "Proof",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };

                var insertIndex = dataGridView3.Columns.Contains("ReservationID")
                    ? Math.Min(dataGridView3.Columns["ReservationID"].Index + 1, dataGridView3.Columns.Count)
                    : dataGridView3.Columns.Count;
                dataGridView3.Columns.Insert(insertIndex, imgCol);

                if (paymentsTable == null) return;

                for (int rowIndex = 0; rowIndex < dataGridView3.Rows.Count; rowIndex++)
                {
                    var dgvRow = dataGridView3.Rows[rowIndex];
                    Image img = null;
                    try
                    {
                        object proof = null;
                        if (dgvRow.DataBoundItem is DataRowView drv && drv.Row.Table.Columns.Contains("PaymentProof"))
                        {
                            proof = drv["PaymentProof"];
                        }
                        else if (paymentsTable.Columns.Contains("PaymentProof") && rowIndex < paymentsTable.Rows.Count)
                        {
                            proof = paymentsTable.Rows[rowIndex]["PaymentProof"];
                        }

                        if (proof != null && proof != DBNull.Value)
                        {
                            if (proof is byte[] bytes && bytes.Length > 0)
                            {
                                using var ms = new MemoryStream(bytes);
                                using var tmp = Image.FromStream(ms);
                                img = new Bitmap(tmp);
                            }
                            else
                            {
                                var path = proof.ToString();
                                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                                {
                                    var fileBytes = File.ReadAllBytes(path);
                                    using var ms2 = new MemoryStream(fileBytes);
                                    using var tmp2 = Image.FromStream(ms2);
                                    img = new Bitmap(tmp2);
                                }
                            }
                        }
                    }
                    catch
                    {
                        img = null;
                    }

                    dgvRow.Cells[imgColName].Value = img;
                }

                dataGridView3.RowTemplate.Height = Math.Max(dataGridView3.RowTemplate.Height, 80);
            }
            catch
            {
                // non-fatal
            }
        }

        // Currency helpers
        private void CurrencyTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                var d = ParseCurrencyString(tb.Text);
                tb.Text = d != 0m ? FormatCurrencyString(d) : string.Empty;
            }
        }

        // Allow only numeric characters, decimal and thousand separators, control keys
        private void CurrencyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            var decimalSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var groupSep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

            if (char.IsDigit(e.KeyChar) ||
                e.KeyChar.ToString() == decimalSep ||
                e.KeyChar.ToString() == groupSep)
            {
                // ok
                return;
            }

            // otherwise block
            e.Handled = true;
        }

        private string FormatCurrencyString(decimal value)
        {
            // Use "php" prefix with current-culture formatting for separators
            if (value == Math.Truncate(value))
                return $"php {value.ToString("N0", CultureInfo.CurrentCulture)}";
            return $"php {value.ToString("N2", CultureInfo.CurrentCulture)}";
        }

        private decimal ParseCurrencyString(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;

            // remove "php" if present
            var cleaned = text.Replace("php", "", StringComparison.OrdinalIgnoreCase).Trim();

            // try parse with current culture (allows group separators)
            if (decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var v))
                return v;

            // fallback: keep digits, decimal point and minus
            var fallback = "";
            foreach (var ch in cleaned)
                if (char.IsDigit(ch) || ch == '.' || ch == '-' || ch == ',') fallback += ch;

            // replace any group commas with current decimal separator handling if needed
            if (decimal.TryParse(fallback, NumberStyles.Number, CultureInfo.InvariantCulture, out v))
                return v;

            // final attempt using current culture
            if (decimal.TryParse(fallback, NumberStyles.Number, CultureInfo.CurrentCulture, out v))
                return v;

            return 0m;
        }

        // --- Helper methods for DB and UI messaging ---

        private SqlConnection CreateConnection() => new SqlConnection(connString);

        private DataTable FillTable(SqlCommand cmd)
        {
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        private int ExecuteScalarInt(SqlCommand cmd)
        {
            var obj = cmd.ExecuteScalar();
            return obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
        }

        private int ExecuteNonQuery(SqlCommand cmd) => cmd.ExecuteNonQuery();

        /// <summary>
        /// Try to locate an existing customer by name OR facebook link (when link provided).
        /// If not found, insert the customer. Null/empty links are stored as NULL.
        /// </summary>
        private int FindOrCreateCustomer(SqlConnection conn, SqlTransaction tx, string fullName, string facebookLink)
        {
            // First try to find by fullName (always)
            using (var checkByName = new SqlCommand("SELECT customerID FROM Customers WHERE fullName = @name", conn, tx))
            {
                checkByName.Parameters.AddWithValue("@name", fullName);
                var idObj = checkByName.ExecuteScalar();
                if (idObj != null && idObj != DBNull.Value)
                {
                    return Convert.ToInt32(idObj);
                }
            }

            // If a non-empty facebookLink was supplied, try to find by it
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
        (CustomerID, PackageName, PaymentStatus, NumGuests, CheckInDate, CheckOutDate, SpecialNote, BookingStatus, DateCreated, DateModified)
    VALUES
        (@customerID, @package, @pStatus, @guests, @ci, @co, @notes, @bStatus, SYSUTCDATETIME(), SYSUTCDATETIME());
    SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(insertReservationSql, conn, tx);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@package", packageSelected);
            cmd.Parameters.AddWithValue("@pStatus", "Pending");
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

        /// <summary>
        /// Display a simple modal grid with the selected customer's reservation/payment history.
        /// </summary>
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

        // Small UI helpers to reduce duplication in Save/Cancel flows
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

        private void ClearCustomerFormInputs()
        {
            if (textBox2 != null) textBox2.Clear();
            if (textBox3 != null) textBox3.Clear();
        }
    }
}
