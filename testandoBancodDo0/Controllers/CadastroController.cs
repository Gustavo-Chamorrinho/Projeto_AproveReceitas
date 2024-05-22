using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using testandoBancodDo0.Context;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AproveDbContext _dbContext;

        public CadastroController(AproveDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public IActionResult Cadastrar([Bind("Name,Email,Senha")] UsuarioModel model, string confirmPassword)
        {
            try
            {
                // Verificar se as senhas coincidem
                if (model.Senha != confirmPassword)
                {
                    TempData["ErrorMessage"] = "As senhas não são parecidas, tente novamente.";
                    return View("/Views/Site/Cadastro.cshtml", model);
                }

                //senha com 6 caracteres
                if (model.Senha.Length < 6)
                {
                    TempData["ErrorMessage"] = "As senhas precisam conter pelo menos 6 caracteres";
                    return View("/Views/Site/Cadastro.cshtml", model);
                }

                //Verifica se ja existe um email parecido
                var UsuarioExistente = _dbContext.usuarios.FirstOrDefault(u => u.Email == model.Email);
                if (UsuarioExistente != null)
                {
                    TempData["ErrorMessage"] = "O E-mail digitado já esta sendo utilizado por outro usuário.";
                    return View("/Views/Site/Cadastro.cshtml", model);
                }

                //verifica se contem caracter especial
                if (!CaracterEspecial(model.Senha))
                {
                    TempData["ErrorMessage"] = "A senha precisa conter um caracterer especial EX:'@'";
                    return View("/Views/Site/Cadastro.cshtml", model);
                }


                if (ModelState.IsValid)
                {
                    // gera nome, email e senha para jogar no BD
                    var novoUsuario = new UsuarioModel
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Senha = model.Senha
                    };

                    novoUsuario.SetSenhaHash();

                    // Adiciona ao BD
                    _dbContext.usuarios.Add(novoUsuario);

                    // Salva as alteração no BD
                    _dbContext.SaveChanges();

                    // Redireciona para Login
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

        //funçao para caracteres especiais
        public bool CaracterEspecial(string senha)
        {
            char[] caracteresEspeciais = { '@', '#', '$', '%', '&', '*', '(', ')', '[', ']', '{', '}', '!', '?' };
            return senha.Any(c => caracteresEspeciais.Contains(c));
        }

    }
}



