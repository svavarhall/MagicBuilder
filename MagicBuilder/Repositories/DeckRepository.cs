using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicBuilder.Repositories
{
    public class DeckRepository
    {
        public int Id { get; set; }
        public int DeckName { get; set; }
        public virtual ICollection<MtgDb.Info.Card> Cards { get; set; }
    }
}