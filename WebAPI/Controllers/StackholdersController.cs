using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class StackholdersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<BaseCrudClass> Get()
        {
            //List<BaseCrudClass> list = new List<BaseCrudClass>() {
            //    new BaseCrudClass { Id=1,Name="user 1"},
            //    new BaseCrudClass{ Id=2, Name="user 2"},
            //    new BaseCrudClass{ Id=3, Name="user 3"}
            //};

            //return list;// new string[] { "file1", "file2" };
            return null;
        }

        // GET api/<controller>/5
        public IEnumerable<File> Get(int id)
        {

            return null; ;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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