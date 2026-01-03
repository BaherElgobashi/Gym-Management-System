using GymManagementBLL.ViewModels.SessionViewModels;
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
    }
}
