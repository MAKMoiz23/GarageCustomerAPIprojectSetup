using DAL.Models;

namespace BAL.DTOs
{
    public class SettingsDTO : RspDTO
    {
        public List<LocationDTO>? Location { get; set; }
        public List<Service>? Services { get; set; }
        public List<Setting>? Settings { get; set; }
        public List<Amenities>? Amenities { get; set; }
        public List<Landmark>? Landmarks { get; set; }
    }
}
