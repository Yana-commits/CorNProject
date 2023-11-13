using REST_API.Repositories.Interfaces;
using REST_API.Requests;
using REST_API.Services.Interfaces;

namespace REST_API.Services
{
    public class ProducedOperatonsServce : IProducedOperationsService
    {
        private IProducedOperationsRepository _producedOperationsRepository;

        public ProducedOperatonsServce(IProducedOperationsRepository producedOperationsRepository)
        {
            _producedOperationsRepository = producedOperationsRepository;
        }

        public async Task<List<int?>?> AddOperationsAsync(List<OperationInfoReqest> operationInfos, string license)
        {
            var idsList = new List<int?>();
            foreach (var operationInfo in operationInfos)
            {
                var result = await _producedOperationsRepository.Add(operationInfo.FilePath,
                    operationInfo.ProducedOperations, license);

                idsList.Add(result);
            }
           
            return idsList;
        }
    }
}
