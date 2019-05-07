using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroTest.FileApi.Models
{
        public class MyService
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Endpoint { get; set; }
        public string Swagger { get; set; }
        public string ServiceCheck { get; set; }
        public string Environment { get; set; }
        public string HostName { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}