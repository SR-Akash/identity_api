using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.DTO
{
    public class AuthDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public int expires_in { get; set; }
        public string ActionTime { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
