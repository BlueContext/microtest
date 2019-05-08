using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroTest.FileApi.Models;
using MicroTest.FileApi.ViewModels;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MicroTest.FileApi.Controllers
{
    [Produces("application/json")]
    [Route("api/dir")]
    public class DirController : Controller
    {
        private readonly AppConfiguration appConfiguration;
        //private readonly string userId;
        public DirController(IOptions<AppConfiguration> config)
        {
            appConfiguration = config.Value;
            //userId = HttpContext.User.Identity.Name.ToString();
        }

        // GET: api/Dir/5
        [HttpGet("{*id}")]
        public IActionResult Get([FromQuery]string id)
        {
            var req = id;
            if (Regex.IsMatch(id, @"^([a-zA-Z]\:)\/") || Regex.IsMatch(id, @"^([a-zA-Z]\:)\\") || Regex.IsMatch(id, @"^\\\\"))
            {
                id = id;
            }
            else
            {
                //id = @"\\" + (id.Replace("/", "\\"));
                id = @"\\" + (id.Replace(@"/", @"\"));
            }


            var folder = new NtfsDirectory(id);
            var result = new vmNtfsDirectory(folder, appConfiguration.ApiRootUrl, req);
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            json = json.Replace(@"\\", @"\");

            //return Ok(result);
            //return Ok(json);

            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}