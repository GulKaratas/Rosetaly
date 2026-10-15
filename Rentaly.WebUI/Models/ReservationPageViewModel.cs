using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CarDtos;

namespace Rentaly.WebUI.Models
{
    public class ReservationPageViewModel
    {
        public GetCarByIdDto? Car { get; set; }
        public List<ResultBranchDto> Branches { get; set; } = new();
        public ReservationForm Form { get; set; } = new();
        public int ActiveStep { get; set; } = 1;
        public bool Submitted { get; set; }
        public string ErrorMessage { get; set; } = "";
    }

    public class ReservationForm
    {
        public int CarId { get; set; }
        public string PickupDate { get; set; } = "";
        public string PickupTime { get; set; } = "10:00";
        public string ReturnDate { get; set; } = "";
        public string ReturnTime { get; set; } = "10:00";
        public int PickupBranch { get; set; }
        public int ReturnBranch { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string NationalId { get; set; } = "";
        public string LicenseNo { get; set; } = "";
        public string LicenseDate { get; set; } = "";
    }
}
