using BAL.DTOs;
using DAL.Models;

namespace BAL.Services.IService
{
    public interface ISettingsService
    {
        Task<SettingsDTO?> GetSettings(int? LocationID);
        Task<CarMakeDTO> GetCarMake();
        Task<RspDTO> AddFeedback(Feedback model);
        Task<ReviewDTO> AddReview(Review model);
        Task<ReviewDTO> UpdateReview(Review model);
    }
}