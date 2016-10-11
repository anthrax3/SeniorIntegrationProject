using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using COS492_SIP.Models;

namespace COS492_SIP.DAL
{
    public class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            //Roles
            context.Roles.Add(new Role { role = "admin" });
            context.Roles.Add(new Role { role = "user" });

            //Users
            context.Users.Add(new User { userName = "test", firstName = "Elliot", lastName = "Alderson", email = "ealderson@allsafe.com", phone = "555-555-5555", city = "New York", state = "New York", zipCode = "10001" });

            //User Roles
            context.UserRoles.Add(new UserRole { userName = "test", role = "admin" });

            //Logins
            context.Logins.Add(new Login { userName = "test", password = "test" });

            //Credit Cards
            context.CreditCards.Add(new CreditCard { userName = "test", creditCardNumber = "1234432156788765", expirationDate = new DateTime(2020, 1, 1), svn = "123", brand = "Evil Corp" });
            
            base.Seed(context);
        }
    }
}