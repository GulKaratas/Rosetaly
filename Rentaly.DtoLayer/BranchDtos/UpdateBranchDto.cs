using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DtoLayer.BranchDtos
{
    public class UpdateBranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
       
    }
}
