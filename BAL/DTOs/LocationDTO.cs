using DAL.Models;

namespace BAL.DTOs
{
    public class LocationDTO
    {
        public int LocationID { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public string? Description { get; set; }
        public string? ArabicDescription { get; set; }
        public string? Address { get; set; }
        public string? ArabicAddress { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public string? Rating { get; set; }
        public string? Website { get; set; }
        public int? LandmarkID { get; set; }
        public string? GMapLink { get; set; }
        public bool? IsFeatured { get; set; }
        public int? ReviewCount { get; set; }
        public int[] ReviewCountDetails { get; set; } = Array.Empty<int>();
        public List<Service> Services { get; set; } = new ();
        public List<LocationImagesDTO> LocationImages { get; set; } = new();
        public List<Amenities> Amenities { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<DiscountDTO> Discounts { get; set; } = new();
    }
}
