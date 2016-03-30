using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Common
{
    public class User:BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public bool Active { get; set; }
    }
}