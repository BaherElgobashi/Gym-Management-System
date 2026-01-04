using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfcaes
{
    public interface ISessionService
    {
        // Get All Sessions.
        IEnumerable<SessionViewModel> GetAllSessions();

        // Get Session By Id.
        SessionViewModel? GetSessionById(int SessionId);

        // Create New Session.
        bool CreateSession(CreateSessionViewModel CreatedSession);
    }
}
