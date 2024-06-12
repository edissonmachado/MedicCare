using MedicCare.Domain.Common;
using System.Threading.Tasks;

namespace MedicCare.App.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(int id);
    }
}
