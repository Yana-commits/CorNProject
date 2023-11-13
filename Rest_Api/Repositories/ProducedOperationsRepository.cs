using REST_API.Data;
using REST_API.Models;
using REST_API.Repositories.Interfaces;

namespace REST_API.Repositories
{
    public class ProducedOperationsRepository : IProducedOperationsRepository
    {
        private readonly ApplcationDBContext _dbContext;

        public ProducedOperationsRepository(ApplcationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int?> Add(string file, int number,string license)
        {
            var item = await _dbContext.ProducedOperations.AddAsync(new ProducedOperation
            {
                Time = DateTime.Now,
                File = file,
                License = license,
                Produced = number
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }
    }
}