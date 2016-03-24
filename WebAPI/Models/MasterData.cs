using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class MasterData
    {
        AppDbContext db;
        public MasterData()
        {
            db = new AppDbContext();
        }

        public List<IEnumerable> GetMasterData()
        {
            var results = new AppDbContext()
                .MultipleResults(@" select Id,text from masteritems where active =1 and mastertype='project'
                            select Id, text from masteritems where active = 1 and mastertype = 'module'
                            select Id, text from masteritems where active = 1 and mastertype = 'page'
                            select Id, text from masteritems where active = 1 and mastertype = 'worktype'
                            select Id, text from masteritems where active = 1 and mastertype = 'status'
                            select Id,title [text] from Features
                         ")
                .With<DDLItem>()
                .With<DDLItem>()
                .With<DDLItem>()
                .With<DDLItem>()
                .With<DDLItem>()
                .With<DDLItem>()
                .Execute();

            return results;
        }

       
    }
}