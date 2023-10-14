namespace BAL.DTOs
{
    public class DiscountDTO
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? LocationID { get; set; }
        public int? DiscountID { get; set; }
    }
}
