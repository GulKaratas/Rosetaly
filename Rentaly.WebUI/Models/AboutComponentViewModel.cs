using Rentaly.DtoLayer.About;

namespace Rentaly.WebUI.Models
{
    public class AboutComponentViewModel
    {
        public ResultAboutDto About { get; set; }
        public int TotalCars { get; set; }
        public int TotalRentals { get; set; }
        public int TotalReviews { get; set; }
        public int ExperienceYears { get; set; }
    }
}
