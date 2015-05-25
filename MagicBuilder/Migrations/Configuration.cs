namespace MagicBuilder.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MagicBuilder.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<MagicBuilder.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MagicBuilder.Models.ApplicationDbContext";
        }

        bool AddUserAndRole(MagicBuilder.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin@gmail.com",
            };
            ir = um.Create(user, "Monkey4Hire.com");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        protected override void Seed(MagicBuilder.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            AddUserAndRole(context);
            context.Decks.AddOrUpdate(p => p.Name,
               new Deck
               {
                   Name = "Favorites",
               }
             );
            var decks = new List<Deck>
            {
            new Deck{Name="Red Rush"}
            };

            decks.ForEach(s => context.Decks.Add(s));
            context.SaveChanges();
        }
    }

}
