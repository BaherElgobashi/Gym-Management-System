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

        // Get Session To Update.
        UpdateSessionViewModel? GetSessionToUpdate(int SessionId);

        // Update the Session.
        bool UpdateSession(UpdateSessionViewModel UpdatedSession , int SessionId);

        // Delete The Session.
        bool RemoveSession(int SessionId);

        IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown();


    }
}
