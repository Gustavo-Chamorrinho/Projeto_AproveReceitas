using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers
{
    public class BuscaReceitaController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public BuscaReceitaController(IWebHostEnvironment env)
        {
            _env = env;
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
            return NotFound("Arquivo Json nao encontrado");

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
            return NotFound("Receita não encontrada");
        }
    }
}

