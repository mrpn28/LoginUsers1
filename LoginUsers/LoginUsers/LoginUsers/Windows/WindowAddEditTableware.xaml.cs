using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginUsers.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAddEditTableware.xaml
    /// </summary>
    public partial class WindowAddEditTableware : Window
    {
        public WindowAddEditTableware(Products currentProducts)
        {
            InitializeComponent();
            try
            {
                this.currentProducts = currentProducts;
                tbArticul.Text = currentProducts.Articul.ToString();
                tbName.Text = currentProducts.Name.ToString();
                tbUnit.Text = currentProducts.Unit.ToString();
                tbCost.Text = currentProducts.Cost.ToString();
                tbMaxSale.Text = currentProducts.MaxSale.ToString();
                tbManufacturer.Text = currentProducts.Manufacturer.ToString();
                tbSupplier.Text = currentProducts.Supplier.ToString();
                tbCategory.Text = currentProducts.Category.ToString();
                tbCurrentSale.Text = currentProducts.CurrentSale.ToString();
                tbCount.Text = currentProducts.Count.ToString();
                tbDescription.Text = currentProducts.Description.ToString();
                tbImage.Text = currentProducts.Image.ToString();
            }
            catch { }
        }

        Products currentProducts;
        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            currentProducts.Articul = tbArticul.Text;
            currentProducts.Name = tbName.Text;
            currentProducts.Unit = tbUnit.Text;
            currentProducts.Cost = double.Parse(tbCost.Text);
            currentProducts.MaxSale = int.Parse(tbMaxSale.Text);
            currentProducts.Manufacturer = tbManufacturer.Text;
            currentProducts.Supplier = tbSupplier.Text;
            currentProducts.Category = tbCategory.Text;
            currentProducts.CurrentSale = int.Parse(tbCurrentSale.Text);
            currentProducts.Count = int.Parse(tbCount.Text);
            currentProducts.Description = tbDescription.Text;
            currentProducts.Image = tbImage.Text;
            if (currentProducts.Id == 0)
                App.Dbtableware.Products.Add(currentProducts);
            App.Dbtableware.SaveChanges();
            Close();
        }

        private void btnCancelProduct_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
