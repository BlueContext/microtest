using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace MicroTest.FileApi.Models
{
    public class NtfsAccessRule
    {
        //This class represents the data included in a FileSystemAccessRule object
        // see the class definition in the System.Security.AccessControl for more information

        public string AccessControlType { get; set; }
        //public string AccessMask { get; set; }
        public string FileSystemRights { get; set; }
        public string IdentityReference { get; set; }
        public string InheritanceFlags { get; set; }
        public bool IsInherited { get; set; }
        public string PropagationFlags { get; set; }

        public NtfsAccessRule(FileSystemAccessRule ar)
        {
            AccessControlType = ar.AccessControlType.ToString();
            //AccessMask = ar.AccessMask.ToString();
            FileSystemRights = ar.FileSystemRights.ToString();
            IdentityReference = ar.IdentityReference.ToString();
            InheritanceFlags = ar.InheritanceFlags.ToString();
            IsInherited = ar.IsInherited;
            PropagationFlags = ar.PropagationFlags.ToString();
        }
    }
}