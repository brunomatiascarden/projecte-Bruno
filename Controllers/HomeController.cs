using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace RickAndMortyApp.Controllers

{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()

        {
            var apiUrl = "https://rickandmortyapi.com/api/character/?name=rick&status";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var characters = JsonConvert.DeserializeObject<CharacterList>(data);
                return View(characters.Results);
            }


            return View(new List<Character>());
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var apiUrl = $"https://rickandmortyapi.com/api/character?name={searchTerm}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var characters = JsonConvert.DeserializeObject<CharacterList>(data);
                return View("Index", characters.Results);
            }

            return View("Index", new List<Character>());
        }
    }
}




