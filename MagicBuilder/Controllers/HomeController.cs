using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MtgDb.Info.Driver;
using MagicBuilder.Models;
using MagicBuilder.ViewModels;
using MagicBuilder.Repositories;

namespace MagicBuilder.Controllers
{
    public class HomeController : Controller
    {
        ForgeRepository forgeRepository = new ForgeRepository();

        [AllowAnonymous]
        public ActionResult Index(String searchString)
        {
            var forgeViewModel = new ForgeViewModel();

            return View(forgeViewModel);
        }

        /// <summary>
        /// Search for cards
        /// </summary>
        [HttpGet]
        public ActionResult Search(
            String searchTerm,
            String color,
            String setId,
            String manaCost,
            String rarity)
        {
            var forgeViewModel = new ForgeViewModel();

            forgeViewModel.SearchResults =
                forgeRepository.searchCards(searchTerm, color, setId, manaCost, rarity);

            return View("Index", forgeViewModel);
        }
    }
}