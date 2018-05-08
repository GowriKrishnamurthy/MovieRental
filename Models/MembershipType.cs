using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public short SignUpFee { get; set; }
        //largest number is 12 here for 12 months. so, lets use byte
        public byte DurationInMonths { get; set; }
        
        // its a % between 0 and 100%, so, lets use byte
        public byte DiscountRate { get; set; }
    }
}