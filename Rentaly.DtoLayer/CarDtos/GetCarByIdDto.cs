using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DtoLayer.CarDtos
{
    public class GetCarByIdDto
    {
        public int CarId { get; set; }

        public string PlateNumber { get; set; } 
        public string? VIN { get; set; }

        public int CarModelId { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; } 

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } 

        public int BranchId { get; set; }
        public string BranchName { get; set; } 

        public string? ImageUrl { get; set; }

        public int Year { get; set; }
        public int Kilometer { get; set; }

        public int SeatCount { get; set; }
        public int LuggageCount { get; set; }

        public string FuelType { get; set; } 
        public string Transmission { get; set; }
        public string? PerformanceInfo { get; set; }

        public decimal DailyPrice { get; set; }
        public decimal DepositAmount { get; set; }

        public decimal Rating { get; set; }
        public int ReservationCount { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}