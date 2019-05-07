using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroTest.FileApi.Models;

namespace MicroTest.FileApi.ViewModels
{
        public class vmNtfsDirectory
    {
        //Directory properties
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool Exists { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastWrite { get; set; }
        public bool TerminalLeaf { get; set; }

        //Scan properties
        public Uri DirLink { get; set; }
        public DateTime ScanTime { get; set; }
        public string ScanUser { get; set; }

        //Directory Contents
        public Dictionary<string, Uri> ChildFiles { get; set; }
        public Dictionary<string, Uri> ChildDirectories { get; set; }

        //Directory Security
        public bool InheritanceEnabled { get; set; }
        public string Owner { get; set; }
        public List<vmNtfsAccessRule> AccessControl { get; set; }

        //CONSTRUCTOR
        public vmNtfsDirectory(NtfsDirectory dir, string apiRoot, string reqPath, string user)
        {
            Name = dir.Name;
            FullPath = dir.FullPath;
            Exists = dir.Exists;
            Created = dir.Created;
            LastWrite = dir.LastWrite;
            TerminalLeaf = dir.TerminalLeaf;
            DirLink = new Uri(apiRoot + "Dir?id=" + reqPath.Replace(@"/", @"\"));
            ScanTime = DateTime.Now;
            ScanUser = user;
            InheritanceEnabled = dir.InheritanceEnabled;
            Owner = dir.Owner;
            ChildFiles = new Dictionary<string, Uri>();
            ChildDirectories = new Dictionary<string, Uri>();
            AccessControl = new List<vmNtfsAccessRule>();
            //foreach (var f in dir.ChildFiles)
            if(dir.ChildFiles.Any())
            {
                for (int i = 0; i < dir.ChildFiles.Count; i++)
                {
                    var uri = new Uri(apiRoot + "File?id=" + reqPath.Replace(@"/", @"\") + "\\" + dir.ChildFiles[i]);
                    ChildFiles.Add(dir.ChildFiles[i], uri);
                }
            }


            if (dir.ChildDirectories.Any())
            {
                for (int i = 0; i < dir.ChildDirectories.Count; i++)
                {
                    var uri = new Uri(apiRoot + "Dir?id=" + reqPath.Replace(@"/", @"\") + "\\" + dir.ChildDirectories[i]);
                    ChildDirectories.Add(dir.ChildDirectories[i], uri);
                }
            }

            foreach (var ar in dir.AccessControlList)
            {
                AccessControl.Add(new vmNtfsAccessRule(ar));
            }

        }
    }
}