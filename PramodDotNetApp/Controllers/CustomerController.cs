using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PramodDotNetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PramodDotNetApp.Data;

namespace PramodDotNetApp.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetCustomers")]
        public List<Customer> GetCustomers()
        {
            DataAccess da = new DataAccess(_configuration);
            return da.GetCustomers();
        }

        [HttpGet] 
        public Customer Get(string UserId)
        {
            Guid guid = new Guid(UserId);
            DataAccess da = new DataAccess(_configuration);
            return da.GetCustomers().FirstOrDefault(e=>e.UserId == guid);
        }

        [HttpPost] 
        public string Post(Customer customer)
        {
            customer.UserId = Guid.NewGuid();
            DataAccess da = new DataAccess(_configuration);
            da.InsertCustomer(customer);
            return customer.UserId.ToString();
        }

        [HttpPut] 
        public string Put(Customer customer)
        {
            
            DataAccess da = new DataAccess(_configuration);
            return  da.UpdatetCustomer(customer); 
        }

        [HttpDelete] 
        public string Delete(string userId)
        {
            Guid id = new Guid(userId);
            DataAccess da = new DataAccess(_configuration);
            return da.DeleteCustomer(id);
        }

        [HttpGet]
        [Route("ActiveOrdersByCustomers")]
        public List<Order> ActiveOrdersByCustomers(string UserId)
        {
            Guid guid = new Guid(UserId);
            DataAccess da = new DataAccess(_configuration);
            return da.ActiveOrderByCustomer(guid);
        }


    }
}
