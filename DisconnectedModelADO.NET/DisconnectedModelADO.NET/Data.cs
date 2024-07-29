using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DisconnectedModelADO.NET
{
    class Data
    {
        SqlConnection _conn;
        SqlDataAdapter _adapter;
        DataSet _dataSet;
        DataTable _dataTable;
        SqlCommandBuilder _cmdBuilder;
        string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        public string Connstr { get { return connstr; } }

        public Data()
        {
            _conn = new SqlConnection(Connstr);
            string query = "Select ProductID, ProductName,UnitPrice, UnitsInStock from Products";
            _adapter = new SqlDataAdapter(query, _conn);
            _cmdBuilder = new SqlCommandBuilder(_adapter);
            FillDataset();
        }


        public void FillDataset()
        {
            _dataSet = new DataSet();
            _adapter.Fill(_dataSet);
            _dataTable = _dataSet.Tables[0];

            //Add primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _dataTable.Columns["ProductID"];
            pk[0].AutoIncrement = true;
            _dataTable.PrimaryKey = pk;
        }

        public DataTable GetDataTable()
        {
            FillDataset();
            return _dataTable;
        }
        public DataRow FindByID(int id)
        {

            DataRow row = _dataTable.Rows.Find(id);
            return row;
        }

        public DataTable DeleteRow(int id)
        {
            DataRow row = FindByID(id);
            if (row != null)
            {
                row.Delete();
                MessageBox.Show("Row Deleted");

            }
            else
            {
                MessageBox.Show("ID not found");
            }
            _cmdBuilder.GetDeleteCommand();
            _adapter.Update(_dataTable);
            return _dataTable;
        }
        public DataTable InsertProduct(string name, decimal price, short units)
        {

            DataRow row = _dataTable.NewRow();
            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["unitsInstock"] = units;

            _dataTable.Rows.Add(row);
            _adapter.InsertCommand = _cmdBuilder.GetInsertCommand();
            _adapter.Update(_dataTable);
            return _dataTable;

        }

        public DataTable UpdateProduct(int id, string name, decimal price, short
        quantity)
        {
            DataRow row = _dataTable.Rows.Find(id);
            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["UnitsInStock"] =
            quantity;
            _adapter.UpdateCommand = _cmdBuilder.GetUpdateCommand();
            _adapter.Update(_dataTable);
            return _dataTable ;
        }
    }
}

