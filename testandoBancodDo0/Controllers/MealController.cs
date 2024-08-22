using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using testandoBancodDo0.Models;

//AREA DE TESTES

namespace testandoBancodDo0.Controllers
{
    public class MealController : Controller
    {
        private readonly HttpClient _httpClient;

        public MealController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Console.WriteLine("MealController instantiated"); 
        }

        public async Task<IActionResult> IndexMeal(string search)
        {
            Console.WriteLine($"Search term: {search}"); 

            if (string.IsNullOrEmpty(search))
            {
                return View(null);
            }

            var url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={search}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var mealResponse = JsonConvert.DeserializeObject<MealResponse>(jsonString);
                return View("~/Views/Site/IndexMeal.cshtml", mealResponse);
            }

            return View(null);
        }
    }

}

