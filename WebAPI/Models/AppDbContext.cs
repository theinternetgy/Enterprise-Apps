using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;
using System;
using webapi.Models.Settings;
using webapi.Models.Common;

namespace webapi.Models
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(): base("DefaultConnection")
        {
        }
        
        //public static AppDbContext Create()
        //{
        //    return new AppDbContext();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Feature>()
            //        .HasMany<File>(s => s.Files)
            //        .WithRequired()
            //        .HasForeignKey(s => s.FeatureId);
        }

        public DbSet<Feature> Featues { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Stackholder> Stackholders { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<StoryPoint> StoryPoints { get; set; }
        public DbSet<Settings.MasterItem> Masters { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public static class DbSetExtensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }
    }

}