using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class FeaturesController : ApiController
    {

        // GET: api/Features
        public IEnumerable<Feature> Get()
        {
            var features = new FeaturesService();
            return features.GetTopFeatures();
        }

        public string Get(string filter)
        {
            if (string.IsNullOrEmpty(filter) == false)
            {
                var param = JsonConvert.DeserializeObject<FilterProperties>(filter);
            }
                return "value";
        }

        // GET: api/Features/5
        public Feature Get(int id)
        {
            var featureService = new FeaturesService();
            return featureService.GetFirst(id);
        }

        // POST: api/Features
        public void Post([FromBody]Feature value)
        {
            var featureService = new FeaturesService();
            featureService.Save(value);
        }

        // PUT: api/Features/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Features/5
        public void Delete(int id)
        {
        }
    }
}
