using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class FilesController : ApiController
    {
        AppDbContext db = new AppDbContext();
        // GET api/<controller>
        public IEnumerable<BaseCrudClass> Get()
        {
            //List<BaseCrudClass> list = new List<BaseCrudClass>() {
            //    new BaseCrudClass { Id=1,Name="file 1"},
            //    new BaseCrudClass{ Id=2, Name="file 2"}, 
            //    new BaseCrudClass{ Id=3, Name="file 3"}
            //};

            //return list;// new string[] { "file1", "file2" };
            return null;
        }

        // GET api/<controller>/5
        public IEnumerable<File> Get(int id)
        {
            var list = db.Files.Where(o=>o.FeatureId==id);//.ToList();

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