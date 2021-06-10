using Microsoft.AspNetCore.Mvc;

namespace ChatRoomsUI.Controllers
{
    public class ChatBase : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
