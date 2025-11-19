using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OrchardFarmRMS
{
    public partial class PackageConf : Form
    {
        private DataTable packagesTable;
        private string connString;

        public PackageConf()
        {
            InitializeComponent();

            connString = ConfigurationManager.ConnectionStrings["OrchardFarmDB"]?.ConnectionString
                         ?? throw new InvalidOperationException("Connection string 'OrchardFarmDB' not found.");

            searchBox.TextChanged -= SearchBox_TextChanged;
            searchBox.TextChanged += SearchBox_TextChanged;

            AddPackageBtn.Click -= AddPackageBtn_Click;
            AddPackageBtn.Click += AddPackageBtn_Click;

            DelPackageBtn.Click -= DelPackageBtn_Click;
            DelPackageBtn.Click += DelPackageBtn_Click;

            EditPackageBtn.Click -= EditPackageBtn_Click;
            EditPackageBtn.Click += EditPackageBtn_Click;

            dataGridView1.RowHeaderMouseDoubleClick -= DataGridView1_RowHeaderMouseDoubleClick;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;

            dataGridView1.CellDoubleClick -= DataGridView1_CellDoubleClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            btnConfirmAdd.Click -= BtnConfirmAdd_Click;
            btnConfirmAdd.Click += BtnConfirmAdd_Click;

            btnCancelAdd.Click -= BtnCancelAdd_Click;
            btnCancelAdd.Click += BtnCancelAdd_Click;

            ButtonSettings();
            LoadPackages();
        }

        private void ButtonSettings()
        {
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(0, 1);

            var selectedColor = Color.FromArgb(131, 223, 117);
            var normalColor = Color.FromArgb(209, 254, 216);

            if (tab1Btn != null)
                tab1Btn.BackColor = tabControl1.SelectedIndex == 0 ? selectedColor : normalColor;
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to log out?", "Confirm log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
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

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 105, 44); // match main
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Bookman Old Style", 14F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersHeight = 48;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.DefaultCellStyle.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular);
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(131, 223, 117);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 244); // subtle banding

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        // load packages from DB
        private void LoadPackages()
        {
            try
            {
                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand(@"
SELECT PackageID, PackageName, PackagePrice, MaxGuests, Active, Details
FROM Packages
ORDER BY PackageName", conn);

                using var adapter = new SqlDataAdapter(cmd);
                packagesTable = new DataTable();
                adapter.Fill(packagesTable);

                dataGridView1.DataSource = packagesTable;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AllowUserToAddRows = false;

                if (dataGridView1.Columns.Contains("PackageID"))
                    dataGridView1.Columns["PackageID"].HeaderText = "Package ID";
                if (dataGridView1.Columns.Contains("PackageName"))
                    dataGridView1.Columns["PackageName"].HeaderText = "Package Name";
                if (dataGridView1.Columns.Contains("PackagePrice"))
                {
                    dataGridView1.Columns["PackagePrice"].HeaderText = "Package Price";
                    dataGridView1.Columns["PackagePrice"].DefaultCellStyle.Format = "'php' #,##0.00";
                }
                if (dataGridView1.Columns.Contains("MaxGuests"))
                    dataGridView1.Columns["MaxGuests"].HeaderText = "Max Guests";
                if (dataGridView1.Columns.Contains("Active"))
                    dataGridView1.Columns["Active"].HeaderText = "Active";
                if (dataGridView1.Columns.Contains("Details"))
                {
                    dataGridView1.Columns["Details"].HeaderText = "Details";
                    dataGridView1.Columns["Details"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                ConfigureGrid(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading packages: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (packagesTable == null) return;

            var filter = searchBox.Text.Trim().Replace("'", "''");
            if (string.IsNullOrWhiteSpace(filter))
            {
                packagesTable.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                packagesTable.DefaultView.RowFilter = $"PackageName LIKE '%{filter}%' OR Details LIKE '%{filter}%'";
            }
        }

        private void AddPackageBtn_Click(object sender, EventArgs e)
        {
            EnterAddMode();
            tabControl1.SelectedTab = addPackageTab;
            ButtonSettings();
        }

        private void EnterAddMode()
        {
            addHeaderLabel.Text = "Add Package";
            btnConfirmAdd.Text = "Save Package";
            btnConfirmAdd.Tag = null;

            txtNameAdd.Text = string.Empty;
            txtPriceAdd.Text = string.Empty;
            numMaxGuestsAdd.Value = Math.Max(numMaxGuestsAdd.Minimum, 1);
            chkActiveAdd.Checked = true;
            txtDetailsAdd.Text = string.Empty;
        }

        private void EditPackageBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a package to edit.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!dataGridView1.Columns.Contains("PackageID"))
            {
                MessageBox.Show("Selected row does not contain PackageID.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var idObj = dataGridView1.CurrentRow.Cells["PackageID"].Value;
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("Cannot determine the selected package id.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = Convert.ToInt32(idObj);

            txtNameAdd.Text = dataGridView1.CurrentRow.Cells["PackageName"].Value?.ToString() ?? string.Empty;
            txtPriceAdd.Text = dataGridView1.CurrentRow.Cells["PackagePrice"].Value != DBNull.Value
                ? Convert.ToDecimal(dataGridView1.CurrentRow.Cells["PackagePrice"].Value).ToString("0.00")
                : "0.00";
            numMaxGuestsAdd.Value = dataGridView1.CurrentRow.Cells["MaxGuests"].Value != DBNull.Value
                ? Math.Max(numMaxGuestsAdd.Minimum, Convert.ToInt32(dataGridView1.CurrentRow.Cells["MaxGuests"].Value))
                : (decimal)numMaxGuestsAdd.Minimum;
            chkActiveAdd.Checked = dataGridView1.CurrentRow.Cells["Active"].Value != DBNull.Value
                ? Convert.ToBoolean(dataGridView1.CurrentRow.Cells["Active"].Value)
                : true;
            txtDetailsAdd.Text = dataGridView1.CurrentRow.Cells["Details"].Value?.ToString() ?? string.Empty;

            addHeaderLabel.Text = "Edit Package";
            btnConfirmAdd.Text = "Save Changes";
            btnConfirmAdd.Tag = id; 

            tabControl1.SelectedTab = addPackageTab;
            ButtonSettings();
        }

        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditPackageBtn_Click(sender, EventArgs.Empty);
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPackageBtn_Click(sender, EventArgs.Empty);
        }
        
        private void BtnConfirmAdd_Click(object sender, EventArgs e)
        {
            var name = txtNameAdd.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Package name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNameAdd.Focus();
                return;
            }

            if (!decimal.TryParse(txtPriceAdd.Text.Trim(), out var price))
                price = 0m;

            var maxGuests = (int)numMaxGuestsAdd.Value;
            var active = chkActiveAdd.Checked;
            var details = txtDetailsAdd.Text.Trim();

            if (btnConfirmAdd.Tag == null)
            {
                try
                {
                    ExecuteNonQuery(@"
INSERT INTO Packages (PackageName, PackagePrice, MaxGuests, Active, Details)
VALUES (@name, @price, @maxGuests, @active, @details);", cmd =>
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@maxGuests", maxGuests);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@details", (object)details ?? DBNull.Value);
                    });

                    MessageBox.Show("Package added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var id = Convert.ToInt32(btnConfirmAdd.Tag);
                try
                {
                    var rows = ExecuteNonQuery(@"
UPDATE Packages
SET PackageName = @name,
    PackagePrice = @price,
    MaxGuests = @maxGuests,
    Active = @active,
    Details = @details
WHERE PackageID = @id;", cmd =>
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@maxGuests", maxGuests);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@details", (object)details ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@id", id);
                    });

                    if (rows > 0)
                        MessageBox.Show("Package updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Package not updated (row not found).", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            LoadPackages();
            EnterAddMode();
            tabControl1.SelectedIndex = 0;
            ButtonSettings();
        }

        private void BtnCancelAdd_Click(object sender, EventArgs e)
        {
            EnterAddMode();
            tabControl1.SelectedIndex = 0;
            ButtonSettings();
        }

        private void DelPackageBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a package to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!dataGridView1.Columns.Contains("PackageID"))
            {
                MessageBox.Show("Selected row does not contain PackageID.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var idObj = dataGridView1.CurrentRow.Cells["PackageID"].Value;
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("Cannot determine the selected package id.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = Convert.ToInt32(idObj);
            var name = dataGridView1.CurrentRow.Cells["PackageName"].Value?.ToString() ?? id.ToString();

            var confirm = MessageBox.Show($"Delete package \"{name}\"?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var rows = ExecuteNonQuery("DELETE FROM Packages WHERE PackageID = @id", cmd => cmd.Parameters.AddWithValue("@id", id));
                if (rows > 0)
                {
                    MessageBox.Show("Package deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPackages();
                }
                else
                {
                    MessageBox.Show("Package not found or not deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // helper to execute non-query SQL commands
        private int ExecuteNonQuery(string sql, Action<SqlCommand> addParameters)
        {
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand(sql, conn);
            addParameters?.Invoke(cmd);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
