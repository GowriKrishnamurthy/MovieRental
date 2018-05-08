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
        // Index method to show all customers
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
        // Details method to show details of each customer id
        public ActionResult Details(int id)
        {
            /* 
             * SingleOrDefault - Returns the only element of a sequence, or a default value if the sequence is empty; 
             * this method throws an exception if there is more than one element in the sequence.
            */
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

    }
}