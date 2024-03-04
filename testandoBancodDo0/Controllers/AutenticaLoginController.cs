using Microsoft.AspNetCore.Mvc;
using testandoBancodDo0.Models;
using testandoBancodDo0.Data;  
using System;
using System.Linq;
using testandoBancodDo0.Context;
using Microsoft.AspNetCore.Http;

//adicionado recentemente (validação para login no banco)

namespace testandoBancodDo0.Controllers
{
    public class AutenticaLoginController : Controller
    {
        private readonly AproveDbContext _dbContext;

        public AutenticaLoginController(AproveDbContext dbContext)
        {
            _dbContext = dbContext;
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
                    // Verificar as credenciais no banco de dados
                    var usuario = _dbContext.usuarios.FirstOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);

                    if (usuario != null)
                    {
                        //armazenamento de nome do usuario que fizer login
                        HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                        HttpContext.Session.SetString("UserName", usuario.Name);
                       // Console.WriteLine($"O valor de UserId na sessão é: {usuario.Id}"); //teste para verificar o Id

                        string UsuarioLogado = HttpContext.Session.GetString("UserName");
                        ViewBag.UserName = UsuarioLogado;

                        // Se as credenciais forem válidas, redireciona para a página principal
                        return RedirectToAction("Home", "Site");
                    }
                    else
                    {
                        //aqui exibe a mensagem que joga lá para o html na parte do If fazendo um foreach e mostrando os erros.
                        ModelState.AddModelError(string.Empty, "Informações Inválidas. Tente novamente.");
                        ModelState.AddModelError(string.Empty, "Verifique as informações digitadas.");
                        return View("~/Views/Site/Login.cshtml",model);
                    }
                }

                // Se houver erros de validação, retorna a página de login com os erros(talvez,nao necessario esse trecho)
                return View("~/Views/Site/Login.cshtml",model);
            }
            catch (Exception ex)
            {
                // erros para ajudar na depuração
                Console.WriteLine($"Erro ao fazer login: {ex.Message}");
                return View();
            }
        }
    }
}

