using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieRental.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET all customers - api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET single customer -  api/Customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            return (customer == null)
                ? throw new HttpResponseException(HttpStatusCode.NotFound)
                : customer;
        }

        //POST - api/Customers
        [HttpPost]
        public void CreateCustomer(Customer customer)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        //PUT - api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            // Check if the customer is available in Db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //copy all the values from the customer object of the request body to the database object
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter= customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId= customer.MembershipTypeId;
            
            //Commit the changes from memory to DB
            _context.SaveChanges();
        }

        //DELETE - api/Customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            // Check if the customer is available in Db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // Remove the particualr customer row from the DB context 
            _context.Customers.Remove(customerInDb);

            //Commit the changes from memory to DB
            _context.SaveChanges();
        }
    }
}
