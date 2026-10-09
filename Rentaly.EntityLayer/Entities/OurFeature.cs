using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class OurFeature
    {
        public int OurFeatureId { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }  
        public string IconCssClass { get; set; } 
        public int DisplayOrder { get; set; }    
    }
}
