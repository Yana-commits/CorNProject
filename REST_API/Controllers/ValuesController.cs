using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace REST_API.Controllers
{
   
    public class ValuesController : Controller
    {
        private readonly string key = "123";
        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    var bbb = 0;
        //    return Ok();
        //}

        [HttpGet]

        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> IsActual(string localKey)
        {
            var result = false;

            if(key.Equals(localKey))
                result = true;

            return Ok(result);
        }
    }

    
}
