using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MagicBuilder.Models;
using MtgDb.Info.Driver;
using Microsoft.AspNet.Identity;

namespace MagicBuilder.Controllers
{
    public class ForgeController : Controller
    {
        // Open a connection to Mtgdb.com
        // https://api.mtgdb.info/ for info
        Db cardData = new Db();

        // GET: Forge
        public ActionResult Index()
        {
            return View();
        }

        // GET: Forge/Search/
        [Authorize]
        public ActionResult Details(FormCollection searchData)
        {
            if (searchData == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MtgDb.Info.Card[] cards = cardData.Search(searchData["searchTerm"]);
            if (cards == null)
            {
                return HttpNotFound();
            }
            return View(cards);
        }
    }
}