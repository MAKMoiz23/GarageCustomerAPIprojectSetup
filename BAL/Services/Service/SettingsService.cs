using BAL.DTOs;
using BAL.Services.IService;
using DAL.DBAccess.IData;
using DAL.GlobalAndCommon;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json;

namespace BAL.Services.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly IGarageData _gData;
        private readonly IConfiguration _config;

        public SettingsService(IGarageData gData, IConfiguration config)
        {
            _gData = gData;
            _config = config;
        }

        public async Task<SettingsDTO?> GetSettings(int? LocationID)
        {
            var rsp = new SettingsDTO();

            LocationID = LocationID == 0 ? null : LocationID;
            DateTime Date = DateTime.UtcNow.AddMinutes(180).Date;

            var ds = await _gData.LoadMultipleData<dynamic>("[dbo].[sp_GetLocationsV2_CAPI]", new { LocationID, Date });

            var _dtLocationInfo = JArray.Parse(JsonConvert.SerializeObject(ds[0])).ToObject<List<LocationDTO>>()?.ToList() ?? new List<LocationDTO>();
            var _dtServiceInfo = JArray.Parse(JsonConvert.SerializeObject(ds[1])).ToObject<List<DAL.Models.Service>>()?.ToList() ?? new List<DAL.Models.Service>();
            var _dtLocImageInfo = JArray.Parse(JsonConvert.SerializeObject(ds[2])).ToObject<List<LocationImagesDTO>>()?.ToList() ?? new List<LocationImagesDTO>();
            var _dtSettingInfo = JArray.Parse(JsonConvert.SerializeObject(ds[3])).ToObject<List<Setting>>()?.ToList() ?? new List<Setting>();
            var _dtAmenitiesInfo = JArray.Parse(JsonConvert.SerializeObject(ds[4])).ToObject<List<Amenities>>()?.ToList() ?? new List<Amenities>();
            var _dtReviewsInfo = JArray.Parse(JsonConvert.SerializeObject(ds[5])).ToObject<List<Review>>()?.ToList() ?? new List<Review>();
            var _dtDiscountInfo = JArray.Parse(JsonConvert.SerializeObject(ds[6])).ToObject<List<DiscountDTO>>()?.ToList() ?? new List<DiscountDTO>();
            var _dtServiceInfoAll = JArray.Parse(JsonConvert.SerializeObject(ds[7])).ToObject<List<DAL.Models.Service>>()?.ToList() ?? new List<DAL.Models.Service>();
            var _dtAminitiesInfoAll = JArray.Parse(JsonConvert.SerializeObject(ds[8])).ToObject<List<Amenities>>()?.ToList() ?? new List<Amenities>();
            var _dtLandmarks = JArray.Parse(JsonConvert.SerializeObject(ds[9])).ToObject<List<Landmark>>()?.ToList() ?? new List<Landmark>();
            var _dtSettingLocation = JArray.Parse(JsonConvert.SerializeObject(ds[10])).ToObject<List<LocationJunc>>()?.ToList() ?? new List<LocationJunc>();
            var _dtReviewCustomer = new List<ReportReview>();
            if (12 <= ds.Count) JArray.Parse(JsonConvert.SerializeObject(ds[11])).ToObject<List<ReportReview>>()?.ToList();

            AdminURLs(out string? adminUrl, out string? cAdminUrl, out string? cpAdminUrl, out string? apiUrl);

            rsp.Location = _dtLocationInfo.ToList();
            rsp.Services = _dtServiceInfoAll.ToList();
            rsp.Settings = _dtSettingInfo.ToList();
            rsp.Amenities = _dtAminitiesInfoAll.ToList();
            rsp.Landmarks = _dtLandmarks.ToList();

            foreach (var j in rsp.Settings)
            {
                j.Locations = _dtSettingLocation.Where(x => x.SettingID == j.ID).ToList();
                j.Image = j.Image == null ? null : cAdminUrl + j.Image;
            }
            foreach (var j in rsp.Landmarks)
            {
                j.Image = j.Image == null ? null : cAdminUrl + j.Image;
            }
            foreach (var j in rsp.Amenities)
            {
                j.Image = j.Image == null ? null : cAdminUrl + j.Image;
            }
            foreach (var j in rsp.Services)
            {
                j.Image = j.Image == null ? null : cAdminUrl + j.Image;
            }
            foreach (var i in _dtLocationInfo)
            {
                var opening = Global.TimespanToDecimal(TimeSpan.Parse(i.OpenTime ?? ""));
                var closing = Global.TimespanToDecimal(TimeSpan.Parse(i.CloseTime ?? ""));

                if (opening > closing)
                {
                    i.OpenTime = Global.DateParse(DateTime.UtcNow.AddMinutes(180).ToString("MM/dd/yyyy") + ' ' + i.OpenTime);
                    i.CloseTime = Global.DateParse(DateTime.UtcNow.AddDays(1).AddMinutes(180).ToString("MM/dd/yyyy") + ' ' + i.CloseTime);
                }
                else
                {
                    i.OpenTime = Global.DateParse(DateTime.UtcNow.AddMinutes(180).ToString("MM/dd/yyyy") + ' ' + i.OpenTime);
                    i.CloseTime = Global.DateParse(DateTime.UtcNow.AddMinutes(180).ToString("MM/dd/yyyy") + ' ' + i.CloseTime);
                }

                i.BrandImage = i.BrandImage == null ? null : adminUrl + i.BrandImage;
                i.Services = _dtServiceInfo.Where(x => x.LocationID == i.LocationID).ToList();
                i.LocationImages = _dtLocImageInfo.Where(x => x.LocationID == i.LocationID).ToList();
                i.Amenities = _dtAmenitiesInfo.Where(x => x.LocationID == i.LocationID).ToList();
                i.Discounts = _dtDiscountInfo.Where(x => x.LocationID == i.LocationID).ToList();
                foreach (var j in i.LocationImages)
                {
                    j.ImageURL = j.ImageURL == null ? null : cAdminUrl + j.ImageURL;
                }
                foreach (var j in i.Amenities)
                {
                    j.Image = j.Image == null ? null : cAdminUrl + j.Image;
                }
                foreach (var j in i.Services)
                {
                    j.Image = j.Image == null ? null : cAdminUrl + j.Image;
                }

                foreach (var j in i.Discounts)
                {
                    j.FromDate = Global.DateParse(j.FromDate ?? "");
                    j.ToDate = Global.DateParse(j.ToDate ?? "");
                }
                i.Reviews = _dtReviewsInfo.Where(x => x.LocationID == i.LocationID).ToList();

                var rating1 = i.Reviews.Where(x => x.RateVal > 4 && x.RateVal <= 5).Count();
                var rating2 = i.Reviews.Where(x => x.RateVal > 3 && x.RateVal <= 4).Count();
                var rating3 = i.Reviews.Where(x => x.RateVal > 2 && x.RateVal <= 3).Count();
                var rating4 = i.Reviews.Where(x => x.RateVal > 1 && x.RateVal <= 2).Count();
                var rating5 = i.Reviews.Where(x => x.RateVal >= 0 && x.RateVal <= 1).Count();
                i.ReviewCountDetails = new int[5] { rating1, rating2, rating3, rating4, rating5 };
                foreach (var j in i.Reviews)
                {
                    j.Customers = _dtReviewCustomer.Where(x => x.ReviewID == j.ReviewID).ToList();
                    j.Date = Global.DateParse(j.Date ?? "");
                }
            }

            rsp.Status = 1;
            rsp.Description = "Successful";

            return rsp;
        }

        private void AdminURLs(out string? adminUrl, out string? cAdminUrl, out string? cpAdminUrl, out string? apiUrl)
        {
            adminUrl = _config["URL:AdminURL"];
            cpAdminUrl = _config["URL:CpAdminURL"];
            apiUrl = _config["URL:ApiURL"];
            cAdminUrl = _config["URL:CAdminURL"];
        }

        public async Task<CarMakeDTO> GetCarMake()
        {
            var rsp = new CarMakeDTO();

            var ds = await _gData.LoadMultipleData<dynamic>("[dbo].[sp_GetCarMake_CAPI]", new { });

            rsp.CarMake = JArray.Parse(JsonConvert.SerializeObject(ds[0])).ToObject<List<CarMakeListDTO>>()?.ToList() ?? new List<CarMakeListDTO>();
            var _dtCarModels = JArray.Parse(JsonConvert.SerializeObject(ds[1])).ToObject<List<CarModelListDTO>>()?.ToList() ?? new List<CarModelListDTO>();

            AdminURLs(out string? adminUrl, out string? cAdminUrl, out string? cpAdminUrl, out string? apiUrl);


            foreach (var i in rsp.CarMake)
            {
                i.ImagePath = i.ImagePath == null ? null : cpAdminUrl + i.ImagePath;
                i.CarModels = _dtCarModels.Where(x => x.MakeID == i.MakeID).ToList();
            }

            rsp.Status = 1;
            rsp.Description = "Successful";

            return rsp;
        }
        public async Task<RspDTO> AddFeedback(Feedback model)
        {
            RspDTO rsp = new();

            await _gData.SaveData<dynamic>("[dbo].[sp_AddFeedback]",
                                           new { ParamTable1 = JsonConvert.SerializeObject(model) });

            rsp.Status = 1;
            rsp.Description = "Feedback Added";

            return rsp;
        }

        public async Task<ReviewDTO> AddReview(Review model)
        {
            ReviewDTO rsp = new ();

            var rspReviews = await _gData.SaveQueryable<Review, dynamic>("[dbo].[sp_InsertReview_CAPI_NEW]",
                                                                      new { paramTable1 = JsonConvert.SerializeObject(model) });

            rsp.Reviews = rspReviews;
            rsp.Status = 1;
            rsp.Description = "Review Added Successfully";
            return rsp;
        }

        public async Task<ReviewDTO> UpdateReview(Review model)
        {
            ReviewDTO rsp = new();
            var rspReviews = await _gData.SaveQueryable<Review, dynamic>("[dbo].[sp_UpdateReview_CAPI_NEW]",
                                                                      new { paramTable1 = JsonConvert.SerializeObject(model) });
            rsp.Reviews = rspReviews;
            rsp.Status = 1;
            rsp.Description = "Review Update Successfully";
            return rsp;
        }
    }
}
