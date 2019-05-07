using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace MicroTest.FileApi.Models
{
    public class NtfsFile
    {
        //File properties
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool Exists { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastWrite { get; set; }

        //File security
        public bool InheritanceEnabled { get; set; }
        public string Owner { get; set; }
        public List<NtfsAccessRule> AccessControlList { get; set; }

        //CONSTRUCTOR
        public NtfsFile(string path)
        {
            FullPath = path;
            TestPath();
            AccessControlList = new List<NtfsAccessRule>();
            if (Exists == true)
            {
                GetDirInfo();
                GetSecurityInfo();
            }
        }

        //Private Methods
        private void TestPath()
        {
            if (File.Exists(FullPath))
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
            var f = new FileInfo(FullPath);
            Name = f.Name;
            Created = f.CreationTime;
            LastWrite = f.LastWriteTime;
        }

        private void GetSecurityInfo()
        {
            try
            {
                var fi = new FileInfo(FullPath);
                var sec = fi.GetAccessControl();
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