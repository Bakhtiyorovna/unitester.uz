using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitester_Service.Dtos.Auth
{
    public class TeacherRegisterDto
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; }
        public string UserName { get; set; } = String.Empty;
        public IFormFile Image { get; set; } = default!;
        public string Password { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
