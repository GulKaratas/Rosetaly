using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DtoLayer.About
{
    public class ResultAboutDto
    {
        public int AboutId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string FirstAdvantageTitle { get; set; }
        public string FirstAdvantageDescription { get; set; }
        public string SecondAdvantageTitle { get; set; }
        public string SecondAdvantageDescription { get; set; }
    }
}
