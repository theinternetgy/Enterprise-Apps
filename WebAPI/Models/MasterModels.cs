using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{

    public class MasterCollection
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Projects { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Modules { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Pages { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable ParentFeatures { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Types { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Team { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Users { get; set; }
    }

    public class DDLItem
    {        
        public int Id { get; set; }
        public string value{ get; set; }
        public string text { get; set; }
    }

}