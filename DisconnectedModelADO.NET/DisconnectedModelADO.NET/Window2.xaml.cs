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
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DisconnectedModelADO.NET
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True"); //step1 SqlConnection

            String query = "Select * from Products;Select * from Orders"; //two tables

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn); //2nd step Sql Dataadapter

            DataSet ds = new DataSet(); //3rd step Dataset, creating empty dataset

            adapter.Fill(ds); //filling our dataset with tables

            DataTable tblProduct = ds.Tables[0]; //1st Datatable
            DataTable tblOrder = ds.Tables[1];//2nd Datatable

            grdProduct.ItemsSource = tblProduct.DefaultView; //displaying the tables inside the tables
            grdOrder.ItemsSource = tblOrder.DefaultView;

        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
       

    }

}
