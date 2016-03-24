using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Settings
{
    public class MasterItem : BaseEntity
    {
        public string MasterType { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
    }

    public class FilterSettings: MasterItem
    {

    }
}