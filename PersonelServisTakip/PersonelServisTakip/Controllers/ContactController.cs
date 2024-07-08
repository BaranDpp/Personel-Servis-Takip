using Microsoft.AspNetCore.Mvc;

namespace PersonelServisTakip.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
