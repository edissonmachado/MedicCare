using MedicCare.App.Common;

namespace MedicCare.Persistence.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
