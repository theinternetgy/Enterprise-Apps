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

        public MasterCollection Get(string filter)
        {
            MasterCollection list = null;

            if (string.IsNullOrEmpty(filter) == false)
            {
                MasterData mdata = new MasterData();
                list = new MasterCollection();

                if (filter == "teammapping")
                {

                    list.Team = mdata.GetMasterData(CrudTypes.Team);
                    list.Users = mdata.GetUsers();
                }
                else
                {
                    CrudTypes myStatus;
                    Enum.TryParse(filter, out myStatus);

                    if (myStatus==CrudTypes.Team)
                        list.Team = mdata.GetMasterData(myStatus);
                    else if (myStatus == CrudTypes.Users)
                        list.Users = mdata.GetMasterData(myStatus);
                }
            }

            return list;
        }
    }
}
