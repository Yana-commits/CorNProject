using REST_API.Requests;

namespace REST_API.Repositories.Interfaces
{
    public interface IProducedOperationsRepository
    {
        Task<int?> Add(string file, int number, string license);
    }
}
