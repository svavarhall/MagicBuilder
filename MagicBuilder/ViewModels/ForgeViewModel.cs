using MagicBuilder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MagicBuilder.ViewModels
{
    public class ForgeViewModel
    {
        public String UserId { get; set; }

        public String SearchTerm { get; set; }

        public String DeckName { get; set; }

        public int? currentDeckId { get; set; }

        public virtual Deck CurrentDeck { get; set; }

        [DisplayName("Avaliable Decks")]
        public virtual ICollection<Deck> Decks { get; set; }

        [DisplayName("Current Deck")]
        public virtual ICollection<MtgDb.Info.Card> CurrentCards { get; set; }

        [DisplayName("Search results")]
        public virtual ICollection<MtgDb.Info.Card> SearchResults { get; set; }
    }
}