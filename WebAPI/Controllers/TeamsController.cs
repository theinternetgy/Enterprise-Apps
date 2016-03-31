using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class TeamsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable Get()
        {
            var master = new MasterData();
            var data = master.GetUserTeamMappingList();
            return data;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]UserTeamMapping value)
        {
            var master = new MasterData();
            master.SaveUserTeamMapping(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}