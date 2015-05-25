using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MtgDb.Info.Driver;

namespace MagicBuilder.Controllers
{
    public class HomeController : Controller
    {
        Db database = new Db();

        [AllowAnonymous]
        public ActionResult Index()
        {
            MtgDb.Info.Card card = database.GetRandomCard();
            return View(card);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}