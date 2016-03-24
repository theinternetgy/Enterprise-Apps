namespace webapi.Migrations
{
    using Models.Settings;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<webapi.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

         
        protected override void Seed(webapi.Models.AppDbContext context)
        {

            context.Masters.AddOrUpdate(
              p => p.Text,
              new MasterItem { MasterType= "Project", Text = "Summit", Active=true },
              new MasterItem { MasterType= "Module", Text = "Incident", Active = true },
              new MasterItem { MasterType= "Module", Text = "Service Request", Active = true },
              new MasterItem { MasterType= "Module", Text = "Problem", Active = true },
              new MasterItem { MasterType= "Module", Text = "Work Order", Active = true },
              new MasterItem { MasterType= "Module", Text = "Change Management", Active = true },
              new MasterItem { MasterType= "Page", Text = "IM_Logticket.aspx", Active = true },
              new MasterItem { MasterType= "Page", Text = "IM_TicketDetail.aspx", Active = true },
              new MasterItem { MasterType= "Page", Text = "SR_TicketDetail.aspx", Active = true },
              new MasterItem { MasterType= "WorkType", Text = "Customization", Active = true },
              new MasterItem { MasterType= "WorkType", Text = "Development", Active = true },
              new MasterItem { MasterType= "WorkType", Text = "Bug fix", Active = true },
              new MasterItem { MasterType= "Status", Text = "Planned", Active = true },
              new MasterItem { MasterType= "Status", Text = "Completed", Active = true }
            );


            //context.Database.ExecuteSqlCommand(@"               
            //    SET IDENTITY_INSERT [dbo].[masteritems] ON 
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (1, N'Summit', N'', 1, N'project')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (2, N'Incident', NULL, 1, N'module')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (3, N'Service Request', NULL, 1, N'module')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (4, N'IM_Logticket.aspx', NULL, 1, N'page')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (5, N'Customization', NULL, 1, N'worktype')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (6, N'Development', NULL, 1, N'worktype')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (7, N'Bug fix', NULL, 1, N'worktype')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (8, N'Planned', NULL, 1, N'status')
            //    INSERT [dbo].[masteritems] ([Id], [text], [value], [active], [mastertype]) VALUES (9, N'Completed', NULL, 1, N'status')
            //    SET IDENTITY_INSERT [dbo].[masteritems] OFF
            //     ");


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}




//delete from[dbo].[Tables]
//delete from[dbo].[Files]
//delete from[dbo].[Masters]
//delete from[dbo].[Features]





//drop table[dbo].[Tables]
//drop table[dbo].[Files]
//drop table[dbo].[Masters]
//drop table[dbo].[Features]




//select* from features
//select* from Stackholders
//select* from files
//select* from tables
//select* from storedprocedures
//select* from functions
//select* from[dbo].[UnitTestCases]

//select* from[dbo].[RepositoryItems]
//select* from[dbo].[OtherInfoItems]

//select* from logs

//--		delete from stackholders
//--		delete from logs


//--delete from __MigrationHistory



//      if not exists(select* from sysobjects where name= 'Masters' and xtype = 'U') begin
//                 CREATE TABLE[dbo].[Masters](
//	                [Id]
//[int] IDENTITY(1,1) NOT NULL,

//[text] [nvarchar](max) NULL,
//	                [value]
//[nvarchar](max) NULL,
//	                [active]
//[bit]
//NULL,
//	                [mastertype]
//[nchar](10) NULL,
//                 CONSTRAINT[PK_Masters] PRIMARY KEY CLUSTERED
//               (
//                   [Id] ASC

//               )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//                ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
