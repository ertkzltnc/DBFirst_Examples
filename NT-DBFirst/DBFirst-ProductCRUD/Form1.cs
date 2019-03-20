using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_ProductCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            TedarikCİDoldur();
            KategoriDoldur();
            UrunDoldur();
        }
        
        public void TedarikCİDoldur()
        {
            var list = db.Suppliers.Select(x => new
            {
                x.SupplierID,
                x.CompanyName
            }).ToList();
            cmbTedarik.DisplayMember = "CompanyName";
            cmbTedarik.ValueMember = "SupplierID";
            cmbTedarik.DataSource = list;
        }
        public void KategoriDoldur()
        {
            var list = db.Categories.Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }).ToList();
            cmbKatgeori.DisplayMember = "CategoryName";
            cmbKatgeori.ValueMember = "CategoryID";
            cmbKatgeori.DataSource = list;
        }
        public void UrunDoldur()
        {
            var urun = db.Products.Where(x => x.ProductName.Contains(txtAra.Text)).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.Categories.CategoryName,
                x.Suppliers.CompanyName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
            dgSonuc.DataSource = urun;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            UrunDoldur();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            try
            {
                p.ProductName = txtAd.Text;
                p.UnitPrice =Convert.ToDecimal(txtFiyat.Text);
                p.UnitsInStock = Convert.ToInt16(txtStok.Text);
                p.SupplierID =Convert.ToInt32(cmbTedarik.SelectedValue.ToString());
                p.CategoryID= Convert.ToInt32(cmbKatgeori.SelectedValue.ToString());
                db.Products.Add(p);
                db.SaveChanges();
                UrunDoldur();
                Temizle();



            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgSonuc.Rows[dgSonuc.CurrentRow.Index].Cells["ProductID"].Value);//3
                var urun = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                db.Products.Remove(urun);
                db.SaveChanges();
                UrunDoldur();
                Temizle();


            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                
                int id = Convert.ToInt32(dgSonuc.Rows[dgSonuc.CurrentRow.Index].Cells["ProductID"].Value);//3
                var urun = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                urun.ProductName = txtAd.Text;
                urun.UnitPrice = Convert.ToDecimal(txtFiyat.Text);
                urun.UnitsInStock = Convert.ToInt16(txtStok.Text);
                urun.SupplierID = Convert.ToInt32(cmbTedarik.SelectedValue.ToString());
                urun.CategoryID = Convert.ToInt32(cmbKatgeori.SelectedValue.ToString());
                db.SaveChanges();
                UrunDoldur();
                Temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
            

        }
        public void Temizle()
        {
            txtAd.Text = "";
            txtAra.Text = "";
            txtFiyat.Text = "";
            txtStok.Text = "";
        }
        
    }
}
