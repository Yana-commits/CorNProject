using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using REST_API.Confgurations;
using REST_API.Requests;
using REST_API.Responses;
using REST_API.Services.Interfaces;
using System.Net;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]

    public class ValueController : ControllerBase
    {
        private readonly string key = "123";
        private readonly HostConfig _hostConfig;

        private IProducedOperationsService _produceOperationsService;

        public ValueController(IProducedOperationsService produceOperationsService)
        {
            _produceOperationsService = produceOperationsService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> IsActual(IsActualRequest localKey)
        {
            var result = false;

            if (key.Equals(localKey.Key))
                result = true;

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetIdsResponse), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> AddOperation(List<OperationInfoReqest> operationInfo)
        {
            var result = await _produceOperationsService.AddOperationsAsync(operationInfo, key);

            return Ok(new GetIdsResponse() { Data = result });
        }

        [HttpGet]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> IsInBasket()
        {

            bool result = true;
            return Ok(result);
        }
    }
}
