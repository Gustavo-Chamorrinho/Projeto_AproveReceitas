using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Resources;
using testandoBancodDo0.Context;
using testandoBancodDo0.Controllers.Validacoes;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AproveDbContext _dbContext;
        private readonly ResourceManager _resourceManager;

        public CadastroController(AproveDbContext dbContext)
        {
            _dbContext = dbContext;
            _resourceManager = new ResourceManager("testandoBancodDo0.Resources.ResourceMensagensErro", Assembly.GetExecutingAssembly());
        }

        [HttpPost]
        public IActionResult Cadastrar([Bind("Name,Email,Senha,ConfirmPassword")] UsuarioModel model, string confirmPassword)
        {
            try
            {
                var valida = new ValidacaoCadastroUsu(_resourceManager, _dbContext);
                var errorMessages = valida.Validar(model, confirmPassword);

                if (errorMessages.Any())
                {
                    TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
                    return View("/Views/Site/Cadastro.cshtml", model);
                }

                if (ModelState.IsValid)
                {
                    var novoUsuario = new UsuarioModel
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Senha = model.Senha
                    };

                    novoUsuario.SetSenhaHash();

                    _dbContext.usuarios.Add(novoUsuario);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Login", "Site");
                }

                return View("/Views/Site/Cadastro.cshtml", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar: {ex.Message}");
                return View();
            }
        }
    }
}
