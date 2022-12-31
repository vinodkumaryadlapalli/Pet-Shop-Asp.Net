using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PetShop.Models
{
    public class PetShopDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUser user1 = new ApplicationUser
            {
                UserName = "vinod96@gmail.com",
                Email = "vinod96@gmail.com",
                DateOfBirth = new DateTime(2001, 2, 1)
            };
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            IdentityRole role1 = new IdentityRole { Name = "Admin" };
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(role1);
            userManager.Create(user1, "assignment3");
            userManager.AddToRole(user1.Id, "Admin");

            Pet p1 = new Pet()
            {
                Id = 101,
                Name = "Cat",
                isMale = false,
                Breed = "Cymric",
                Owner = user1
            };
            context.Pets.Add(p1);
            user1.Pets.Add(p1);

            context.Pets.Add(new Pet
            {
                Id = 102,
                Name = "Snake",
                isMale = false,
                Breed = "Ball Python"
            });

            context.Pets.Add(new Pet
            {
                Id = 103,
                Name = "Snake",
                isMale = true,
                Breed = "Borneo Python"
            });

            base.Seed(context);
        }

    }
}