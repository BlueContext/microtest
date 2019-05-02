using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MicroTest.Api.QueueClient;

namespace MicroTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class QueueController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "The JSON", "is coming", "from inside", "the container!!!" };
        }

        [HttpGet("next", Name="GetNext")]
        public ActionResult<string> GetNext()
        {
            string result = "";

            var receiver = new Receive("micro-mq");
            result = receiver.ReceiveNextMessage();
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            var sender = new Send("micro-mq");
            sender.SendMessage(value);
            return Ok(value);
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