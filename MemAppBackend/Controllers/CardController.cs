using MemAppBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MemAppBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CardController : ApiController
    {
        private static string MemAppDB;


        public CardController()
        {
            MemAppDB = ConfigurationManager.ConnectionStrings["MemAppDB"].ConnectionString;
        }

        static void Main(string[] args)
        {
            new DeckController();
            Console.ReadLine();
        }

        [HttpGet]
        [Route("api/Card/{deckID}")]
        public List<Card> Get(int deckID)
        {
            List<Card> result = new List<Card>();

            using (MySqlConnection connection = new MySqlConnection(MemAppDB))
            {
                connection.Open();
                try
                {
                    MySqlCommand getCards = new MySqlCommand("SELECT  card.CardID, card.DeckID, card.LearningStage," +
                        "card.DueDate, cardvalues.Value, cardvalues.ValueTitle, cardvalues.ValueType " +
                        "FROM card INNER " +
                        "JOIN cardvalues " +
                        "ON card.CardID = cardvalues.CardID " +
                        "WHERE card.DeckID = @DeckID " +
                        "ORDER BY card.CardID", connection);
                    getCards.Parameters.AddWithValue("@DeckID", deckID);

                    using (MySqlDataReader reader = getCards.ExecuteReader())
                    {
                        Card curCard = new Card();
                        Value curValue = new Value();
                        List<Value> cardItems = new List<Value>();
                        int lastID = -1;
                        while (reader.Read())
                        {
                            if (lastID != (int)reader["CardID"])
                            {
                                if (curCard.cardID != 0)
                                {
                                    curCard.cardValues = cardItems;
                                    result.Add(curCard);

                                    cardItems = new List<Value>();
                                    curCard = new Card();
                                    
                                }

                                curCard.cardID = (int)reader["CardID"];
                                curCard.deckID = (int)reader["DeckID"];
                                curCard.learningStage = (int)reader["LearningStage"];
                                curCard.dueDate = (DateTime)reader["DueDate"];

                            }
                            curValue.value = reader["Value"].ToString();
                            curValue.valueTitle = reader["ValueTitle"].ToString();
                            curValue.valueType = reader["ValueType"].ToString();
                            cardItems.Add(curValue);
                            curValue = new Value();

                            lastID = (int)reader["CardID"];
                            
                        }
                        curCard.cardValues = cardItems;
                        result.Add(curCard);
                    }
                }
                catch
                {
                }
                return result;
            }
        }
    }
}
