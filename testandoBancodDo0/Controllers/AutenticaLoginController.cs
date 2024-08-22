using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Resources;
using testandoBancodDo0.Context;
using testandoBancodDo0.Helper;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class AutenticaLoginController : Controller
    {
        private readonly AproveDbContext _dbContext;
        private readonly ResourceManager _resourceManager;

        public AutenticaLoginController(AproveDbContext dbContext)
        {
            _dbContext = dbContext;
            _resourceManager = new ResourceManager("testandoBancodDo0.Resources.ResourceMensagensErro", Assembly.GetExecutingAssembly());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _dbContext.usuarios.FirstOrDefault(u => u.Email == model.Email && u.Senha == model.Senha.GerarHash());

                    if (usuario != null)
                    {
                        HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                        HttpContext.Session.SetString("UserName", usuario.Name);
                        HttpContext.Session.SetString("Email", usuario.Email);

                        string? UsuarioLogado = HttpContext.Session.GetString("UserName");
                        ViewBag.UserName = UsuarioLogado;

                        return RedirectToAction("PrincipalHome", "Site");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = _resourceManager.GetString("USUARIO_NULO");
                        return View("~/Views/Site/Login.cshtml", model);
                    }
                }

                return View("~/Views/Site/Login.cshtml", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer login: {ex.Message}");
                string? ErroGeral = _resourceManager.GetString("ERRO_GERAL");
                return NotFound(ErroGeral);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Site");
        }
    }
}
