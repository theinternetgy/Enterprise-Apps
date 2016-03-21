namespace webapi.Migrations
{
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

            context.Database.ExecuteSqlCommand(@" 

CREATE TABLE [dbo].[Masters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](max) NULL,
	[value] [nvarchar](max) NULL,
	[active] [bit] NULL,
	[mastertype] [nchar](10) NULL,
 CONSTRAINT [PK_Masters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Masters] ON 
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (1, N'Summit', N'', 1, N'project')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (2, N'Incident', NULL, 1, N'module')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (3, N'Service Request', NULL, 1, N'module')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (4, N'IM_Logticket.aspx', NULL, 1, N'page')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (5, N'Customization', NULL, 1, N'worktype')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (6, N'Development', NULL, 1, N'worktype')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (7, N'Bug fix', NULL, 1, N'worktype')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (8, N'Planned', NULL, 1, N'status')
INSERT [dbo].[Masters] ([Id], [text], [value], [active], [mastertype]) VALUES (9, N'Completed', NULL, 1, N'status')
SET IDENTITY_INSERT [dbo].[Masters] OFF
 ");


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