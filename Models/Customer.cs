using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter{ get; set; }

        // Associate Customer Model class with MembershipType
        public MembershipType MembershipType{ get; set; }
        
        //foreign key
        public byte MembershipTypeId { get; set; }
    }
}