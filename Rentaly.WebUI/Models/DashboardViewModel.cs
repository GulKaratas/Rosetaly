using Rentaly.DtoLayer.CarDtos;
using Rentaly.DtoLayer.RentalDtos;

namespace Rentaly.WebUI.Models
{
    public class DashboardViewModel
    {
        public int CarCount { get; set; }
        public int AvailableCarCount { get; set; }
        public int CustomerCount { get; set; }
        public int BranchCount { get; set; }
        
        public int PendingRentalCount { get; set; }
        public int ApprovedRentalCount { get; set; }
        public decimal ApprovedRevenue { get; set; }

        public List<ResultRentalDto> RecentRentals { get; set; } = new();
        public List<ResultRentalDto> TodayPickups { get; set; } = new();
        public List<ResultRentalDto> TodayReturns { get; set; } = new();
        public List<ResultCarDto> RecentCars { get; set; } = new();
        public string MorningBriefing { get; set; } = "";
        public List<PendingRentalAnalysisDto> PendingAnalyses { get; set; } = new();
    }
}