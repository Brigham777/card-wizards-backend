using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemAppBackend.Models
{
    public class Card
    {
        public int cardID { get; set; }
        public int deckID { get; set; }
        public int learningStage {
            get;
            set;
        }
        public DateTime dueDate { get; set; }
        public List<Value> cardValues { get; set; }
    }

    public enum LearningStages
    {
        notPlanted = 0,
        planted = 1,
        sprouted = 2,
        rooted = 3
    }

    public class Value
    {
        public string value { get; set; }
        public string valueTitle { get; set; }
        public string valueType { get; set; }
    }
}