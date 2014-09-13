using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BabyBuddy.Api.Services;

namespace BabyBuddy.Api.Controllers
{
    public class MotionsController : ApiController
    {
        // GET: api/Motions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Motions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Motions
        public void Post([FromBody]string deviceId)
        {
            var sample = new SamplesService();
            sample.MotionDetected(deviceId);
        }

        // PUT: api/Motions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Motions/5
        public void Delete(int id)
        {
        }
    }
}
