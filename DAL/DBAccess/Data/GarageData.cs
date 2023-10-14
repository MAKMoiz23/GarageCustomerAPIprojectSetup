using DAL.DBAccess.IData;
using DAL.Repositories.Repository;
using Microsoft.Extensions.Configuration;

namespace DAL.DBAccess.Data
{
    public sealed class GarageData : GenericCrudRepository, IGarageData
    {
        public GarageData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("garageUAT");
        }
    }
}
