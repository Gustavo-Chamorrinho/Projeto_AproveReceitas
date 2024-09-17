using Microsoft.AspNetCore.Mvc;
using System.Resources;
using testandoBancodDo0.ApiServices;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class BuscaReceitaController : Controller
    {
        private readonly ReceitasServices _receitasServices;
        private readonly ResourceManager _resourceManager;

        public BuscaReceitaController(ReceitasServices receitasServices, ResourceManager resourceManager)
        {
            _receitasServices = receitasServices;
            _resourceManager = resourceManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string nome)
        {
            var receitas = await _receitasServices.GetReceitasAsync();

            var resultados = receitas
                .Where(r => r.Nome.ToLower().Contains(nome.ToLower()))
                .ToList();

            var usuarioAutenticado = HttpContext.Session.GetString("UserId");
            if (resultados.Any())
            {
                if (usuarioAutenticado == null)
                {
                    return View("~/Views/Site/PrincipalHome.cshtml", resultados);
                }
                return View("~/Views/Site/PrincipalHomeAutenticado.cshtml", resultados);
            }

            string? ErroGeral = _resourceManager.GetString("ERRO_GERAL");
            if (usuarioAutenticado == null)
            {
                return View("~/Views/Site/PrincipalHome.cshtml");
            }
            return View("~/Views/Site/PrincipalHomeAutenticado.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var receitas = await _receitasServices.GetReceitasAsync();

            var receita = receitas.FirstOrDefault(r => r.Id == id);
            var usuarioAutenticado = HttpContext.Session.GetString("UserId");
            if (receita != null && usuarioAutenticado != null)
            {
                return View("~/Views/Receitas/PrincipalHomeAutenticado.cshtml", receita);
            }
            string? ErroGeral = _resourceManager.GetString("ERRO_GERAL");
            return View("~/Views/Site/PrincipalHome.cshtml");
            
        }
    }
}
