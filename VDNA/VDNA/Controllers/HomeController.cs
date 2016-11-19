using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VDNA.Models;

namespace VDNA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instructions()
        {
            return View();
        }

        public ActionResult ResetDatabase()
        {
            return View();
        }

        public ActionResult XSSAttack()
        {
            return View();
        }

        public ActionResult SQLInject()
        {
            return View();
        }

        public ActionResult BruteForce()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult GenerateDatabase()
        {
            //Create Empty Database
            var myDbContext = new ApplicationDbContext();
            myDbContext.Database.Delete();
            myDbContext.Database.Create();

            //Populate tables with initial data
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(myDbContext));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(myDbContext));

            // Create Admin Role
            RoleManager.Create(new IdentityRole("Admin"));
            RoleManager.Create(new IdentityRole("User"));

            //Creating Admin Account
            PasswordHasher hasher = new PasswordHasher();
            ApplicationUser adminUser = new ApplicationUser();
            adminUser.Email = "admin@test.com";
            adminUser.EmailConfirmed = true;
            adminUser.PasswordHash = hasher.HashPassword("mockingjay");
            adminUser.UserName = "admin@test.com";
            UserManager.Create(adminUser);
            //Giving it admin privliges
            var currentUser = UserManager.FindByName(adminUser.UserName);
            UserManager.AddToRole(currentUser.Id, "Admin");
            return RedirectToAction("Register", "Account");
        }

        public static List<CreditCard> GetCardsByUser(string user)
        {
            List<CreditCard> results;
            using (var context = new ApplicationDbContext())
            {
                results = (from c in context.CreditCards
                              where c.UserName.Contains(user)
                              select c).ToList();
            }
            return results;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XSSDemo(string nameToFind)
        {
            TempData["XSSName"] = nameToFind;
            using (var context = new ApplicationDbContext())
            {
                var results = from u in context.Users
                              where u.Email.Contains(nameToFind)
                              select u;
                var resultsArr = results.ToArray();
                List<string> namesFound = new List<string>();
                foreach (var r in resultsArr)
                {
                    namesFound.Add(r.UserName);
                }
                TempData["XSSSearch"] = namesFound.ToArray();
            }
            return RedirectToAction("XSSAttack", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SQLIDemo(string cardNumber, string CVV, string expirationDate)
        {
            var username = User.Identity.GetUserName();
            using (var context = new ApplicationDbContext())
            {
                CreditCard cardToAdd = new CreditCard();
                cardToAdd.UserName = username;
                cardToAdd.CardNumber = cardNumber;
                cardToAdd.CVV = CVV;
                cardToAdd.ExpirationDate = parseDate(expirationDate);
                context.CreditCards.Add(cardToAdd);
                context.SaveChanges();
            }
            return RedirectToAction("SQLInject", "Home");
        }

        private DateTime parseDate(string dateToParse)
        {
            string[] splitDate = dateToParse.Split('/');
            int month = Convert.ToInt32(splitDate[0]);
            int year = Convert.ToInt32(splitDate[1]);
            return new DateTime(year, month, 1);
        }
    }
}