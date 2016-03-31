using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using webapi.Models.Common;

namespace webapi.Models
{
    public class MasterData
    {
        AppDbContext db;
        public MasterData()
        {
            db = new AppDbContext();
        }

        public IEnumerable GetMasterData(CrudTypes master)
        {
            IEnumerable list = null;
            if (master == CrudTypes.Team)
            {
                list = db.Masters.Where(o=>o.MasterType==master.ToString());
            }
            return list;
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

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                db.Users.Add(user);
            }
            else
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
        }

        public IEnumerable GetUsers()
        {
            return db.Users;
        }

        public void SaveUserTeamMapping(UserTeamMapping mapping)
        {
            if(mapping!=null)
            {
                StringBuilder sql = new StringBuilder();
                for (int i = 0; i < mapping.Teams.Count(); i++)
                {
                    for (int j = 0; j < mapping.Users.Count(); j++)
                    {
                        sql.AppendFormat(" if not exists (select 1 from UserTeamMapping where UserId={0} and TeamId={1}) insert into UserTeamMapping(UserId,TeamId)values({0},{1});",mapping.Users[j], mapping.Teams[i]);
                    }
                }
                db.Database.ExecuteSqlCommand(sql.ToString());
            }
        }

        public IEnumerable GetUserTeamMappingList()
        {
            var mappings = new UserTeamMapping();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"  select b.firstname [User],c.text [Team] from UserTeamMapping a inner join Users b on b.id = a.userid inner join masteritems c on c.id = a.teamid ");

            var a = db.Database.SqlQuery<UserTeamMappingString>(sql.ToString()).ToList();
 
            return a;
        }

        public UserTeamMapping GetUserTeamMapping()
        {
            var mappings = new UserTeamMapping();

            StringBuilder sql = new StringBuilder();
            sql.Append(@" declare @userid varchar(max)='',@teamid varchar(max)=''; select @userid = @userid+cast(UserId as varchar)+',', @teamid = @teamid+cast(TeamId as varchar)+',' from UserTeamMapping; 
                 if @userid is not null
                 set @userid = SUBSTRING(@userid,1,len(@userid)-1)
                 if @teamid is not null
                 set @teamid = SUBSTRING(@teamid,1,len(@teamid)-1)
                select @userid [Users], @teamid [Teams] ");

            var a=db.Database.SqlQuery<UserTeamMappingString>(sql.ToString()).FirstOrDefault();

            if (a != null)
            {
                mappings.Users = a.User.Split(',').Select(x => int.Parse(x)).ToList();
                mappings.Teams = a.Team.Split(',').Select(x => int.Parse(x)).ToList();
            }
            return mappings;
        }
    }
}