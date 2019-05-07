using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroTest.FileApi
{
        public class AppConfiguration
    {
        public string ApiName { get; set; }
        public int ApiVersion { get; set; }
        public string HostName { get; set; }
        public string ApiRootUrl { get; set; }
        public string SwaggerUrl { get; set; }
        public int GroupMembersEnumerationLimit { get; set; }
    }
}