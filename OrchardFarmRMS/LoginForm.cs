using OrchardFarmRMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

/*
CREATE TABLE dbo.Users
(
    UserID       INT IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(100) NOT NULL UNIQUE,
    Password     NVARCHAR(100) NOT NULL UNIQUE,
    UserRole     NVARCHAR(50)  NOT NULL,
    CreatedAt    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME()
);

INSERT INTO dbo.Users (Username, Password, UserRole)
VALUES 
('admin', 'admin123', 'admin'),
('staff', 'staff123', 'staff');

 * -- Customers
CREATE TABLE Customers (
    customerID    INT IDENTITY(1,1) PRIMARY KEY,
    fullName      NVARCHAR(200) NOT NULL,
    facebookLink  NVARCHAR(500) NULL,
    CreatedAt     DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Packages
CREATE TABLE Packages (
    PackageID     INT IDENTITY(1,1) PRIMARY KEY,
    PackageName   NVARCHAR(200) NOT NULL,
    PackagePrice  DECIMAL(18,2) NULL,
    MaxGuests     INT NULL,
    Active        BIT NOT NULL DEFAULT 1,
    Details       NVARCHAR(MAX) NULL
);

-- Reservation
CREATE TABLE Reservation (
    ReservationID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID    INT NOT NULL,
    PackageName   NVARCHAR(200) NULL,        -- used by Select/Load/Update flows in code
    PaymentStatus NVARCHAR(50) NULL,
    NumGuests     INT NULL,
    CheckInDate   DATE NULL,
    CheckOutDate  DATE NULL,
    SpecialNote   NVARCHAR(MAX) NULL,
    BookingStatus NVARCHAR(50) NULL,
    DateCreated   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DateModified  DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Reservation_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(customerID)
        ON DELETE CASCADE
);

CREATE INDEX IX_Reservation_CustomerID ON Reservation(CustomerID);
CREATE INDEX IX_Reservation_DateCreated ON Reservation(DateCreated DESC);

-- Payment
CREATE TABLE Payment (
    PaymentID     INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID INT NOT NULL,
    PackagePrice  DECIMAL(18,2) NULL,
    PaymentDate   DATETIME2 NULL,
    TotalDue      DECIMAL(18,2) NULL,
    ExtensionFee  DECIMAL(18,2) NULL,
    AmountPaid    DECIMAL(18,2) NULL,
    PaymentProof  VARBINARY(MAX) NULL,       -- code supports varbinary bytes or a file-path string
    PaymentStatus NVARCHAR(50) NULL,
    CONSTRAINT FK_Payment_Reservation FOREIGN KEY (ReservationID) REFERENCES Reservation(ReservationID)
        ON DELETE CASCADE
);

CREATE INDEX IX_Payment_ReservationID ON Payment(ReservationID);
CREATE INDEX IX_Payment_PaymentDate ON Payment(PaymentDate DESC);

-- Optional: small view used by LoadReservations() (matches the SELECT in code)
CREATE VIEW vw_ReservationsList AS
SELECT 
    R.ReservationID, 
    C.fullName AS CustomerName,
    R.PackageName, 
    R.PaymentStatus, 
    R.NumGuests, 
    R.CheckInDate, 
    R.CheckOutDate, 
    R.BookingStatus,
    R.SpecialNote,
    R.DateCreated
FROM Reservation R
JOIN Customers C ON R.CustomerID = C.customerID;

-- Optional: small view used by LoadPayments() (matches the SELECT in code)
CREATE VIEW vw_PaymentList AS
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
FROM Payment;
ALTER TABLE Payment DROP CONSTRAINT FK_Payment_Reservation;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_Reservation
FOREIGN KEY (ReservationID) REFERENCES Reservation(ReservationID) ON DELETE CASCADE;*/
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
            this.AcceptButton = LoginBtn;
            RoleCheck();
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
