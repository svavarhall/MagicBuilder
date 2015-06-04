using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicBuilder.Models
{
    public class Card
    {
        public Card() { }
        public Card(int cardId, int deckId)
        {
            this.cardId = cardId;
            this.deckId = deckId;
        }
        public int Id { get; set; }
        public int deckId { get; set; }
        public int cardId { get; set; }
    }
}