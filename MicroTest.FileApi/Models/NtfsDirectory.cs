using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace MicroTest.FileApi.Models
{
    public class NtfsDirectory
    {
        //Directory properties
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool Exists { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastWrite { get; set; }
        public bool TerminalLeaf { get; set; }

        //Directory contents
        public List<string> ChildFiles { get; set; }
        public List<string> ChildDirectories { get; set; }

        //Directory security
        public bool InheritanceEnabled { get; set; }
        public string Owner { get; set; }
        public List<NtfsAccessRule> AccessControlList { get; set; }

        //CONSTRUCTOR
        public NtfsDirectory(string path)
        {
            FullPath = path;
            TestPath();
            ChildFiles = new List<string>();
            ChildDirectories = new List<string>();
            AccessControlList = new List<NtfsAccessRule>();
            if (Exists == true)
            {
                TerminalLeaf = false;
                ChildFiles = new List<string>();
                ChildDirectories = new List<string>();
                GetDirInfo();
                GetSecurityInfo();
            }
        }

        //Private Methods
        private void TestPath()
        {
            if (Directory.Exists(FullPath))
            {
                Exists = true;
            }
            else
            {
                Exists = false;
            }
        }

        private void GetDirInfo()
        {
            var di = new DirectoryInfo(FullPath);
            Name = di.Name;
            Created = di.CreationTime;
            LastWrite = di.LastWriteTime;
            var dirs = Directory.EnumerateDirectories(FullPath).AsParallel();
            var files = Directory.EnumerateFiles(FullPath);
            if (dirs.Count() == 0 && files.Count() == 0)
            {
                TerminalLeaf = true;
            }
            if (dirs.Count() > 0)
            {
                foreach (var d in dirs)
                {
                    ChildDirectories.Add(Path.GetFileName(d));
                }
            }

            if (files.Count() > 0)
            {
                foreach (var f in files)
                {
                    ChildFiles.Add(Path.GetFileName(f));
                }
            }
        }

        private void GetSecurityInfo()
        {
            try
            {
                var di = new DirectoryInfo(FullPath);
                var sec = di.GetAccessControl();
                if (sec.GetAccessRules(false, true, typeof(System.Security.Principal.SecurityIdentifier)).Count > 0)
                {
                    InheritanceEnabled = true;
                }
                Owner = sec.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                var rules = sec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

                foreach (FileSystemAccessRule r in rules)
                {
                    var rule = new NtfsAccessRule(r);
                    AccessControlList.Add(rule);
                }
            }
            catch (InvalidOperationException e)
            {

            }
        }
    }
}