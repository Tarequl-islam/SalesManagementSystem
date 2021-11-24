using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.Ajax.Utilities;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Gateway
{
    public class SalesGateway
    {
        private string connectionString;
        private SqlConnection connection;
        private SqlDataReader reader;

        public SalesGateway()
        {
            connectionString = WebConfigurationManager.
                    ConnectionStrings["SalesManagementDBconString"].
                    ConnectionString;
            connection = new SqlConnection(connectionString);
        }
        public int Save(Item item)
        {
            string query = "INSERT INTO SalesItem(ItemId, Price, Quantity, Total) VALUES(@ItemId, @Price, @Quantity, @Total)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ItemId", item.ItemId);
            command.Parameters.AddWithValue("@Price", item.Price);
            command.Parameters.AddWithValue("@Quantity", item.Quantity);
            command.Parameters.AddWithValue("@Total", item.Total);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }

        public List<Item> GetAllItems()
        {
            string query = "SELECT * FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            reader = command.ExecuteReader();
            List<Item> itemList = new List<Item>();
            while (reader.Read())
            {
                Item item = new Item();
                item.Id = Convert.ToInt32(reader["Id"]);
                item.Name = reader["Name"].ToString();
                itemList.Add(item);

            }
            connection.Close();
            return itemList;
        }
        public List<Item> GetAllSalesItem()
        {
            string query = "SELECT * FROM SalesItem INNER JOIN Items ON SalesItem.ItemId=Items.Id";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            reader = command.ExecuteReader();
            List<Item> itemList = new List<Item>();
            while (reader.Read())
            {
                Item item = new Item();
                item.Id = Convert.ToInt32(reader["Id"]);
                item.Name = reader["Name"].ToString();
                item.Price = Convert.ToInt32(reader["Price"]);
                item.Quantity = Convert.ToInt32(reader["Quantity"]);
                item.Total = Convert.ToInt32(reader["Total"]);
                itemList.Add(item);

            }
            connection.Close();
            return itemList;
        }
        public Item GetSelectedItem(int itmId)
        {
            string query = "SELECT * FROM Items WHERE Id=@Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", itmId);
            connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            Item item = new Item();
            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = reader["Name"].ToString();
            item.Price = Convert.ToInt32(reader["Price"]);
            connection.Close();
            return item;
        }

        public int GetTotalSumPrice()
        {
            string query = "SELECT SUM(Total) FROM SalesItem";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            object temp = command.ExecuteScalar();
            int sum;
            if (temp.GetType() != typeof(DBNull))
            {
                sum = Convert.ToInt32(temp);
            }
            else
            {
                sum = 0;
            }
            connection.Close();
            return sum;
        }

        public int SaveSales(Sale sale)
        {
            string query = "INSERT INTO SalesList(CustomerName, Phone, Address, Total, Paid, Due) " +
                           "VALUES(@CustomerName, @Phone, @Address, @Total, @Paid, @Due)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerName", sale.CustomerName);
            command.Parameters.AddWithValue("@Phone", sale.CustomerPhone);
            command.Parameters.AddWithValue("@Address", sale.CustomerAddress);
            command.Parameters.AddWithValue("@Total", sale.TotalPrice);
            command.Parameters.AddWithValue("@Paid", sale.Paid);
            command.Parameters.AddWithValue("@Due", sale.Due);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        public int ClearSalesItem()
        {
            string query = "DELETE FROM SalesItem";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }

        public List<Sale> GetAllSalesList()
        {
            string query = "SELECT * FROM SalesList";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            reader = command.ExecuteReader();
            List<Sale> saleList = new List<Sale>();
            while (reader.Read())
            {
                Sale sale = new Sale();
                sale.Id = Convert.ToInt32(reader["Id"]);
                sale.CustomerName = reader["CustomerName"].ToString();
                sale.CustomerPhone = reader["Phone"].ToString();
                sale.CustomerAddress = reader["Address"].ToString();
                sale.TotalPrice = Convert.ToInt32(reader["Total"]);
                sale.Paid = Convert.ToInt32(reader["Paid"]);
                sale.Due = Convert.ToInt32(reader["Due"]);
                saleList.Add(sale);

            }
            connection.Close();
            return saleList;
        }

    }
}