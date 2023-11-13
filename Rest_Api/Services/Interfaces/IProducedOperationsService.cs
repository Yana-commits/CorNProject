using REST_API.Requests;

namespace REST_API.Services.Interfaces
{
    public interface IProducedOperationsService
    {
        Task<List<int?>?> AddOperationsAsync(List<OperationInfoReqest> operationInfos, string license);
    }
}
