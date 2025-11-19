using OrchardFarmRMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OrchardFarmRMS
{
    public partial class LoginForm : Form
    {
        // User Role variables
        private bool isAdmin = false;
        private bool isStaff = false;
        public LoginForm()
        {
            InitializeComponent();
            RoleCheck();
            this.AcceptButton = LoginBtn;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            String usernameTxt = UserTxtbox.Text.Trim();
            String passwordTxt = PassTxtbox.Text;

            // Basic validation, passed through UserAccess.ValidateCredentials
            if (string.IsNullOrEmpty(usernameTxt) || string.IsNullOrEmpty(passwordTxt))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var ua = new UserAccess();
                var role = ua.ValidateCredentials(usernameTxt, passwordTxt);

                if (role == null)
                {
                    MessageBox.Show("Invalid username or password.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PassTxtbox.Clear();
                    PassTxtbox.Focus();
                    return;
                }

                // set role for comparison
                var userRole = role.Trim().ToLower();

                if (isAdmin && userRole != "admin")
                {
                    MessageBox.Show("This account is not an Admin. Select the proper role or use an Admin account.", "Role Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isStaff && userRole != "staff")
                {
                    MessageBox.Show("This account is not a Staff account. Select the proper role or use a Staff account.", "Role Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // if success, open the appropriate form
                MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (isAdmin) 
                {
                    PackageConf packageConf = new PackageConf();
                    packageConf.Show();
                }
                else // catch all is isStaff, only other option
                {
                    MainAdminForm mainStaffForm = new MainAdminForm();
                    mainStaffForm.Show();
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking credentials: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            isAdmin = true;
            isStaff = false;
            UpdateBtnColor();
            RoleCheck();
        }

        private void StaffBtn_Click(object sender, EventArgs e)
        {
            isStaff = true;
            isAdmin = false;
            UpdateBtnColor();
            RoleCheck();
        }

        private void UpdateBtnColor()
        {
            // Custom color change for when button is selected
            var selectedColor = Color.FromArgb(24, 112, 87);
            var normalColor = Color.SeaGreen;

            StaffBtn.BackColor = isStaff ? selectedColor : normalColor;
            AdminBtn.BackColor = isAdmin ? selectedColor : normalColor;
        }

        private void RoleCheck()
        {
            // enable input fields only when a role btn is selected
            bool roleSelected = isAdmin || isStaff;
            UserTxtbox.Enabled = roleSelected;
            PassTxtbox.Enabled = roleSelected;
            LoginBtn.Enabled = roleSelected;
        }


    }
}
