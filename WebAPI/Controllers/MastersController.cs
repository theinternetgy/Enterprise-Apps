using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class MastersController : ApiController
    {
        AppDbContext db = new AppDbContext();

        public MasterCollection Get()
        {
            var list = new MasterCollection();

            MasterData mdata = new MasterData();
            var result = mdata.GetMasterData();

            list.Projects = result[0];
            list.Modules = result[1];
            list.Pages = result[2];
            list.Types = result[3];
            list.Status = result[4];
            list.ParentFeatures = result[5];

            return list;
        }
    }
}
