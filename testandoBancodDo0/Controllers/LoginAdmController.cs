using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace testandoBancodDo0.Controllers
{
    public class LoginAdmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string username, string password)
        {
            if (username == "Aprove" && password == "aprove")
            {
                return RedirectToAction("ExibirSenha", "LoginAdm");
            }
            else
            {
                TempData["ErrorMessage"] = "Usuário não encontrado, talvez você não tenha permissão para essa página.";
                return RedirectToAction("TelaAdm", "Site");
            }
        }

        public IActionResult ExibirSenha()
        {
            var senhaExpirou = HttpContext.Session.GetString("senhaExpirou");
            if (string.IsNullOrEmpty(senhaExpirou) || DateTime.Parse(senhaExpirou) < DateTime.Now.AddHours(-24))
            {
                var senha = GerarSenha(8);
                HttpContext.Session.SetString("senhaExpirou", DateTime.Now.ToString());
                HttpContext.Session.SetString("senha", senha);
                ViewBag.Senha = senha;
            }
            else
            {
                ViewBag.Senha = HttpContext.Session.GetString("senha");
            }

            return View("/Views/Site/ExibirSenha.cshtml");
        }

        private string GerarSenha(int tamanho)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[tamanho];
                rng.GetBytes(bytes);
                var charArray = new char[tamanho];
                for (int i = 0; i < tamanho; i++)
                {
                    charArray[i] = caracteres[bytes[i] % caracteres.Length];
                }
                return new string(charArray);
            }
        }
    }
}


