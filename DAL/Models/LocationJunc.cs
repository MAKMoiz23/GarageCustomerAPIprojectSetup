namespace DAL.Models
{
    public partial class LocationJunc
    {
        public int ID { get; set; }
        public int? SettingID { get; set; }
        public int? LocationID { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public virtual Setting? Setting { get; set; }
    }
}
