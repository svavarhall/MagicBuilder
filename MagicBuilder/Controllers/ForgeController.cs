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
using System.Data.Entity;

namespace MagicBuilder.Controllers
{
    public class ForgeController : Controller
    {
        ForgeRepository forgeRepository = new ForgeRepository();
        /// <summary>
        /// Initial view
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var forgeViewModel = new ForgeViewModel();

            forgeViewModel = populateViewModel(forgeViewModel);
            

            //send viewmodel into UI (View)
            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Search for cards
        /// </summary>
        [HttpGet]
        [Authorize]
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
            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Create new deck linked to user
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeck(String deckName)
        {
            var forgeViewModel = new ForgeViewModel();  

            if (ModelState.IsValid)
            {
                forgeRepository.createDeck(deckName, User.Identity.GetUserId());
            }

            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Select a deck to work with
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult SelectDeck(int deckId)
        {
            var forgeViewModel = new ForgeViewModel();

            Session["_currentDeckId"] = deckId; 
            forgeViewModel.CurrentDeck = forgeRepository.getDeckById(forgeViewModel.currentDeckId);

            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Select a deck to work with
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RenameDeck(Deck deck)
        {
            var forgeViewModel = new ForgeViewModel();

            Deck oldDeck = forgeRepository.getDeckById(deck.DeckID);

            if (ModelState.IsValid)
            {
                oldDeck.Name = deck.Name;
                forgeRepository.updateDeck(oldDeck);
            }
            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Delete deck with id
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeck(int? deckId)
        {
            var forgeViewModel = new ForgeViewModel();
            forgeRepository.deleteDeck(deckId);
            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        /// <summary>
        /// Add card to deck
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddCard(int? cardId, int? deckId)
        {
            var forgeViewModel = new ForgeViewModel();

            if (ModelState.IsValid && cardId != null && deckId != null)
            {
                forgeRepository.addCardToDeck((int)cardId, (int)deckId);
            }
            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }


        /// <summary>
        /// Remove a card with cardId from Deck
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCard(int? cardId, int? deckId)
        {
            var forgeViewModel = new ForgeViewModel();

            if (ModelState.IsValid && cardId != null && deckId != null)
            {
                forgeRepository.removeCardFromDeck((int)cardId, (int)deckId);
            }
            forgeViewModel = populateViewModel(forgeViewModel);

            return View("Index", forgeViewModel);
        }

        public ForgeViewModel populateViewModel(ForgeViewModel forgeViewModel)
        {
            
            var userId = User.Identity.GetUserId();

            forgeViewModel.UserId = userId;
            forgeViewModel.Decks = forgeRepository.getUserDecks(userId);
            var id = Session["_currentDeckId"] as int?;
            if (id != null)
            {
                forgeViewModel.CurrentDeck = forgeRepository.getDeckById(id);
                forgeViewModel.CurrentCards = forgeRepository.getCardDetailedInfo(forgeViewModel.CurrentDeck.CardsInDeck);

            }

            return forgeViewModel;
        }
    }
}