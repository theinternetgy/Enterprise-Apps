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
                .MultipleResults(@" select Id,text from masters where active =1 and mastertype='project'
                            select Id, text from masters where active = 1 and mastertype = 'module'
                            select Id, text from masters where active = 1 and mastertype = 'page'
                            select Id, text from masters where active = 1 and mastertype = 'worktype'
                            select Id, text from masters where active = 1 and mastertype = 'status'
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

        public IEnumerable<FeatureListItem> GetTopFeatures()
        {
            var result = db.Database.SqlQuery<FeatureListItem>(@" SELECT top 10 a.[Id],a.[Project],a.[Module],mod.text [ModuleName],a.[Page],page.text [PageName],a.[Parent],a.[Type],a.[Status],status.text [StatusName],a.[Title],a.[Description],a.[Keywords],a.[Created],a.[Updated] 
                                    FROM [dbo].[Features] a 
                                    Left outer join [Masters] status on status.Id=a.[Status] 
                                    Left outer join [Masters] mod on mod.Id=a.[Module] 
                                    Left outer join [Masters] page on page.Id=a.[Page] 
            ");
            return result;
        }
    }
}