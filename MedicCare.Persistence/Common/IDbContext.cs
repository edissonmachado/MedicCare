using System.Data;

namespace MedicCare.Persistence.Common
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
