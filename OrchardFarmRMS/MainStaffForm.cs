using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OrchardFarmRMS
{
    public partial class MainAdminForm : Form
    {
        public MainAdminForm()
        {
            InitializeComponent();
            ButtonSettings();


        }

        private void ButtonSettings()
        {
            // Hide prefixed tab control appearance for custom tab buttons
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(0, 1);

            // For color change when selected
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

        private void MainAdminForm_Load(object sender, EventArgs e)
        {

        }

        private void AddResBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void CustomerClearBtn_Click(object sender, EventArgs e)
        {
            NameBox.Clear();
            ContactBox.Clear();
        }

        private void ResClearBtn_Click(object sender, EventArgs e)
        {
            packageBox.SelectedIndex = -1;
            guestBox.Value = 0;
            checkInDateBox.Value = DateTime.Now;
            checkOutDateBox.Value = DateTime.Now;
            noteBox.Clear();

        }

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void confirmBtn_Click_1(object sender, EventArgs e)
        {
            // Validate and clean up input field values
            var customerName = NameBox.Text.Trim();
            var contact = ContactBox.Text.Trim();
            var packageSelected = packageBox.SelectedItem?.ToString() ?? string.Empty;
            var guests = (int)guestBox.Value;
            var checkIn = checkInDateBox.Value.Date;
            var checkOut = checkOutDateBox.Value.Date;
            var notes = noteBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Customer name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NameBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contact))
            {
                MessageBox.Show("Contact is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ContactBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(packageSelected))
            {
                MessageBox.Show("Please select a package.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                packageBox.Focus();
                return;
            }

            if (guests <= 0)
            {
                MessageBox.Show("Guests must be at least 1.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guestBox.Focus();
                return;
            }

            if (checkOut < checkIn)
            {
                MessageBox.Show("Check-out date must be the same or after check-in date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkOutDateBox.Focus();
                return;
            }

            // Insert entry to db
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["OrchardFarmDB"]?.ConnectionString
                                 ?? throw new InvalidOperationException("Connection string 'OrchardFarmDB' not found.");

                const string sql = @"
INSERT INTO dbo.Reservation
    (PackageName, Payment Status, )
VALUES
    (@name, @contact, @package, @guests, @ci, @co, @notes, GETDATE());";

                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 200) { Value = customerName });
                cmd.Parameters.Add(new SqlParameter("@contact", SqlDbType.NVarChar, 100) { Value = contact });
                cmd.Parameters.Add(new SqlParameter("@package", SqlDbType.NVarChar, 100) { Value = packageSelected });
                cmd.Parameters.Add(new SqlParameter("@guests", SqlDbType.Int) { Value = guests });
                cmd.Parameters.Add(new SqlParameter("@ci", SqlDbType.Date) { Value = checkIn });
                cmd.Parameters.Add(new SqlParameter("@co", SqlDbType.Date) { Value = checkOut });
                cmd.Parameters.Add(new SqlParameter("@notes", SqlDbType.NVarChar, -1) { Value = (object)notes ?? DBNull.Value });

                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Reservation saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear the form and return to reservations list tab (index 0 assumed)
                    ResClearBtn_Click(null, EventArgs.Empty);
                    tabControl1.SelectedIndex = 0;
                    ButtonSettings();

                }
                else
                {
                    MessageBox.Show("Reservation was not saved. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving reservation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
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
    }
}
