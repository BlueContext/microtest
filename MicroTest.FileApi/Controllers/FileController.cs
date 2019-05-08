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
    [Route("api/file")]
    public class FileController : Controller
    {
        private readonly AppConfiguration appConfiguration;
        //private readonly string userId = "GET USER HERE";
        public FileController(IOptions<AppConfiguration> config)
        {
            appConfiguration = config.Value;
        }

        // GET: api/File/5
        [HttpGet("{*id}")]
        public IActionResult Get([FromQuery]string id)
        {
            var req = id;

            if (Regex.IsMatch(id, @"^([a-zA-Z]\:)\/") || Regex.IsMatch(id, @"^([a-zA-Z]\:)\\"))
            {
                id = id;
            }
            else
            {
                //id = @"\\" + (id.Replace("/", "\\"));
                id = @"\\" + (id.Replace(@"/", @"\"));
            }
            var file = new NtfsFile(id);
            var result = new vmNtfsFile(file, appConfiguration.ApiRootUrl, req);
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            json = json.Replace(@"\\", @"\");
            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };

        }
    }
}