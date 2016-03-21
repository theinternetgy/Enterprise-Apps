using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class FeaturesService
    {
        AppDbContext db = new AppDbContext();

        public bool Save(Feature feature)
        {
            bool result = true;

            string datetimeformat = "yyyy-MM-dd HH:mm:ss";//, dateformat= "YYYY-MM-DD";

            if (feature.Id == 0)
            {
                feature.GUID = Guid.NewGuid();
                feature.Created = DateTime.Now.ToString(datetimeformat);
                feature.Updated = DateTime.Now.ToString(datetimeformat);

                db.Featues.Add(feature);
            }
            else
            {
                feature.Updated = DateTime.Now.ToString(datetimeformat);

                db.Entry<Feature>(feature).State = System.Data.Entity.EntityState.Modified;
            }
            
            db.SaveChanges();

            return result;
        }

        public Feature GetFirst(int id)
        {
            var feature = db.Featues.Where(o => o.Id == id).FirstOrDefault();
            return feature;
        }

        public IEnumerable<FeatureListItem> GetTopFeatures()
        {
            var result = db.Database.SqlQuery<FeatureListItem>(@" SELECT top 10 a.[Id],a.[GUID],a.[Project],a.[Module],mod.text [ModuleName],a.[Page],page.text [PageName],a.[Parent],a.[Type],a.[Status],status.text [StatusName],a.[Title],a.[Description],a.[Keywords],a.[Created],a.[Updated] 
                                    FROM [dbo].[Features] a 
                                    Left outer join [Masters] status on status.Id=a.[Status] 
                                    Left outer join [Masters] mod on mod.Id=a.[Module] 
                                    Left outer join [Masters] page on page.Id=a.[Page] 
            ");
            return result;
        }
    }
}