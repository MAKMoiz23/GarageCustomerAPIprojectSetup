namespace DAL.Models
{
    public class Setting
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? ArabicTitle { get; set; }
        public string? Description { get; set; }
        public string? ArabicDescription { get; set; }
        public string? Image { get; set; }
        public string? AlternateImage { get; set; }
        public string? PageName { get; set; }
        public string? Type { get; set; }
        public int? DisplayOrder { get; set; }
        public List<LocationJunc>? Locations { get; set; }
    }
}
