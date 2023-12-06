using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Cadastrar([Bind("Name,Email,Senha")] UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // gera nome,email e senha para jogar no banco de dados
                    var novoUsuario = new UsuarioModel
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Senha = model.Senha
                        
                    };

                    // Adicione o novo usuário ao banco de dados
                    _dbContext.usuarios.Add(novoUsuario);

                    // Salva as alterações no banco de dados
                    _dbContext.SaveChanges();

                    // Redireciona para a página login apos cadastrar
                    return RedirectToAction("Login", "Site");
                }

                // Se houver erros de validação, retorna a página de cadastro com os erros
                Console.WriteLine("Deu erro aqui camarada"); //ajustar essa mensagem de erro.
                return View("/Views/Site/Cadastro.cshtml", model);
            }
            catch (Exception ex)
            {
                // erros para ajudar na depuração
                Console.WriteLine($"Erro ao cadastrar: {ex.Message}");
                return View();
            }
        }


        [HttpPost]
        public IActionResult SolicitarReceita([Bind("Titulo,Descricao,Ingredientes")] ReceitaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // gera nome,email e senha para jogar no banco de dados
                    var novaReceita = new ReceitaModel
                    {
                        Titulo = model.Titulo,
                        Descricao= model.Descricao,
                        Ingredientes = model.Ingredientes

                    };

                    // Adicione o novo usuário ao banco de dados
                    _dbContext.receitas.Add(novaReceita);

                    // Salva as alterações no banco de dados
                    _dbContext.SaveChanges();


                    // Se as credenciais forem válidas, redireciona para a página principal
                    return RedirectToAction("Home", "Site");
                }

                // Se houver erros de validação, retorna a página de cadastro com os erros
                Console.WriteLine("Deu erro aqui camarada"); //ajustar essa mensagem de erro.
                return View("/Views/Site/CadastrarReceita.cshtml", model);
            }
            catch (Exception ex)
            {
                // erros para ajudar na depuração
                Console.WriteLine($"Erro ao cadastrar: {ex.Message}");
                return View();
            }
        }

    }
}



