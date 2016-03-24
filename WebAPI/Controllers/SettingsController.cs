using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Controllers
{
    public class SettingsController : ApiController
    {
        // GET: api/Settings
        public IEnumerable<string> Get()
        {
            return new string[] { "Nothing to show" };
        }

        // GET: api/Settings/5
        public string Get(int id)
        {
            return "value";
        }

        public IEnumerable<MasterItem> Get(string filter)
        {
            IEnumerable<MasterItem> list = null;
            var service = new SettingsService();
            if (string.IsNullOrEmpty(filter) == false)
            {
                var param = JsonConvert.DeserializeObject<FilterSettings>(filter);
                if (param != null && string.IsNullOrEmpty(param.MasterType) == false)
                {
                    list = service.GetAll(param);
                }
            }

            return list;
        }

        // POST: api/Settings
        public void Post([FromBody] Models.Settings.MasterItem value)
        {
            if(value!=null && string.IsNullOrEmpty(value.MasterType) == false)
            {
                var service = new SettingsService();
                service.Save(value);
            }
        }

        // PUT: api/Settings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Settings/5
        public void Delete(int id)
        {
        }
    }
}
