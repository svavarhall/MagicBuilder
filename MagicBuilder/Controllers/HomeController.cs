using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MtgDb.Info.Driver;
using MagicBuilder.Models;

namespace MagicBuilder.Controllers
{
    public class HomeController : Controller
    {
        Db database = new Db();  
        private ApplicationDbContext db = new ApplicationDbContext();

        //MtgDb.Info.CardSet[] sets;
        MtgDb.Info.Card[] cards;

        [AllowAnonymous]
        public ActionResult Index(String searchString)
        {

            //sets = database.GetSets();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                cards = database.Search(searchString.Trim(), 0, 25, false);
            }

            if (cards == null)
            {
                cards = new MtgDb.Info.Card[] { };
            }
            //ViewBag.Sets = sets;
            return View(cards);
        }
    }
}