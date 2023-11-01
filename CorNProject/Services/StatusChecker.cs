using CorNProject.Enums;
using CorNProject.Requests;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CorNProject.Services
{
    public class StatusChecker
    {
        private static StatusChecker instance;

        private StatusChecker()
        { }
        public static StatusChecker Instance()
        {
            if (instance == null)
                instance = new StatusChecker();

            return instance;
        }

        //private readonly string _key = "123";
        //private readonly string adress = "http://localhost:5269/api/v1/value/isactual";
        private int time = 10000;
        private bool loopStart = true;

        private System.Timers.Timer? _timer;

        public Action<StatusEnum>? isActualKey;

        public void SetTimer()
        {
            _timer = new System.Timers.Timer();

            _timer.Elapsed += OnTimeEvent;
            _timer.Enabled = true;
        }
        public async void OnTimeEvent(Object obj, ElapsedEventArgs e)
        {
            if (loopStart)
            {
                _timer.Interval = time;
                _timer.AutoReset = true;
                loopStart = false;
            }

          var status =  await GetStatus();
            isActualKey?.Invoke(status);
        }
        public async Task<StatusEnum> GetStatus()
        {
            var config = new ConfigService();
            bool result = await IsActual(config);
            var status = CheckKey(result, config);

            return status;
        }
        public async Task<bool> IsActual(ConfigService config)
        {
           
            IsActualRequest key = new IsActualRequest() { Key = config.LicenseKey };

            var result = await HttpRequests.SendAsync<bool, IsActualRequest>(config.Addresses.ServerConnection,
                HttpMethod.Post,
               key);

            return result;
        }

        private StatusEnum CheckKey(bool isActual, ConfigService config)
        {
            StatusEnum status = StatusEnum.OnlineVersion;

            if (CheckConnection.CheckForInternetConnection(config))
            {
                if (isActual)
                    status = StatusEnum.OnlineVersion;

                else
                    status = StatusEnum.NoLicense;
            }
            else
            {
                status = StatusEnum.NoConnection;
            }

            return status;
        }
    }
}
