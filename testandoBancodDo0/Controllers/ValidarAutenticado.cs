using Microsoft.AspNetCore.Mvc;

namespace testandoBancodDo0.Controllers
{
    public class ValidarAutenticado : Controller
    {
        [HttpGet]
        public IActionResult Autenticado()
        {
            
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("PrincipalHomeAutenticado", "Site");
            }

            
            return RedirectToAction("PrincipalHome", "Site");
        }
    }
}
