using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MagicBuilder.Models;
using MtgDb.Info.Driver;
using Microsoft.AspNet.Identity;
using MagicBuilder.Repositories;
using MagicBuilder.ViewModels;

namespace MagicBuilder.Controllers
{
    public class ForgeController : Controller
    {
        // Open a connection to Mtgdb.com
        // https://api.mtgdb.info/ for info
        Db mtgDb = new Db();

        // Open a connection to the user database
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Initial view
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var deckRepository = new DeckRepository();
            var forgeViewModel = new ForgeViewModel();
            var userId = User.Identity.GetUserId();

            // Get decks from the database
            List<Deck> decks = db.Decks.Where(t => t.UserId == userId).ToList();

            //assign values for viewmodel
            forgeViewModel.Decks = decks;
            forgeViewModel.UserId = userId;

            //send viewmodel into UI (View)
            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Search for cards
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult Search(ForgeViewModel forgeViewModel)
        {
            MtgDb.Info.Card[] searchResults = new MtgDb.Info.Card[] {};
            if (!String.IsNullOrEmpty(forgeViewModel.SearchString))
            {
                searchResults = mtgDb.Search(forgeViewModel.SearchString.Trim(), 0, 25, false);
            }

            if (searchResults.Length > 0)
            {
                forgeViewModel.SearchResults = searchResults;
            }
            
            return View("Index",forgeViewModel);
        }
    }
}