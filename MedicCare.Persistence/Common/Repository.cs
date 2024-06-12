using MedicCare.App.Common;
using MedicCare.Domain.Common;

namespace MedicCare.Persistence.Common
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
