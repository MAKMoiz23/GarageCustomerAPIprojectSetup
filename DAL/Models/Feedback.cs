namespace DAL.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public string? About { get; set; }
        public string? Topic { get; set; }
        public string? Details { get; set; }
        public int? StatusID { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CustomerID { get; set; }
    }
}
