using CorNProject.Requests;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CorNProject.Services
{
    public class StatusChecker
    {
        private readonly string key = "123";
        private readonly string adress = "http://localhost:5269/api/v1/value/isactual";
        private int time = 10000;
        private bool loopStart = true;

        private System.Timers.Timer? _timer;

        public Action<bool>? isActualKey;

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
            
            bool result = await IsActual(new IsActualRequest() { Key = key});
            isActualKey?.Invoke(result);
        }
        public async Task<bool> IsActual(IsActualRequest key)
        {
            var result = await HttpRequests.SendAsync<bool, IsActualRequest>(adress,
                HttpMethod.Post,
               key);

            if(result==null)
                result= false;
            //var result = true;

            return result;
        }
    }
}
