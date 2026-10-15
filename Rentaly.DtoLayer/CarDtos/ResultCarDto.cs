using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rentaly.DtoLayer.CarDtos
{
    public class ResultCarDto
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; } 
        public string ModelName { get; set; } 
        public string CategoryName { get; set; }
        public string PlateNumber { get; set; }

        public string? ImageUrl { get; set; }
        public int Year { get; set; }
        public int SeatCount { get; set; }
        public int LuggageCount { get; set; }

        public string FuelType { get; set; } 
        public string Transmission { get; set; } 
        public string? PerformanceInfo { get; set; }

        public decimal DailyPrice { get; set; }
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; }
    }
}