using RecapProject1.Entities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RecapProject1
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.ToList();
            }
        }

        private void LoadProductsByCategory(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.CategoryId == categoryId).ToList();
            }
        }

        private void LoadProductsByProductName(string productName)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
            }
        }

        private void LoadCategories()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = nameof(Category.CategoryName);
                cbxCategory.ValueMember = nameof(Category.CategoryId);
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgwProduct.DataSource != null)
            {
                LoadProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadProducts();
            }
            else
            {
                LoadProductsByProductName(txtSearch.Text);
            }
        }
    }
}