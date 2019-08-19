using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemAppBackend.Models
{
    public class Deck
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        public string deckName { get; set; }
        public int cardsLearning { get; set; }
        public int cardsNew { get; set; }
        public int cardsReview { get; set; }
    }
}