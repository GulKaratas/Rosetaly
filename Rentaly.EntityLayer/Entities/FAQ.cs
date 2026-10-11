using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class FAQ
    {
        public int FAQId { get; set; }
        public string Question { get; set; } 
        public string Answer { get; set; }   
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }    
    }
}
