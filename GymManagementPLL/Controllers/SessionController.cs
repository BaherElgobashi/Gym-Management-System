using GymManagementBLL.Services.Interfcaes;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSessions();

            return View(Sessions);
        }
    }
}
