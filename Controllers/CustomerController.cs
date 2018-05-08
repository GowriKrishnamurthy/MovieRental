using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ViewResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
            new Customer{Id=1,Name="James Williams"},
            new Customer{Id=2,Name="John Smith"}
            };
        }
    }
}