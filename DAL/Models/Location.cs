namespace DAL.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public int RowID { get; set; }
        public string? Name { get; set; }
        public string? Descripiton { get; set; }
        public string? ArabicDescription { get; set; }
        public string? Address { get; set; }
        public string? ArabicAddress { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public int? TimeZoneID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public TimeSpan? Open_Time { get; set; }
        public TimeSpan? Close_Time { get; set; }
        public int? UserID { get; set; }
        public int? LicenseID { get; set; }
        public string? DeliveryServices { get; set; }
        public decimal? DeliveryCharges { get; set; }
        public string? DeliveryTime { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? StatusID { get; set; }
        public int? CustomerStatusID { get; set; }
        public string? CompanyCode { get; set; }
        public int? LandmarkID { get; set; }
        public string? Gmaplink { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsFeatured { get; set; }
        public string? ArabicName { get; set; }
        public string? Currency { get; set; }
        public string? VATNO { get; set; }
        public decimal? Tax { get; set; }
        public bool? AllowNegativeInventory { get; set; }
    }
}
