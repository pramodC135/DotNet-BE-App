using Microsoft.Extensions.Configuration;
using PramodDotNetApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PramodDotNetApp.Data
{
    public class DataAccess
    {
        private readonly IConfiguration _configuration;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Customer customer = new Customer();
                    customer.UserId = new Guid(dr["userId"].ToString());
                    customer.UserName = Convert.ToString(dr["userName"]);
                    customer.Email = Convert.ToString(dr["email"]);
                    customer.FirstName = Convert.ToString(dr["firstName"]);
                    customer.LastName = Convert.ToString(dr["lastName"]);
                    customer.CreatedOn = Convert.ToDateTime(dr["createOn"]);
                    customer.IsActive = Convert.ToBoolean(dr["isActive"]);
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public List<Order> ActiveOrderByCustomer(Guid customerId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("dbo.ActiveOrdersByCustomerId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", customerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Order order = new Order();
                    order.OrderId = new Guid(dr["OrderId"].ToString());
                    order.ProductId = new Guid(dr["ProductId"].ToString());
                    order.OrderStatus = Convert.ToInt16(dr["OrderStatus"]);
                    order.OrderType = Convert.ToInt16(dr["OrderType"]);
                    order.OrderBy = new Guid(dr["OrderBy"].ToString());
                    order.OrderedOn = Convert.ToDateTime(dr["OrderedOn"]);
                    order.ShippedOn = Convert.ToDateTime(dr["ShippedOn"]);
                    order.IsActive = Convert.ToBoolean(dr["isActive"]);
                    orders.Add(order);
                }
            }

            return orders;
        }

        public string InsertCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customer VALUES(@UserId , @Username , @Email , @FirstName , @LastName, @CreatedOn, @IsActive )", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                cmd.Parameters.AddWithValue("@Username", customer.UserName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();
            }

            return "Customer Added Successfully";
        }

        public string UpdatetCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET Username = @Username , Email = @Email , FirstName = @FirstName , LastName = @LastName, IsActive = @IsActive  WHERE UserId = @UserId", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                cmd.Parameters.AddWithValue("@Username", customer.UserName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);            
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return "Customer Updated Successfully";
        }


        public string DeleteCustomer(Guid userId)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE userId=@UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return "Customer Dleted Successfully";
        }
    }
}
