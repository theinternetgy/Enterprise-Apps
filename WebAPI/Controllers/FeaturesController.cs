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

        AppDbContext db = new AppDbContext();

        // GET: api/Features
        public IEnumerable<Feature> Get()
        {
            var mdata = new MasterData();
            return mdata.GetTopFeatures();
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
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Features
        public void Post([FromBody]Feature value)
        {
            db.Featues.Add(value);
            db.SaveChangesAsync();
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
