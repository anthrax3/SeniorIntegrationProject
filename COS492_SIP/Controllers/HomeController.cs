using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COS492_SIP.DAL;

namespace COS492_SIP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Setup()
        {
            return View();
        }

        public ActionResult AddCard()
        {
            return View();
        }

        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public void GenerateDatabase()
        {
            //MyDbContext db = new MyDbContext();
            //Index();
        }
    }
}