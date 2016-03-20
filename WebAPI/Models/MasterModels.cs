using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{

    public class MasterCollection
    {
        public IEnumerable Projects { get; set; }
        public IEnumerable Modules { get; set; }
        public IEnumerable Pages { get; set; }
        public IEnumerable ParentFeatures { get; set; }
        public IEnumerable Types { get; set; }
        public IEnumerable Status { get; set; }
    }

    public class DDLItem
    {        
        public int Id { get; set; }
        public string value{ get; set; }
        public string text { get; set; }
    }

}