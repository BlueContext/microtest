using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroTest.FileApi.Models
{
    public class Service
    {
        public string ApiName { get; set; }
        public int ApiVersion { get; set; }
        public string HostName { get; set; }
        public Uri ApiRootUrl { get; set; }
        public Uri SwaggerUrl { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}