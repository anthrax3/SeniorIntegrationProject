using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace MVFA_ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Console.WriteLine("Loading Home page");
            return View();
        }

        public ActionResult Setup()
        {
            return View();
        }

        public ActionResult GenerateDatabase()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MySqlCommand cmd = new MySqlCommand("Generate_ASPNET_Database", new MySqlConnection(constr));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return RedirectToAction("Register", "Account");
        }
    }
}