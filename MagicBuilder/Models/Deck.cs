using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MagicBuilder.Models
{
    public class Deck
    {
        public int DeckID { get; set; }
        public String UserId { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}