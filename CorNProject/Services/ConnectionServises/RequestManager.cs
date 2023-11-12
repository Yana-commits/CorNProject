using CorNProject.Data;
using CorNProject.Requests;
using CorNProject.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject.Services.ConnectionServises
{
    internal class RequestManager
    {
        private ConfigService _config;

        public RequestManager()
        {
            _config = new ConfigService();
        }

        public async Task SetLoggsIntoDatabase(List<MyLogger> myLoggers)
        {
            var infoList = new List<OperationInfoReqest>();

            foreach (var item in myLoggers)
            {
                var request = new OperationInfoReqest()
                {
                    FilePath = item.FileName,
                    ProducedOperations = item.Number
                };
                infoList.Add(request);
            }

            var result = await HttpRequests.SendAsync<GetIdsResponse, List<OperationInfoReqest>>($"{_config.Addresses.ServerConnection}/addoperation",
               HttpMethod.Post,
              infoList);
        }

        public async Task<bool> IsActual()
        {

            IsActualRequest key = new IsActualRequest() { Key = _config.LicenseKey };

            var result = await HttpRequests.SendAsync<bool, IsActualRequest>($"{_config.Addresses.ServerConnection}/isactual",
                HttpMethod.Post,
               key);

            return result;
        }
    }
}
