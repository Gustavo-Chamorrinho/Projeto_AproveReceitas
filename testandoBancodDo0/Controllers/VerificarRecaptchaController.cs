using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace testandoBancodDo0 // Substitua pelo nome do seu namespace
{
    [Route("Home/[controller]")]
    [ApiController]
    public class VerificarRecaptchaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TokenRecaptcha token)
        {
            // Chave secreta do reCAPTCHA
            string secretKey = "6LdrooUpAAAAAKUhQGQn1YaHtEPVy6_gmdR8JFBq"; // Substitua pela sua chave secreta

            using (var httpClient = new HttpClient())
            {
                // Enviar uma solicitação POST para o endpoint de verificação do reCAPTCHA
                var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", secretKey),
                    new KeyValuePair<string, string>("response", token.Token)
                }));

                // Verificar a resposta do Google reCAPTCHA
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RecaptchaResponse>(content);

                    if (result.Success)
                    {
                        // Token reCAPTCHA válido, faça o que for necessário aqui
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

