using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webapi.Models;
using webapi.Models.Common;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace webapi.Models
{
    public class FeaturesService: CrudOperation
    {
        AppDbContext db;

        public FeaturesService()
        {
            db = new AppDbContext();
        }

        public Feature Save(Feature feature)
        {
              
            #region [-- Create New --]


            if (feature.Id == 0)
            {
                feature.GUID = Guid.NewGuid();
                feature.Created = Helper.now;
                feature.Updated = Helper.now;

                db.Featues.Add(feature);
            }
            #endregion
            
            #region [-- Edit Existing --]

            else
            {
                feature.Updated = Helper.now;

                var existingFeature = db.Featues.AsNoTracking().Where(o=>o.Id == feature.Id)
                                            .Include(s => s.Stackholders)
                                            .Include(s => s.Files)
                                            .Include(s => s.Tables)
                                            .Include(s => s.StoredProcedures)
                                            .Include(s => s.Functions)
                                            .Include(s => s.UnitTestCases)
                                            .Include(s => s.RepositoyItems)
                                            .Include(s => s.OtherInfoItems)
                                            .Include(s => s.Logs)
                                            .Include(s => s.StoryPoints)
                                            .FirstOrDefault<Feature>();

                #region [-- Logs --]

                IEnumerable<Log> addedLogs = null;
                IEnumerable<Log> modifiedLogs = null;
                

                if (feature.Logs != null)
                {
                    addedLogs = feature.Logs.Except(existingFeature.Logs, tchr => tchr.Id);
                    //addedLogs.ToList().ForEach(log => log.Date = now);
                    addedLogs.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Added);

                    modifiedLogs = feature.Logs.Except(addedLogs, tchr => tchr.Id);
                    modifiedLogs.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Modified);
                }

                #endregion

                #region [-- Stackholders --]

                if (feature.Stackholders != null)
                {
                    var repo = new FeaturesRepository<Stackholder>(db, CrudTypes.Stackholders, feature.Id);
                    repo.AddOrUpdate(existingFeature.Stackholders, feature.Stackholders);

                    //var addedStackholders = feature.Stackholders.Except(existingFeature.Stackholders, tchr => tchr.Id);
                    //addedStackholders.ToList().ForEach(stk => {db.Entry(stk).State = EntityState.Added;});

                    //var modifiedStackholders = feature.Stackholders.Except(addedStackholders, tchr => tchr.Id);
                    //modifiedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Modified);
                }

                #endregion

                //Files

                if (feature.Files != null)
                {
                    var repo = new FeaturesRepository<File>(db, CrudTypes.Files, feature.Id);
                    repo.AddOrUpdate(existingFeature.Files, feature.Files);
                }

                //Tables

                if (feature.Tables != null)
                {
                    var repo = new FeaturesRepository<Table>(db, CrudTypes.Tables, feature.Id);
                    repo.AddOrUpdate(existingFeature.Tables, feature.Tables);
                }

                //StoredProcedures

                if (feature.StoredProcedures != null)
                {
                    var repo = new FeaturesRepository<StoredProcedure>(db, CrudTypes.StoredProcedures, feature.Id);
                    repo.AddOrUpdate(existingFeature.StoredProcedures, feature.StoredProcedures);                    
                }

                //Functions

                if (feature.Functions != null)
                {
                    var repo = new FeaturesRepository<Function>(db, CrudTypes.Functions, feature.Id);
                    repo.AddOrUpdate(existingFeature.Functions, feature.Functions);                    
                }

                //UnitTestCases

                if (feature.UnitTestCases != null)
                {
                    var repo = new FeaturesRepository<UnitTestCase>(db, CrudTypes.UnitTestcases, feature.Id);
                    repo.AddOrUpdate(existingFeature.UnitTestCases, feature.UnitTestCases);                    
                }

                //RepositoryInfoItems

                if (feature.RepositoyItems != null)
                {
                    var repo = new FeaturesRepository<RepositoryItem>(db, CrudTypes.RepositoryInfo, feature.Id);
                    repo.AddOrUpdate(existingFeature.RepositoyItems, feature.RepositoyItems);                    
                }

                //OtherInfoItems

                if (feature.OtherInfoItems != null)
                {
                    var repo = new FeaturesRepository<OtherInfoItem>(db, CrudTypes.OtherInfo, feature.Id);
                    repo.AddOrUpdate(existingFeature.OtherInfoItems, feature.OtherInfoItems);                    
                }

                //StoryPoints

                if (feature.StoryPoints != null)
                {
                    var repo = new FeaturesRepository<StoryPoint>(db, CrudTypes.StoryPoint, feature.Id);
                    repo.AddOrUpdate(existingFeature.StoryPoints, feature.StoryPoints);                    
                }


                #region unused

                //Find newly added Stackholders by updatedStackholders (teacher came from client sided) Minus existingStackholders = newly added Stackholders
                //var addedStackholders = feature.Stackholders.Except<Stackholder>(existingFeature.Stackholders, new GenericEqualityComparer<Stackholder>());
                //addedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Added);

                ////3- Find deleted Stackholders by existing Stackholders - updatedStackholders = deleted Stackholders
                //var deletedStackholders = existingFeature.Stackholders.Except(feature.Stackholders, new GenericEqualityComparer<Stackholder>());
                //deletedStackholders.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Deleted);

                //4- Find modified Stackholders by updatedStackholders - addedStackholders = modified Stackholders
                //var modifiedTeachers = feature.Stackholders.Except(addedStackholders, new GenericEqualityComparer<Stackholder>());
                //modifiedTeachers.ToList().ForEach(stk => db.Entry(stk).State = EntityState.Modified);

                #endregion

                db.Entry<Feature>(feature).State = System.Data.Entity.EntityState.Modified;
            }

            #endregion

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
                .Include(o=>o.StoredProcedures)
                .Include(o=>o.Functions)
                .Include(o=>o.UnitTestCases)
                .Include(o=>o.RepositoyItems)
                .Include(o=>o.OtherInfoItems)
                .Include(o=>o.Logs)
                .Include(o=>o.StoryPoints)
                .FirstOrDefault();
            return feature;
        }

        public IEnumerable<FeatureListItem> GetTopFeatures()
        {
            var result = db.Database.SqlQuery<FeatureListItem>(@" SELECT top 10 a.[Id],a.[GUID],a.[Project],a.[Module],mod.text [ModuleName],a.[Page],page.text [PageName],a.[Parent],a.[Type],a.[Status],status.text [StatusName],a.[Title],a.[Description],a.[Keywords],a.[Created],a.[Updated] 
                                    FROM [dbo].[Features] a 
                                    Left outer join [masteritems] status on status.Id=a.[Status] 
                                    Left outer join [masteritems] mod on mod.Id=a.[Module] 
                                    Left outer join [masteritems] page on page.Id=a.[Page] 
            ");
            return result;
        }

        public IEnumerable<FeatureListItem> GetAll(FilterProperties filter)
        {

            //var result = db.Featues
            //    .Where(o => (filter.Module > 0 && o.Module == filter.Module || filter.Module==0)  )
            //    .Include(o=>o.Status)
            //    .Select(a => new FeatureListItem {
            //        Id= a.Id,Title = a.Title, Description = a.Description
            //    })
            //    .AsEnumerable();

            var sql = new StringBuilder();
            var where = new StringBuilder();
            sql.Append(@" SELECT top 100 a.[Id],a.[GUID],a.[Project],a.[Module],mod.text [ModuleName],a.[Page],page.text [PageName],a.[Parent],a.[Type],a.[Status],status.text [StatusName],a.[Title],a.[Description],a.[Keywords],a.[Created],a.[Updated] 
                                    FROM [dbo].[Features] a 
                                    Left outer join [masteritems] status on status.Id=a.[Status] 
                                    Left outer join [masteritems] mod on mod.Id=a.[Module] 
                                    Left outer join [masteritems] page on page.Id=a.[Page] 
            ");

            if (filter.Module > 0)
                where.AppendFormat(" a.Module={0} ",filter.Module);

            if (filter.Page > 0)
            {
                if (where.Length > 0)
                    where.Append(" and ");

                where.AppendFormat(" a.Page={0} ", filter.Page);
            }

            if (filter.Status>0)
            {
                if (where.Length > 0)
                    where.Append(" and ");

                where.AppendFormat(" a.Status={0} ", filter.Status);
            }

            if (filter.Type > 0)
            {
                if (where.Length > 0)
                    where.Append(" and ");

                where.AppendFormat(" a.Type={0} ", filter.Type);
            }

            if (where.Length > 0)
                sql.Append(" where "+where.ToString());

            var result = db.Database.SqlQuery<FeatureListItem>(sql.ToString());
            return result;
        }
    }


    public class FeaturesRepository<T> where T : class,IBaseCrud
    {

        AppDbContext db = null;
        CrudTypes crudType;
        int primaryId = 0;
        public FeaturesRepository(CrudTypes _type, int _primaryId)
        {
            db = new AppDbContext();
            this.crudType = _type;
            this.primaryId = _primaryId;
        }
        public FeaturesRepository(AppDbContext _db, CrudTypes _type, int _primaryId)
        {
            this.db = _db;
            this.crudType = _type;
            this.primaryId = _primaryId;
        }

        /// <summary>
        /// To add or update the records
        /// </summary>
        /// <param name="ItemsFromDb">existing list of items in db</param>
        /// <param name="ItemsUpdated">added or modified list of items from front end</param>
        public void AddOrUpdate(IEnumerable<T> ItemsFromDb,IEnumerable<T> ItemsUpdated)
        {

            var addedItems = ItemsUpdated.Except(ItemsFromDb, tchr => tchr.Id);
            addedItems.ToList().ForEach(stk => {
                db.Entry(stk).State = EntityState.Added;
            });


            var modifiedItems = ItemsUpdated.Except(addedItems, tchr => tchr.Id);
            if (modifiedItems != null && modifiedItems.Count() > 0)
            {
                var log = new StringBuilder();
                bool ismodified = false;
                 
                modifiedItems.ToList().ForEach(stk =>
                {
                    var entry = db.Entry(stk);
                    entry.State = EntityState.Modified;
                    foreach (var prop in entry.OriginalValues.PropertyNames)
                    {
                        var original = entry.GetDatabaseValues().GetValue<object>(prop);
                        var current = entry.CurrentValues.GetValue<object>(prop);
                        if (!object.Equals(original, current))
                        {
                            entry.Property(prop).IsModified = true;
                            ismodified = true;
                            //Debug.WriteLine("original:" + original, ", current:" + current);
                            log.AppendFormat("<span class=\"text-muted\"><span class=\"text-primary\">{0}</span> : {1} {2}</span>", prop, current, (original != null && original.ToString().Length > 0 ? "(" + original + ")" : "")).AppendLine();
                        }
                    }                    
                });
                if(ismodified)
                db.Logs.Add(new Log { Info = string.Format(" <label class=\"text-info\">{0} Modified </label><br />{1}", this.crudType, log.ToString()), Active = true, Date = Helper.now, FeatureId = this.primaryId });
            }
        }
    }

}