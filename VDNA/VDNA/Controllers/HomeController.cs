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
using System.Text.RegularExpressions;

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

        public static string GetSecurityLevel()
        {
            string result;
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    result = (from c in context.SecuritySettings
                              select c.SecurityLevel).ToList().First();
                }
                catch(Exception)
                {
                    result = "not found";
                }
            }
            return result;
        }

        public ActionResult UpdateSecurity(string securityLevel)
        {
            using (var context = new ApplicationDbContext())
            {
                context.SecuritySettings.Remove(context.SecuritySettings.First());
                context.SecuritySettings.Add(new SecuritySettings() { SecurityLevel = securityLevel });
                context.SaveChanges();
            }
            UpdateAdminPassword();
            return RedirectToAction("Settings");
        }

        public void UpdateAdminPassword()
        {
            using (var context = new ApplicationDbContext())
            {
                string adminId = (from u in context.Users
                             where u.Email.Equals("admin@test.com")
                             select u.Id).First().ToString();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                if(GetSecurityLevel().Equals("high"))
                {
                    UserManager.RemovePassword(adminId);
                    UserManager.AddPassword(adminId, "q1S2@fXZETG9433!2");
                }
                else
                {
                    UserManager.RemovePassword(adminId);
                    UserManager.AddPassword(adminId, "mockingjay");
                }
                context.SaveChanges();
            }
        }

        public ActionResult GenerateDatabase()
        {
            //Create Empty Database
            var myDbContext = new ApplicationDbContext();
            //myDbContext.Database.Delete();
            //myDbContext.Database.Create();

            //Populate tables with initial data
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(myDbContext));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(myDbContext));

            // Create Admin Role
            RoleManager.Create(new IdentityRole("Admin"));
            RoleManager.Create(new IdentityRole("User"));

            //Setting initial security to low
            myDbContext.SecuritySettings.Add(new SecuritySettings() { SecurityLevel = "low" });

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

        public static List<CreditCard> GetCardsByUser(string userId)
        {
            List<CreditCard> results;
            using (var context = new ApplicationDbContext())
            {
                results = (from c in context.CreditCards
                              where c.UserId.Contains(userId)
                              select c).ToList();
            }
            return results;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XSSDemo(string nameToFind)
        {
            if(GetSecurityLevel().Equals("high"))
            {
                Regex r = new Regex(@"[^a-zA-Z0-9/!'?.-]");
                nameToFind = r.Replace(nameToFind, "");
            }
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
            var userId = User.Identity.GetUserId();
            if(GetSecurityLevel().Equals("high"))
            {
                using (var context = new ApplicationDbContext())
                {
                    CreditCard cardToAdd = new CreditCard();
                    cardToAdd.UserId = userId;
                    cardToAdd.CardNumber = cardNumber;
                    cardToAdd.CVV = CVV;
                    try
                    {
                        cardToAdd.ExpirationDate = parseDate(expirationDate);
                    }
                    catch (Exception)
                    {
                        cardToAdd.ExpirationDate = new DateTime();
                    }
                    context.CreditCards.Add(cardToAdd);
                    context.SaveChanges();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string date = "";
                    try
                    {
                        date = parseDateWeak(expirationDate).ToString();
                    }
                    catch(Exception) { }
                    string sql = "INSERT INTO [dbo].CreditCards([UserId], [CardNumber], [CVV], [ExpirationDate]) VALUES('" + userId + "', '" + cardNumber + "', '" + CVV + "', '" + date + "');";
                    var command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
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

        private int parseDateWeak(string dateToParse)
        {
            string[] splitDate = dateToParse.Split('/');
            string month = splitDate[0];
            string year = splitDate[1];
            
            string convertedDate = year + month + "01";
            return Convert.ToInt32(convertedDate);

        }
    }
}