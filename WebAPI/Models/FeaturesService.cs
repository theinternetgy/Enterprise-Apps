using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Models
{
    public class FeaturesService
    {
        AppDbContext db = new AppDbContext();

        public Feature Save(Feature feature)
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

                var existingFeature = db.Featues.AsNoTracking().Where(o=>o.Id == feature.Id)
                                            .Include(s => s.Stackholders)
                                            .Include(s => s.Files)
                                            .FirstOrDefault<Feature>();

                var addedStackholders = feature.Stackholders.Except(existingFeature.Stackholders, tchr => tchr.Id);
                addedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Added);

                var modifiedStackholders = feature.Stackholders.Except(addedStackholders, tchr => tchr.Id);
                modifiedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Modified);


                //Find newly added Stackholders by updatedStackholders (teacher came from client sided) Minus existingStackholders = newly added Stackholders
                //var addedStackholders = feature.Stackholders.Except<Stackholder>(existingFeature.Stackholders, new GenericEqualityComparer<Stackholder>());
                //addedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Added);

                ////3- Find deleted Stackholders by existing Stackholders - updatedStackholders = deleted Stackholders
                //var deletedStackholders = existingFeature.Stackholders.Except(feature.Stackholders, new GenericEqualityComparer<Stackholder>());
                //deletedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Deleted);

                //4- Find modified Stackholders by updatedStackholders - addedStackholders = modified Stackholders
                //var modifiedTeachers = feature.Stackholders.Except(addedStackholders, new GenericEqualityComparer<Stackholder>());
                //modifiedTeachers.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Modified);

                db.Entry<Feature>(feature).State = System.Data.Entity.EntityState.Modified;
            }
            
            db.SaveChanges();

            if(feature.Id>0)
            {
                return this.GetFirst(feature.Id);
            }

            return feature;
        }

        public Feature GetFirst(int id)
        {
            var feature = db.Featues
                .Where(o => o.Id == id)
                .Include(o=>o.Stackholders)
                .Include(o=>o.Files)
                .Include(o=>o.Tables)
                .FirstOrDefault();
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