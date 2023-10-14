namespace DAL.Models
{
    public class ReportReview
    {
        public int ReportReveiwID { get; set; }
        public int? ReviewID { get; set; }
        public int? CustomerID { get; set; }
        public int? StatusID { get; set; }
        public string? Reason { get; set; }
        public int? LikeStatus { get; set; }
        public DateTime? Date { get; set; }
        public int? LikeValue { get; set; } = 0;
        public int? DislikeValue { get; set; } = 0;
    }
}
