using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Resources;
using testandoBancodDo0.Context;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class SolicitacaoReceitaController : Controller
    {
        private readonly AproveDbContext _dbContext;
        private readonly ValidarImagemController _validarImagemController;
        private readonly ResourceManager _resourceManager;

        public SolicitacaoReceitaController(AproveDbContext dbContext, ValidarImagemController validarImagem)
        {
            _dbContext = dbContext;
            _validarImagemController = validarImagem;
            _resourceManager = new ResourceManager("testandoBancodDo0.Resources.ResourceMensagensErroImg", Assembly.GetExecutingAssembly());
        }

        [HttpPost]
        public IActionResult SolicitarReceita([Bind("Titulo,Descricao,Ingredientes,Dificuldade,Custo,TempoPreparo,UnidadeTempo")] ReceitaModel model, IFormFile imagem)
        {
            var usuarioAutenticado = HttpContext.Session.GetString("UserId");
            if (usuarioAutenticado == null)
            {
                TempData["ErrorMessage"] = _resourceManager.GetString("SOLICITAR_NULO");
                return View("/Views/Site/CadastraReceita.cshtml", model);
            }
            try
            {
                var mensagensErro = _validarImagemController.ValidarImagem(imagem);

                if (mensagensErro.Any())
                {
                    TempData["ErrorMessage"] = string.Join("<br/>", mensagensErro);
                    return View("/Views/Site/CadastraReceita.cshtml", model);
                }

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

                    var novaReceita = new ReceitaModel
                    {
                        Titulo = model.Titulo,
                        Descricao = model.Descricao,
                        Ingredientes = model.Ingredientes,
                        IdUsuario = usuarioID,
                        imagem = model.imagem,
                        Dificuldade = model.Dificuldade,
                        Custo = model.Custo,
                        TempoPreparo = model.TempoPreparo,
                        UnidadeTempo = model.UnidadeTempo
                    };

                    _dbContext.receitas.Add(novaReceita);
                    _dbContext.SaveChanges();

                    return RedirectToAction("PrincipalHomeAutenticado", "Site");
                }

                TempData["ErrorMessage"] = _resourceManager.GetString("SOLICITAR_ERRO");
                return View("/Views/Site/CadastraReceita.cshtml", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar: {ex.Message}");
                TempData["ErrorMessage"] = _resourceManager.GetString("SOLICITAR_ERRO");
                return View();
            }
        }
    }
}
