using DAL.DBAccess.IData;
using DAL.Repositories.Repository;
using Microsoft.Extensions.Configuration;

namespace DAL.DBAccess.Data
{
    public sealed class GarageCustomerData : GenericCrudRepository, IGarageCustomerData
    {
        public GarageCustomerData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("garageCustomerUAT");
        }
    }
}
