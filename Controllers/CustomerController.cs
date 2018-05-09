using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MovieRental.Controllers
{
    public class CustomerController : Controller
    {
        // Index method to show all customers
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Index method to show all customers from Customer table
        public ViewResult Index()
        {
            //var customers = GetCustomers();
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();   
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
            //var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

    }
}