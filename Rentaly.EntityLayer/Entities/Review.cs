using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
 
        public class Review
        {
            public int ReviewId { get; set; }

            public int CarId { get; set; }
            public Car Car { get; set; } 

            public int CustomerId { get; set; }
            public Customer Customer { get; set; } 

            public int Score { get; set; }
            public string? Comment { get; set; }
            public DateTime CreatedAt { get; set; } 
        }
    
}
