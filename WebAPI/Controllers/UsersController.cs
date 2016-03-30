using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.Models.Common;

namespace webapi.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable Get()
        {
            var master = new MasterData();
            return master.GetUsers();
        }

        // GET api/<controller>/5
        public IEnumerable<File> Get(int id)
        {
            return null; ;
        }

        // POST api/<controller>
        public IEnumerable Post([FromBody]User value)
        {
            var master = new MasterData();
            master.SaveUser(value);
            return master.GetUsers();
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