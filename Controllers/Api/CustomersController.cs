﻿using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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
        public IHttpActionResult GetCustomers()
        {
            // using Select extension method of Linq
            // Mapper converts Customer to CustomerDto
            var customerDto= _context.Customers
                .Include(m=>m.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDto);
        }

        //GET single customer -  api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // customer object thats returned can be simply mapped to Dto,passing the customer object.
            if (customer == null)
               return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer)); 
        }

        //POST - api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                return BadRequest();

            // Here we need to map our DTO to domain object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //Customer object has the ID that is generated by the DB.
            //We need to add this ID to our DTO.
            customerDto.Id = customer.Id;

            //Return the Created Status Code as 201 Created(as per the RESTful Convention
            // Param:1-URI,2-object created.
            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }

        //PUT - api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                return BadRequest();

            // Check if the customer is available in Db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            // copy all the values from the customer DTO object of the request body to the database object
            //Mapper.Map<CustomerDto, Customer>(customerDto,customerInDb);
            // Mapper.Map above can replaced as below
            Mapper.Map(customerDto, customerInDb);

            /* 
             * Code below used for manual mapping can be removed as we use AutoMapper
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter= customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId= customer.MembershipTypeId;
            */
            //Commit the changes from memory to DB
            _context.SaveChanges();
            return Ok();
        }

        //DELETE - api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            // Check if the customer is available in Db
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            // Remove the particualr customer row from the DB context 
            _context.Customers.Remove(customerInDb);

            //Commit the changes from memory to DB
            _context.SaveChanges();
            return Ok();
        }
    }
}
