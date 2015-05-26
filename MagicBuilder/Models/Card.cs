using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MagicBuilder.Models
{
    public class Card
    {
        public int CardID { get; set; }
        public String MultiverseId { get; set; }
    }
}