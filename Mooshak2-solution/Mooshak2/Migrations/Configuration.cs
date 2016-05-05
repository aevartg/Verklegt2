using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mooshak2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mooshak2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mooshak2.Models.ApplicationDbContext context)
        {
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
            //context.Roles.AddOrUpdate(r => r.Name,
            //    new IdentityRole {Name = "Student"},
            //    new IdentityRole {Name = "Teacher"}
            //);
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //string[] roleNames = {"Student", "Teacher"};
            //IdentityResult roleResult;
            //foreach (var roleName in roleNames)
            //{
            //    if (!RoleManager.RoleExists(roleName))
            //    {
            //        roleResult = RoleManager.Create(new IdentityRole(roleName));
            //    }
            //}

        }
    }
}
