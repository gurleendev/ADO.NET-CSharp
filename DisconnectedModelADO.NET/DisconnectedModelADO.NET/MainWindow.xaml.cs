using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace DisconnectedModelADO.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data;
        public MainWindow()
        {
            InitializeComponent();
            data =new Data();
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable tblProduct=data.GetDataTable();
            grdProducts.ItemsSource = tblProduct.DefaultView;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id =int.Parse(txtId.Text);
            DataRow row=data.FindByID(id);
            if(row != null )
            {
                txtName.Text = row["ProductName"].ToString();
                txtPrice.Text = row["UnitPrice"].ToString() ;
                txtQuantity.Text = row["UnitsInStock"].ToString();


            }
            else 
            {
                MessageBox.Show("Row not found with given 10");
            }
        }

        private void btnShowWindow2_Click(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Show();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id =int.Parse(txtId.Text);
            DataTable tbl=data.DeleteRow(id);
            LoadGrid();
            

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name=txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity=short.Parse(txtQuantity.Text);
            DataTable tbl= data.InsertProduct(name, price, quantity);
            grdProducts.ItemsSource = tbl.DefaultView;
            LoadGrid();


        }

        private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            decimal price =
            decimal.Parse(txtPrice.Text); 
            short quantity = short.Parse(txtQuantity.Text);
            DataTable tbl = data.UpdateProduct(id, name, price, quantity);
            grdProducts.ItemsSource=tbl.DefaultView;
            LoadGrid();
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}