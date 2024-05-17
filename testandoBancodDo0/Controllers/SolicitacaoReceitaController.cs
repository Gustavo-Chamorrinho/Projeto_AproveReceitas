using Microsoft.AspNetCore.Mvc;
using testandoBancodDo0.Context;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class SolicitacaoReceitaController : Controller
    {
        private readonly AproveDbContext _dbContext;

        public SolicitacaoReceitaController(AproveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult SolicitarReceita([Bind("Titulo,Descricao,Ingredientes")] ReceitaModel model, IFormFile imagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioID = HttpContext.Session.GetString("UserId");

                    if (imagem != null && imagem.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            imagem.CopyTo(ms);
                            model.imagem = ms.ToArray();
                        }
                    }
                    // gera titulo,descriçao e ingredientes para jogar no banco de dados
                    var novaReceita = new ReceitaModel
                    {
                        Titulo = model.Titulo,
                        Descricao = model.Descricao,
                        Ingredientes = model.Ingredientes,
                        IdUsuario = usuarioID,
                        imagem = model.imagem

                    };

                    // Adiciona o  usuário ao banco de dados
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
