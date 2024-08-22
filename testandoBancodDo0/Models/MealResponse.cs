using Newtonsoft.Json;

//AREA DE TESTES

namespace testandoBancodDo0.Models
{
    public class MealResponse
    {
        [JsonProperty("meals")]
        public List<Meal> Meals { get; set; }
    }

    public class Meal
    {
        [JsonProperty("idMeal")]
        public string IdMeal { get; set; }

        [JsonProperty("strMeal")]
        public string Name { get; set; }

        [JsonProperty("strCategory")]
        public string Category { get; set; }

        [JsonProperty("strArea")]
        public string Area { get; set; }

        [JsonProperty("strInstructions")]
        public string Instructions { get; set; }

        [JsonProperty("strMealThumb")]
        public string Thumbnail { get; set; }
    }
}
