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
            
            string secretKey = "6LdrooUpAAAAAKUhQGQn1YaHtEPVy6_gmdR8JFBq"; // chave secretea do site

            using (var httpClient = new HttpClient())
            {
                // Enviar uma solicitação POST para o endpoint de verificação do reCAPTCHA
                var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", secretKey),
                    new KeyValuePair<string, string>("response", token.Token)
                }));

                // Verifica a resposta do Google reCAPTCHA
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RecaptchaResponse>(content);

                    if (result.Success)
                    {
                        // Token reCAPTCHA válido
                        return Ok(new { success = true, message = "Token reCAPTCHA válido." });
                    }
                }
            }

            // Token reCAPTCHA inválido
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

