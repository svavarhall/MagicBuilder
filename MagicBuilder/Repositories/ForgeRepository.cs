using MagicBuilder.Models;
using MagicBuilder.ViewModels;
using Microsoft.AspNet.Identity;
using MtgDb.Info.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicBuilder.Repositories
{
    public class ForgeRepository
    {
        // Open a connection to Mtgdb.com
        // https://api.mtgdb.info/ for info
        Db mtgDb = new Db();

        // Open a connection to the user database
        private ApplicationDbContext database = new ApplicationDbContext();

        // Get decks from the database
        public List<Deck> getUserDecks(String userId) 
        {
            List<Deck> decks = database.Decks.Where(t => t.UserId == userId).ToList();
            return decks;
        }

        // Get specific deck by Id from the database
        public Deck getDeckById(int? deckId)
        {
            Deck deck = null;
            if (deckId != null)
            {
                deck = database.Decks.Find(deckId);
                List<Card> cards = database.Cards.Where(t => t.deckId == deckId).ToList();
                if(cards != null)
                {
                    deck.CardsInDeck = cards;
                }
            }
            
            return deck;
        }

        // Create a new deck
        public void createDeck(String deckName, String userId)
        {
            Deck newDeck = new Deck();
            newDeck.Name = deckName;
            newDeck.UserId = userId;
            bool exists = database.Decks.Any(deck =>
                deck.Name == deckName && deck.UserId == userId);
            if(!exists)
            {
                database.Decks.Add(newDeck);
                database.SaveChanges();
            }
            
        }

        // Update Deck
        public void updateDeck(Deck deck)
        {
            database.Entry(deck).State = EntityState.Modified;
            database.SaveChanges();
        }

        // Delete Deck
        public void deleteDeck(int? deckId)
        {
            if (deckId != null)
            {
                Deck deck = database.Decks.Find(deckId);
                if(deck != null)
                {
                    database.Decks.Remove(deck);
                }

                database.SaveChanges();
            }
        }

        // Get card results from MtgDb
        public MtgDb.Info.Card[] searchCards(String searchString, String color, String setId, String manaCost, String rarity)
        {
            MtgDb.Info.Card[] searchResults = new MtgDb.Info.Card[] { };
            String searchQuery = "";

            if (!String.IsNullOrEmpty(searchString))
            {
                searchQuery = searchQuery + "name m '" + searchString.Trim() + "' ";
            }

            if (!String.IsNullOrEmpty(color))
            {
                if(!String.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = String.Concat(searchQuery, " and ");
                }
                searchQuery = searchQuery + "color eq " + color + " ";
            }

            if (!String.IsNullOrEmpty(setId))
            {
                if (!String.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = String.Concat(searchQuery, " and ");
                }
                searchQuery = searchQuery + "setId eq " + setId + " ";
            }

            if (!String.IsNullOrEmpty(manaCost))
            {
                if (!String.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = String.Concat(searchQuery, " and ");
                }
                searchQuery = searchQuery + "convertedmanacost eq " + manaCost + " ";
            }

            if (!String.IsNullOrEmpty(rarity))
            {
                if (!String.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = String.Concat(searchQuery, " and ");
                }
                searchQuery = searchQuery + "rarity eq '" + rarity + "' ";
            }

            if(!String.IsNullOrEmpty(searchQuery)) 
            {
                searchResults = mtgDb.Search(searchQuery, 0, 100, true);
            }

            return searchResults;
        }

        // Get a list of cards from MtgDb.info
        public MtgDb.Info.Card[] getCardDetailedInfo(ICollection<Card> cardsSimple)
        {
            MtgDb.Info.Card[] cards = new MtgDb.Info.Card[] { };
            var idList = new List<int>();

            if (cardsSimple != null)
            {
                foreach(var card in cardsSimple)
                {
                    idList.Add(card.cardId);
                }

                if (idList.Count > 0)
                {
                    cards = mtgDb.GetCards(idList.ToArray());
                }
            }
            return cards;
        }

        public MtgDb.Info.Card getCardById(int cardId)
        {
            MtgDb.Info.Card card = mtgDb.GetCard(cardId);
            return card;
        }

        public void addCardToDeck(int cardId, int deckId)
        {
            if(deckId > 0)
            {
                Card card = new Card(cardId, deckId);
                database.Cards.Add(card);
                database.SaveChanges();
            }

        }

        public void removeCardFromDeck(int cardId, int deckId)
        {
            if (deckId > 0 )
            {
                Card card = database.Cards.Where(c => c.cardId == cardId && c.deckId == deckId).FirstOrDefault();
                database.Cards.Remove(card);
                database.SaveChanges();
            }

        }
    }
}