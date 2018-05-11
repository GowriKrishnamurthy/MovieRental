using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MovieRental.ViewModels;
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
            // Include data from 2 tables - customer and Membership
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        // Details method to show details of each customer id
        public ActionResult Details(int id)
        {
            /* 
             * SingleOrDefault - Returns the only element of a sequence, or a default value if the sequence is empty; 
             * this method throws an exception if there is more than one element in the sequence.
            */
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // On click of New customer button, this method gets called to populate page with defaults
        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();

            // Create a Viewmodel as we need a view that combines 2 entities - MembershipType and Customer
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipType = membershipType
            };
            return View("CustomerForm", viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //This method gets called when Save button is clicked on new customer form
        public ActionResult Save(Customer customer)
        {
            // Form level Validation check
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipTypes.ToList()
                };
                //If form validation failed - return to same form.
                return View("CustomerForm", viewModel);
            }
            
            if (customer.Id == 0)
            {
                //New customer
                _context.Customers.Add(customer);
            }
            else
            {
                //existing customer - Edit changes
                //Single method used - throws exception if  customer was not found with this ID
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate= customer.Birthdate;
                customerInDb.MembershipTypeId= customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //Alternative options to save to Db
                //1. TryUpdateModel(customerInDb); //Security issues may be come up
                //2. Mapper.map(customer, customerInDb);
            }
            _context.SaveChanges();

            //Once the changes from DB contexts are committed to DB, Home page of customer gets loaded
            return RedirectToAction("Index", "Customer");
        }
        // On click of Edit customer button, this method gets called
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
 
            if (customer== null)
                return HttpNotFound();

            // Create a Viewmodel as we need a view that combines 2 entities - MembershipType and Customer
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}