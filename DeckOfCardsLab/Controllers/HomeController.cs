using DeckOfCardsLab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeckOfCardsLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory newHttpClientFactory)
        {
            _httpClientFactory = newHttpClientFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DisplayDeckOfCards()
        {
            var httpClient = _httpClientFactory.CreateClient(); // created http client from the dependency injected factory
            const string createDeckOfCardsApiUrl = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1"; // var for API URL
            var deckOfCardsCreateModel = httpClient.GetFromJsonAsync<DeckOfCards_Create>(createDeckOfCardsApiUrl).GetAwaiter().GetResult(); // this is the action to combining http client and API URL 
            string drawDeckOfCardsApiFormat = $"https://deckofcardsapi.com/api/deck/{deckOfCardsCreateModel.deck_id}/draw/?count=5";  // this line is like line 31
            var drawDeckOfCardsModel = httpClient.GetFromJsonAsync<DeckOfCards_Draw>(drawDeckOfCardsApiFormat).GetAwaiter().GetResult(); // this is the action to draw 2 cards from the created deck
            return View("DeckOfCards", drawDeckOfCardsModel);
            //https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1
            //{
            //   "success": true,
            // "deck_id": "3p40paa87x90",
            // "shuffled": true,
            // "remaining": 52
            //}
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DisplayReddit()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            const string redditApiUrl = "https://www.reddit.com/r/aww/.json";
            var redditSimpleResponseModel = httpClient.GetFromJsonAsync<RedditSimpleResponse>(redditApiUrl).GetAwaiter().GetResult();
            return View("DisplayReddit", redditSimpleResponseModel);
        }
    }

    // LAB 2:  REDDIT
    public class RedditSimpleResponse
    {
        public string kind { get; set; }
        public RedditSimpleResponse_Data data { get; set; }
    }
    public class RedditSimpleResponse_Data
    {
        public string after { get; set; }
        public RedditSimpleResponse_Data_Child[] children { get; set; }
    }
    public class RedditSimpleResponse_Data_Child
    {
        public string kind { get; set; }
        public RedditSimpleResponse_Data_Child_Data data { get; set; }
    }
    public class RedditSimpleResponse_Data_Child_Data
    {
        public string title { get; set; }
        public string url { get; set; }
        public RedditSimpleResponse_Data_Child_Data_LinkFlairRichText[] link_flair_richtext { get; set; }
    }
    public class RedditSimpleResponse_Data_Child_Data_LinkFlairRichText
    {
        public string a { get; set; }
        public string e { get; set; }
        public string u { get; set; }
    }
}