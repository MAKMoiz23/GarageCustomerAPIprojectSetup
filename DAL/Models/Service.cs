namespace DAL.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ArabicServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public string? ArabicServiceDescription { get; set; }
        public string? Image { get; set; }
        public int? DisplayOrder { get; set; }
        public int? LocationID { get; set; }
        public bool? IsServices { get; set; }
    }
}
