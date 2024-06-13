using Dapper;
using MedicCare.App.Common;
using MedicCare.Domain.Common;

namespace MedicCare.Persistence.Common
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDbContext _context;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE id = {id}";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync <T>(query).ConfigureAwait(false);

            try
            {
                return result.First();
            }
            catch
            {
                throw new EntityNotFoundException();
            }
            
        }
    }
}
