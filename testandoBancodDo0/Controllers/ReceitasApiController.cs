using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using testandoBancodDo0.ApiServices;

namespace testandoBancodDo0.Controllers
{
    public class ReceitasApiController : Controller
    {
        private readonly ReceitasServices _receitasServices;

        public ReceitasApiController(ReceitasServices receitasServices)
        {
            _receitasServices = receitasServices;
        }
        public async Task<IActionResult> IndexReceitas()
        {
            var receitas = await _receitasServices.GetReceitasAsync();
            var usuarioAutenticado = HttpContext.Session.GetString("UserId");
            if (usuarioAutenticado == null)
            {
                return View("~/Views/Site/PrincipalHome.cshtml", receitas);
            }
            return View("~/Views/Site/PrincipalHomeAutenticado.cshtml", receitas);
            
        }

        public async Task<IActionResult> Details (int id)
        {
            var receita = await _receitasServices.GetReceitasByIdAsync(id);
            if (receita is null) return null!;
            return View("~/Views/Receitas/DetalhesReceitas.cshtml", receita);
        }
    }
}
