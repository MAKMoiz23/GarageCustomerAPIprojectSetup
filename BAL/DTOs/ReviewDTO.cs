using DAL.Models;

namespace BAL.DTOs
{
    public class ReviewDTO : RspDTO
    {
        public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
    }
}
