﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        string ConnectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=123";

        void GetAllCustomers() 
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query= "SELECT * FROM Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();

        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "INSERT INTO Customers (CustomerName, CustomerSurname, CustomerCity) VALUES (@customerName, @customerSurname, @customerCity)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);
            command.Parameters.AddWithValue("@customerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Ekleme İşlemi Başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "DELETE FROM Customers WHERE customerId = @customerId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Silme İşlemi Başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            string query = "UPDATE Customers SET CustomerName = @customerName, CustomerSurname = @customerSurname, CustomerCity = @customerCity WHERE customerId = @customerId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);
            command.Parameters.AddWithValue("@customerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.Parameters.AddWithValue("@customerId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Müşteri Güncelleme İşlemi Başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            GetAllCustomers();
        }

        private void btnGetByCustomerId_Click(object sender, EventArgs e)
        {

        }
    }
}
