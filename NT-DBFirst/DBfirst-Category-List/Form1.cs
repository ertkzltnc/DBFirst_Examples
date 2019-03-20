using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBfirst_Category_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            NorthwindEntities db = new NorthwindEntities();
            // secili alan gosterme 1. yol
            
            //var category = db.Categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
            //dataGridView1.DataSource = category;

            // secili alan datagridview de  secili alan gösterme 2.yol

                var category1 = db.Categories.Select(x =>x).ToList();
                dataGridView1.DataSource = category1;
                dataGridView1.Columns["Description"].Visible = false;
                dataGridView1.Columns["Picture"].Visible=false;

            // 3.yol
            //var category1 = db.Categories.ToList();
            //dataGridView1.DataSource = category1;
            //dataGridView1.Columns["Description"].Visible = false;
            //dataGridView1.Columns["Picture"].Visible = false;





        }
    }
}
