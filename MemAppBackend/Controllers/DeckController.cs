using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web.Http;
using System.Configuration;
using MemAppBackend.Models;
using System.Web.Http.Cors;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web;

namespace MemAppBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DeckController : ApiController
    {
        private static string MemAppDB;


        public DeckController()
        {
            MemAppDB = ConfigurationManager.ConnectionStrings["MemAppDB"].ConnectionString;
        }

        static void Main(string[] args)
        {
            new DeckController();
            Console.ReadLine();
        }

        [HttpGet]
        public List<Deck> GetAll()
        {
            List<Deck> result = new List<Deck>();
            string query = "SELECT * FROM deck";

            using (MySqlConnection connection = new MySqlConnection(MemAppDB))
            {
                connection.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Deck curDeck = new Deck();
                        int i = 0;

                        while (reader.Read())
                        {
                            result.Add(new Deck
                            {
                                id = (int)reader["DeckID"],
                                deckName = (string)reader["Name"],
                                cardsLearning = (int)reader["CardsLearning"],
                                cardsNew = (int)reader["CardsNew"],
                                cardsReview = (int)reader["CardsReview"]

                            });
                        }
                    }
                }
                catch
                {
                }
                return result;
            }
        }

        [HttpPost]
        [Route("api/Deck/AddDeck")]
        public IHttpActionResult AddDeck()
        {
            int i = 0;
            string result = string.Empty;

            HttpResponseMessage response = new HttpResponseMessage();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[i];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + postedFile.FileName);
                    try
                    {
                        postedFile.SaveAs(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    i++;
                }
            }


            result += "<ul>";

            result += "</ul>";

            return Json(result);
        }
    }
}
