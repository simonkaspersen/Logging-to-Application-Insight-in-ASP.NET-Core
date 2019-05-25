using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Logging_to_Application_Insight_in_ASP.NET_Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Logging_to_Application_Insight_in_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILog _logger;

        public ValuesController(ILogFactory logFactory)
        {
            _logger = logFactory.GetLogger(GetType());

        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var values = new []{"value1", "value2"};
            _logger.Info($"Request made, returning: {JsonConvert.SerializeObject(values)}");
            return values;
        }
        
        // GET api/values
        [HttpGet("ThrowPlease")]
        public IActionResult ThrowMePlox()
        {
            try
            {
                throw new ArithmeticException("Dividing by 0?? Good luck");
            }
            catch (Exception e)
            {
                _logger.Error("This was bad. not good", e);
                return BadRequest();
            }
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
