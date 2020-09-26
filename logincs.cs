using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.Data.SqlClient;

namespace Resturentmanagementsystem
{
    public partial class login : MetroForm
    {
        public login()
        {
            InitializeComponent();
            pbLogin.Visible = false;
            this.ActiveControl = txtEmail;
            txtEmail.Focus();
        }
        static string databasename = "db_Restaurant.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=db_Restaurant;Integrated Security=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {

            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from login_info where email = '" + txtEmail.Text + "' and password = '" + txtPassword.Text + "'", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                epCorrect.Clear();
                epCorrect.SetError(btnLogin, "Login Success");
                pbLogin.Visible = true;
                timer1.Enabled = true;
              
               


            }
            else
            {
                epWrong.Clear();
                epWrong.SetError(btnLogin, "Invalid Login");

                txtPassword.Text = "";

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbLogin.Value = pbLogin.Value + 2;
            if (pbLogin.Value > 99)
            {
                timer1.Enabled = false;
                this.Hide();
                new Form1().Show();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        
    }
}
