namespace DAL.Models
{
    public class Review
    {
        public int? ReviewID { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? Rate { get; set; }
        public double RateVal { get; set; }
        public string? Date { get; set; }
        public string? Image { get; set; }
        public int? LocationID { get; set; }
        public int? StatusID { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public string? ReportAbuse { get; set; }
        public int? CustomerID { get; set; }
        public List<ReportReview>? Customers { get; set; }
    }
}
