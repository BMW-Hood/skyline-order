using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private ITracer _tracer;

        public ValuesController(ITracer tracer)
        {
            _tracer = tracer;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var builder = _tracer.BuildSpan("Get::api/values/");
            builder.WithTag("machine.name", "machine1").WithTag("cpu.cores", 8);
            var startTime = DateTimeOffset.Now;
            var span = builder.WithStartTimestamp(startTime).Start();

            var logData = new List<KeyValuePair<string, object>>();
            logData.Add(KeyValuePair.Create<string, object>("handling number of events", 6));
            span.Log(DateTimeOffset.Now, logData);


            var @vent = "loop_finished";
            span.Log(DateTimeOffset.Now, @vent);
            span.Finish(DateTimeOffset.Now);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}