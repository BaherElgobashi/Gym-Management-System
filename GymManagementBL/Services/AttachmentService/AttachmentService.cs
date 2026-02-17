using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public string? Upload(string folderName, IFormFile File)
        {
            throw new NotImplementedException();
        }
    
        public bool Delete(string folderName, string fileName)
        {
            throw new NotImplementedException();
        }

    }
}
