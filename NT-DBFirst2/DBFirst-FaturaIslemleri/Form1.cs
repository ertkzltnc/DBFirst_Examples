using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_FaturaIslemleri
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {

            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            grbEkle.Visible = true;

            Doldur();

        }
        static public int orderID;
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            Order o = new Order();
            o.CustomerID = (cbCustomer.SelectedValue.ToString());
            o.EmployeeID = Convert.ToInt32(cbEmploye.SelectedValue);
            o.OrderDate = dtpOrderDate.Value;
            o.RequiredDate = dtpRequriedDate.Value;
            o.ShipVia = (int)(cbShipVia.SelectedValue);
            o.Freight = Convert.ToDecimal(txtFreight.Text);
            db.Orders.Add(o);
          
            db.SaveChanges();
            orderID = o.OrderID;
            MessageBox.Show(orderID.ToString());
            grbEkle.Visible = false;
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cs = db.Customers.Where(x => x.CustomerID == (cbCustomer.SelectedValue.ToString()));
            foreach (var item in cs)
            {
                label7.Text = item.Address;
                label8.Text = item.Country + " " + item.City;
            }
        }

        public void Doldur()
        {
            
            var cst = db.Customers.ToList();

            cbCustomer.DisplayMember = "CompanyName";
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.DataSource = cst;

            var em = db.Employees.ToList();

            cbEmploye.DisplayMember = "LastName";
            cbEmploye.ValueMember = "EmployeeID";
            cbEmploye.DataSource = em;

            var sh = db.Shippers.ToList();

            cbShipVia.DisplayMember = "CompanyName";
            cbShipVia.ValueMember = "ShipperID";
            cbShipVia.DataSource = sh;
        }
        Button btnAra = new Button();
        TextBox txtAra = new TextBox();

        Label lbl = new Label();
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            grbEkle.Visible = false;
           // 
            // btnAra
            // 
            
            btnAra.Top = 100;
            btnAra.Left = 50;
            btnAra.Width = 100;     
            btnAra.Name = "btnAra";
            btnAra.Text = "Edit Order";
            tabPage1.Controls.Add(btnAra);
          
            // textbox 
            txtAra.Top = 75;
            txtAra.Left = 50;
            txtAra.Width = 100;
            txtAra.Name = "txtAra";
            tabPage1.Controls.Add(txtAra);

            // label
            lbl.Top = 70;
            lbl.Left = 10;
            lbl.Width = 100;
            lbl.Text = "OrderID";
            btnAra.Click += new EventHandler(this.btnAra_Click);
            tabPage1.Controls.Add(lbl);

        }
        public void btnAra_Click(Object sender,
                            EventArgs e)
        {
            Form1.orderID = Convert.ToInt32(txtAra.Text);
            Form2 frm2 = new Form2();
            frm2.Show();
        }
    }
}
