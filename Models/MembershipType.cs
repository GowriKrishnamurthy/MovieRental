using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    [Table("MembershipType")]
    public class MembershipType
    {
        public byte Id { get; set; }

        public short SignUpFee { get; set; }
        
        //largest number is 12 here for 12 months. so, lets use byte
        public byte DurationInMonths { get; set; }
        
        // its a % between 0 and 100%, so, lets use byte
        public byte DiscountRate { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        
        //Enum for the Membership type names - to be used in the business logic 
        // avoid hardcoding of the typeids
        public enum MembershipTypeName : byte
        {
            Unknown = 0,
            PayAsYouGo = 1,
            Monthly = 2,
            Quarterly = 3,
            Annual = 4
        }
        //Alternatively readonly static variables can be specified ONLY for the typenames used in the business logic
        // public static readonly byte Unknown = 0;
        // public static readonly byte PayAsYouGo = 1;
    }
}