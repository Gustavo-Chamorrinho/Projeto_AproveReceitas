using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace testandoBancodDo0 
{
    [Route("Home/[controller]")]
    [ApiController]
    public class VerificarRecaptchaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TokenRecaptcha token)
        {

            string? secretKey = Environment.GetEnvironmentVariable("RECAPTCHA_KEY");
            if (string.IsNullOrEmpty(secretKey))
            {
                return StatusCode(500, new { sucess = false, message = "Erro ao tentar carregar a chave." });
            }

            using (var httpClient = new HttpClient())
            {
                
                var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", secretKey),
                    new KeyValuePair<string, string>("response", token.Token)
                }));

                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RecaptchaResponse>(content);

                    if (result.Success)
                    {
                        
                        return Ok(new { success = true, message = "Token reCAPTCHA válido." });
                    }
                }
            }

           
            return BadRequest(new { success = false, message = "Token reCAPTCHA inválido." });
        }
    }

    public class TokenRecaptcha
    {
        public string Token { get; set; }
    }

    public class RecaptchaResponse
    {
        public bool Success { get; set; }
    }
}

