namespace Rentaly.DtoLayer.RentalDtos
{
    public class PendingRentalAnalysisDto
    {
        public int RentalId { get; set; }
        public string CarName { get; set; } = "";
        public string PlateNumber { get; set; } = "";
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool HasConflict { get; set; }
        public int? ConflictingRentalId { get; set; }
        public string ConflictingStatus { get; set; } = "";
        public string AlternativePlate { get; set; } = "";
    }
}
