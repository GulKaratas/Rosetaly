using Rentaly.DtoLayer.CarDtos;

namespace Rentaly.WebUI.Models
{
    public class CarListViewModel
    {
        public List<ResultCarDto> Cars { get; set; } = new();
        public List<string> Brands { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<int> SeatCounts { get; set; } = new();
        public List<string> FuelTypes { get; set; } = new();
    }
}
