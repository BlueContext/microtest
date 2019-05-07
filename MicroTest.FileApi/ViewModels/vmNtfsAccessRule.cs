using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroTest.FileApi.Models;

namespace MicroTest.FileApi.ViewModels
{
        public class vmNtfsAccessRule
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


        //CONSTRUCTOR
        public vmNtfsAccessRule(NtfsAccessRule r)
        {
            AccessControlType = r.AccessControlType;
            FileSystemRights = r.FileSystemRights;
            IdentityReference = r.IdentityReference;
            InheritanceFlags = r.InheritanceFlags;
            IsInherited = r.IsInherited;
            PropagationFlags = r.PropagationFlags;
        }
    }
}