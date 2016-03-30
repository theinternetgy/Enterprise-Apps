using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Settings
{
    public class SettingsService
    {
        AppDbContext db = null;
        public SettingsService()
        {
            db = new AppDbContext();
        }

        public bool Save(MasterItem masterItem)
        {
            bool result = false;

            if (masterItem.Id == 0)
            {
                db.Masters.Add(masterItem);
            }
            else
            {
                db.Entry(masterItem).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return result;
        }

        public IEnumerable<MasterItem> GetAll(FilterSettings filter)
        {
            IEnumerable<MasterItem> result = db.Masters.Where(o=>o.MasterType == filter.MasterType);
            return result;
        }
    }
}