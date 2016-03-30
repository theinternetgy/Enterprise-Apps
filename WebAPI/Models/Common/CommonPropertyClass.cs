using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapi.Models.Common
{
    [NotMapped]
    public class FieldPropertyClass
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}