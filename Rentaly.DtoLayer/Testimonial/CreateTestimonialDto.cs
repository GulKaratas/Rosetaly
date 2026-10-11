using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DtoLayer.Testimonial
{
    public class CreateTestimonialDto 
    {
        public string ClientName { get; set; }  
        public string Title { get; set; }       
        public string Comment { get; set; }     
        public string ImageUrl { get; set; }
    }
}
