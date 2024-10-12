using Microsoft.AspNetCore.Mvc;

namespace NoteBookProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
