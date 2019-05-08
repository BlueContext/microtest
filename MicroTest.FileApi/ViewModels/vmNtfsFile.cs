using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroTest.FileApi.Models;

namespace MicroTest.FileApi.ViewModels
{
        public class vmNtfsFile
    {
        //File properties
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool Exists { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastWrite { get; set; }

        //Scan properties
        public Uri FileLink { get; set; }
        public DateTime ScanTime { get; set; }
        //public string ScanUser { get; set; }

        //File Security
        public bool InheritanceEnabled { get; set; }
        public string Owner { get; set; }
        public List<vmNtfsAccessRule> AccessControl { get; set; }

        //CONSTRUCTOR
        public vmNtfsFile(NtfsFile dir, string apiRoot, string reqPath)
        {
            Name = dir.Name;
            FullPath = dir.FullPath;
            Exists = dir.Exists;
            Created = dir.Created;
            LastWrite = dir.LastWrite;
            FileLink = new Uri(apiRoot + "File?id=" + reqPath);
            ScanTime = DateTime.Now;
            //ScanUser = user;
            InheritanceEnabled = dir.InheritanceEnabled;
            Owner = dir.Owner;

            AccessControl = new List<vmNtfsAccessRule>();

            foreach (var ar in dir.AccessControlList)
            {
                AccessControl.Add(new vmNtfsAccessRule(ar));
            }
        }
    }
}