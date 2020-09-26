using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using KimToo;
using MetroFramework;
using MetroFramework.Forms;


namespace Resturentmanagementsystem
{
    public partial class Form1 : Form
    {
        List<Resturentmanagementsystem.Salestable.Sales> list = new List<Resturentmanagementsystem.Salestable.Sales>();
        Salestable.Sales sales = new Salestable.Sales();
        Salestable.Total amt = new Salestable.Total();
        List<Resturentmanagementsystem.Salestable.Total> amtlist = new List<Resturentmanagementsystem.Salestable.Total>();
        public Form1()
        {
            InitializeComponent();

            this.ActiveControl = txtitemcode;
            txtitemcode.Focus();
            txtprice.Text = "0.00";
        }
        static string databasename = "db_Restaurant.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=db_Restaurant;Integrated Security=True");
        public static float dtot = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            timer1.Start();
            txtitemcode.Text = "";
            txtprice.Text = "0.00";

        }

        private void txtitemcode_Click(object sender, EventArgs e)
        {
            txtitemcode.Text = "";
            txtitemcode.Focus();
        }

        private void txtpayement_Click(object sender, EventArgs e)
        {
            txtprice.Text = "";
            txtprice.Focus();
        }

        private void txtitemcode_MouseClick(object sender, MouseEventArgs e)
        {
            txtitemcode.Text = "";
            txtitemcode.Focus();
        }

        private void txtpayement_MouseClick(object sender, MouseEventArgs e)
        {

            txtprice.Text = "";
            txtprice.Focus();
        }

        private void btnrice_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from rice_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btndrinks_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from drinks_table UNION select prod_ID,food_name from lassie_table ", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from bakery_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnJuice_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from juice_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnCake_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from cake_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnPizza_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from pizza_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnKottu_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from kottu_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnSmoothi_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from smoothi_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnSoup_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from soup_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnSandwich_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from sandwich_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnDevilled_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from devilled_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }

        private void btnIcecream_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select prod_ID,food_name from icecream_table", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;
        }
        float total;
        float netTotal;
        private void btnTotal_Click(object sender, EventArgs e)
        {

            if (valid() == true)
            {
                float price = float.Parse(txtprice.Text);
                string stringValue = price.ToString().Replace(',', '.');
                float qty = float.Parse(txtQty.Text);
                string stringValue2 = qty.ToString().Replace(',', '.');

                total = price * qty;



                txtTotal.Text = total.ToString();
                Salestable.Sales saless = new Salestable.Sales();
                //saless.Item = txtitemcode.Text;
                saless.Description = txtFood_name.Text;
                //saless.price = price;
                saless.Qty = qty;
                saless.Amount = total;
                list.Add(saless);
                ItemList.DataSource = list.Select(i => new { i.Description, i.Qty,i.Amount }).ToList();
                ItemList.Refresh();
                decimal sum=Convert.ToDecimal(list.Select(s=>s.Amount).Sum());
                amtlist.Clear() ;
                amtlist.Add(new Salestable.Total { Totals = "Total Amount", Amount = sum });
                //dataGridView1.DataSource = null;
                dataGridView1.DataSource = amtlist.Select(s => new { s.Totals, s.Amount }).ToList(); ;
                SqlCommand cmd = new SqlCommand("insert into Sale_table values('" + dtpSale.Value.Date + "','" + txtitemcode.Text + "','" + txtFood_name.Text + "','" + txtprice.Text + "','" + txtQty.Text + "','" + txtTotal.Text + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                txtitemcode.Text = "";
                txtprice.Text = "";
                txtQty.Text = "";
                txtFood_name.Text = "";

            }
        }


        private void txtitemcode_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select food_name from bakery_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                txtFood_name.Text = dr[0].ToString();
            }
            con.Close();

            SqlCommand cmd1 = new SqlCommand("select food_name from cake_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                dr1.Read();

                txtFood_name.Text = dr1[0].ToString();
            }
            con.Close();

            SqlCommand cmd2 = new SqlCommand("select food_name from devilled_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();

                txtFood_name.Text = dr2[0].ToString();
            }
            con.Close();

            SqlCommand cmd3 = new SqlCommand("select food_name from drinks_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr3;
            dr3 = cmd3.ExecuteReader();
            if (dr3.HasRows)
            {
                dr3.Read();

                txtFood_name.Text = dr3[0].ToString();
            }
            con.Close();

            SqlCommand cmd4 = new SqlCommand("select food_name from icecream_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr4;
            dr4 = cmd4.ExecuteReader();
            if (dr4.HasRows)
            {
                dr4.Read();

                txtFood_name.Text = dr4[0].ToString();
            }
            con.Close();

            SqlCommand cmd5 = new SqlCommand("select food_name from juice_table where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr5;
            dr5 = cmd5.ExecuteReader();
            if (dr5.HasRows)
            {
                dr5.Read();

                txtFood_name.Text = dr5[0].ToString();
            }
            con.Close();

            SqlCommand cmd6 = new SqlCommand("select food_name from kottu_table where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr6;
            dr6 = cmd6.ExecuteReader();
            if (dr6.HasRows)
            {
                dr6.Read();

                txtFood_name.Text = dr6[0].ToString();
            }
            con.Close();

            SqlCommand cmd7 = new SqlCommand("select food_name from lassie_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr7;
            dr7 = cmd7.ExecuteReader();
            if (dr7.HasRows)
            {
                dr7.Read();

                txtFood_name.Text = dr7[0].ToString();
            }
            con.Close();

            SqlCommand cmd8 = new SqlCommand("select food_name from rice_table where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr8;
            dr8 = cmd8.ExecuteReader();
            if (dr8.HasRows)
            {
                dr8.Read();

                txtFood_name.Text = dr8[0].ToString();
            }
            con.Close();

            SqlCommand cmd9 = new SqlCommand("select food_name from sandwich_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr9;
            dr9 = cmd9.ExecuteReader();
            if (dr9.HasRows)
            {
                dr9.Read();

                txtFood_name.Text = dr9[0].ToString();
            }
            con.Close();

            SqlCommand cmd10 = new SqlCommand("select food_name from smoothi_table where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr10;
            dr10 = cmd10.ExecuteReader();
            if (dr10.HasRows)
            {
                dr10.Read();

                txtFood_name.Text = dr10[0].ToString();
            }
            con.Close();

            SqlCommand cmd11 = new SqlCommand("select food_name from soup_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr11;
            dr11 = cmd11.ExecuteReader();
            if (dr11.HasRows)
            {
                dr11.Read();

                txtFood_name.Text = dr11[0].ToString();
            }
            con.Close();

            SqlCommand cmd12 = new SqlCommand("select food_name from pizza_table  where prod_ID = '" + txtitemcode.Text + "'", con);

            con.Open();

            SqlDataReader dr12;
            dr12 = cmd12.ExecuteReader();
            if (dr12.HasRows)
            {
                dr12.Read();

                txtFood_name.Text = dr12[0].ToString();
            }
            con.Close();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Sale_table where sale_date='" + dtpSale.Value.Date + "'", con);
            DataTable dt = new DataTable();
            con.Open();
            adp.Fill(dt);
            con.Close();
            dgvItems.DataSource = dt;

            EasyHTMLReports frm = new EasyHTMLReports();
            frm.AddString("<center>");
            frm.AddString("<h3>INVOICE</h3>");
            frm.AddImage(pictureBox1.Image, "width=254");
            frm.AddLineBreak();
            frm.AddString("Date :" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "");
            frm.AddString("<h2>'La silvas' Restaurant' </h2>");
            frm.AddString("<h3>Baticaloa Road,Jayanthipura </h3>");
            frm.AddString("<h3>Polonnaruwa. </h3></center>");
            frm.AddHorizontalRule();
            frm.AddDatagridView(dgvItems);
            frm.GetROw(100);
            frm.ShowPrintPreviewDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTotal.Text = "";
            txtitemcode.Text = "";
            txtprice.Text = "";
            txtQty.Text = "";
            txtFood_name.Text = "";
            ItemList.DataSource = null;
            list.Clear();
            dataGridView1.DataSource = null;
            amtlist.Clear();
            txtNettotal.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            total = 0;
            netTotal = 0;
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {


            netTotal += total;
            txtNettotal.Text = total.ToString();
            txtNettotal.Text = netTotal.ToString();

        }

        private void txtitemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtprice.Focus();
            }
        }

        private void txtprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQty.Focus();
            }

        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnTotal.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtitemcode.Focus();
            }
        }


        private void btnexit_Click(object sender, EventArgs e)
        {
            try
            {
                lbldtot.Text = "";
                Application.Exit();
            }
            catch
            {
            }
        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button_click(object sender, EventArgs e)
        {
            txtprice.Text = "";
            txtprice.Focus();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) 
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    btnTotal.PerformClick();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    button2.PerformClick();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    button1.PerformClick();
                }
                
                else if (e.KeyCode == Keys.F5)
                {
                    btnexit.PerformClick();
                }
            }
            catch
            {

            }
        }

        private void txtQty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (valid() == true)
                    btnTotal.PerformClick();
            }
        }

        public bool valid()
        {
            bool value = false;
            if (txtitemcode.Text == "")
            {
                MessageBox.Show("Item code must be entered");
                txtitemcode.Focus();
            }
            else if (txtprice.Text == "")
            {
                MessageBox.Show("Price must be entered");
                txtitemcode.Focus();
            }
            else if (txtQty.Text == "")
            {
                MessageBox.Show("Quantity must be entered");
                txtitemcode.Focus();
            }
            else
            {
                value = true;
            }
            return value;
        }

        private void button13_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                EasyHTMLReports frm = new EasyHTMLReports();            
                frm.AddString("<center>");
                frm.AddImage(pictureBox1.Image, "width=150" );
                frm.AddLineBreak();
                frm.AddString("<h2>'La silvas' Restaurant' </h2>");
                frm.AddString("<h3>Baticaloa Road,Jayanthipura </h3>");
                frm.AddString("<h3>Polonnaruwa. </h3>");
                frm.AddString("<h3>Tell:0769966829,0702062105</h3>");
                frm.AddString("<h3>Email:lasilva298@gmail.com</h3>");      
                frm.AddString("Date :"+DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")+"");
                frm.AddString("<h4>Sales<h4></center>");
                frm.AddString("<center>");
                frm.AddDatagridView(ItemList,"width='50%' border=0 cellspacing=0 cellpadding=5",null);
                var amt = list.Select(s=>s.Amount).Sum();
                frm.AddLineBreak();
                frm.AddDatagridView(dataGridView1, "width='50%' border=0 cellspacing=0 cellpadding=5", null);
                frm.AddString("</center>");
                frm.AddString("<div align=center styel='margin-right:30px;'>Cash Rs:" + textBox1.Text + "</div>");
                frm.AddString("<div align=center styel='margin-right:30px;'>Balance Rs:" + textBox2.Text + "</div>");
                frm.AddString("<center><h2>Thank you!, Come again...</h2" + "></center>");
                //frm.AddHorizontalRule();
                //frm.AddControl(, "", null);
                //frm.AddString("Total Amount:",amt.ToString());
                frm.Print();
                //frm.ShowPrintPreviewDialog();
            }
            catch { }
            try {

                dtot += netTotal;
                lbldtot.Text = dtot.ToString();
            }

            catch { }
        }

       

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            sales = bindingSource1.Current  as Salestable.Sales;
            list = bindingSource1.DataSource as List<Salestable.Sales>;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                decimal givamt = Convert.ToDecimal(textBox1.Text);
                decimal total = Convert.ToDecimal(txtNettotal.Text);

                decimal amt = total - givamt;
                textBox2.Text = amt.ToString();
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("~SKAppmart~\nTell - 077-0579036\nEmail- sktechnoetc@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
