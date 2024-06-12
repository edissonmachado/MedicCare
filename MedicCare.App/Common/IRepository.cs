using MedicCare.Domain;
using System.Threading.Tasks;

namespace MedicCare.App.Common
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
    }
}
