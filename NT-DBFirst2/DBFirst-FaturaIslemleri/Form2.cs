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
    public partial class Form2 : Form
    {
     
        public Form2()
        {
            InitializeComponent();
        }

       

        NorthwindEntities db = new NorthwindEntities();
        int orderIDSil, productIDSil;
         
        private void Form2_Load(object sender, EventArgs e)
        {
            Doldur();
            DoldurData();
            MessageBox.Show(Form1.orderID.ToString());
            var o = db.Orders.Where(x => x.OrderID == Form1.orderID);
            foreach (var item in o)
            {

                cbCustomer2.Text = item.Customer.CompanyName;
                cbEmploye2.Text = item.Employee.LastName;
                cbShipVia2.Text = item.Shipper.CompanyName;
                dtpOrderDate2.Value = item.OrderDate.Value;
                dtpRequriedDate2.Value = item.RequiredDate.Value;
                txtFreight2.Text = item.Freight.ToString();
                txtOrderID.Text = Form1.orderID.ToString();
                txtOrderID.Enabled = false;
            }            

        }
        public void Doldur()
        {
            var cst = db.Customers.ToList();

            cbCustomer2.DisplayMember = "CompanyName";
            cbCustomer2.ValueMember = "CustomerID";
            cbCustomer2.DataSource = cst;

            var em = db.Employees.ToList();

            cbEmploye2.DisplayMember = "LastName";
            cbEmploye2.ValueMember = "EmployeeID";
            cbEmploye2.DataSource = em;

            var sh = db.Shippers.ToList();

            cbShipVia2.DisplayMember = "CompanyName";
            cbShipVia2.ValueMember = "ShipperID";
            cbShipVia2.DataSource = sh;

            var p = db.Products.ToList();
            cbProducts.DisplayMember = "ProductName";
            cbProducts.ValueMember = "ProductID";
            cbProducts.DataSource = p;
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            var o = db.Orders.Where(x => x.OrderID == Form1.orderID).FirstOrDefault();
            
            
                o.CustomerID = (cbCustomer2.SelectedValue.ToString());
                o.EmployeeID = Convert.ToInt32(cbEmploye2.SelectedValue);
                o.OrderDate = dtpOrderDate2.Value;
                o.RequiredDate = dtpRequriedDate2.Value;
                o.ShipVia = (int)(cbShipVia2.SelectedValue);
                o.Freight = Convert.ToDecimal(txtFreight2.Text);
            db.SaveChanges();
                
                
            
            
          
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            var o = db.Orders.Where(x => x.OrderID == Form1.orderID);
            var od = db.Order_Details.Where(x => x.OrderID == Form1.orderID);
            foreach (var item in od)
            {
                db.Order_Details.Remove(item);
            }
            foreach (var item in o)
            {
                db.Orders.Remove(item);
            }
            db.SaveChanges();
            this.Hide();
            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           // var unitprice = db.Products.Where(x => x.ProductID == Convert.ToInt32(cbProducts.SelectedValue));
          
            Order_Detail od = new Order_Detail();
            od.OrderID = Form1.orderID;
            od.ProductID =Convert.ToInt32(cbProducts.SelectedValue);

            Product p = db.Products.Find(od.ProductID);
            od.Quantity = Convert.ToInt16(txtQty.Text);
            od.UnitPrice = (decimal)p.UnitPrice;
            od.Discount = 0;
            db.Order_Details.Add(od);
          
            db.SaveChanges();
            DoldurData();
            
            
            
        }
        public void DoldurData()
        {

            List<MyEntity> od = db.Order_Details.Select(x => new MyEntity
            {
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                ProductName = x.Product.ProductName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Discount = x.Discount,
                total = x.Quantity * x.UnitPrice
            }).Where(x => x.OrderID == Form1.orderID).ToList();
           
            txtTotal.Text = od.Sum(x => x.total).ToString();
            dataGridView1.DataSource = od;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //var ods = db.Order_Details.Where(x => x.ProductID == productIDSil && x.OrderID == orderIDSil).FirstOrDefault();
            Order_Detail ods = db.Order_Details.Find(productIDSil, orderIDSil);

            db.Order_Details.Remove(ods);

            Order_Detail od = new Order_Detail();
            od.OrderID = Form1.orderID;
            od.ProductID = Convert.ToInt32(cbProducts.SelectedValue);

            Product p = db.Products.Find(od.ProductID);
            od.Quantity = Convert.ToInt16(txtQty.Text);
            od.UnitPrice = (decimal)p.UnitPrice;
            od.Discount = 0;
            db.Order_Details.Add(od);

            db.SaveChanges();
            DoldurData();


        }

       

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Doldur();
            orderIDSil = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            productIDSil = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString());
            int proID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value);
            var od = db.Order_Details.Where(x => x.ProductID == productIDSil && x.OrderID == orderIDSil).FirstOrDefault();
            cbProducts.Text = od.Product.ProductName;
            int qua = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value);
            txtQty.Text = qua.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            orderIDSil = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
            productIDSil = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
            MessageBox.Show(orderIDSil.ToString()+" "+productIDSil.ToString());
            var od = db.Order_Details.Where(x => x.ProductID == productIDSil && x.OrderID == orderIDSil).FirstOrDefault();
            db.Order_Details.Remove(od);
            db.SaveChanges();
            
            DoldurData();
            

        }

       
    }
}
