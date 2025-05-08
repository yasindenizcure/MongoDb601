using MongoDB.Driver.Core.Configuration;
using Npgsql;
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
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=123";

        void EmployeeList() 
        {
            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();

        }

        void DepartmentList() 
        {
            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            int employeeSalary = int.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Employees (EmployeeName, EmployeeSurname, EmployeeSalary, DepartmentId) VALUES (@employeeName, @employeeSurname, @employeeSalary, @departmentid)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentid", departmentId);
            command.ExecuteNonQuery();  
            MessageBox.Show("Ekleme işlemi başarılı.");
            connection.Close();
            EmployeeList();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM Employees WHERE employeeId = @employeeId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Personel Silme İşlemi Başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            EmployeeList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            int employeeSalary = int.Parse(txtEmployeeSalary.Text);
            int employeeId = int.Parse(txtEmployeeId.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());


            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = @"
            UPDATE Employees 
            SET EmployeeName = @employeeName, 
            EmployeeSurname = @employeeSurname, 
            EmployeeSalary = @employeeSalary, 
            DepartmentId = @departmentId 
            WHERE employeeId = @employeeId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);
            command.Parameters.AddWithValue("@employeeId", employeeId);
            command.Parameters.AddWithValue("@departmentId", departmentId);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Personel Güncelleme İşlemi Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncelleme başarısız. ID veya departman eşleşmiyor olabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            connection.Close();
            EmployeeList();
        }
    }
}
