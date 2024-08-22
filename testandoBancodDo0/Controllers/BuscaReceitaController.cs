using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Text.Json;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class BuscaReceitaController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ResourceManager _resourceManager;

        public BuscaReceitaController(ResourceManager resourceManager, IWebHostEnvironment env)
        {
            _env = env;
            _resourceManager = resourceManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(string nome)
        {
            string arquivoJson = Path.Combine(_env.WebRootPath, "data", "receitas.json");

            if (System.IO.File.Exists(arquivoJson))
            {
                string ReceitaJson = System.IO.File.ReadAllText(arquivoJson);
                var receitas = JsonSerializer.Deserialize<List<BuscaModel>>(ReceitaJson);

                var resultados = receitas
                    .Where(r => r.Nome.ToLower().Contains(nome.ToLower()))
                    .ToList();
                return View("~/Views/Site/PrincipalHome.cshtml", resultados);
            }
            string? ErroGeral = _resourceManager.GetString("ERRO_GERAL");
            return NotFound(ErroGeral);

        }


        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            string arquivoJson = Path.Combine(_env.WebRootPath, "data", "receitas.json");

            if (System.IO.File.Exists(arquivoJson))
            {
                string ReceitaJson = System.IO.File.ReadAllText(arquivoJson);
                var receitas = JsonSerializer.Deserialize<List<BuscaModel>>(ReceitaJson);

                var receita = receitas.FirstOrDefault(r => r.Id == id);
                if (receita != null)
                {
                    return View("~/Views/Receitas/ReceitaHotDog.cshtml", receita);
                }
            }
            string? ErroGeral = _resourceManager.GetString("ERRO_GERAL");
            return NotFound(ErroGeral);
        }
    }
}

