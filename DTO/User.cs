using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.DTO
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNo { get; set; }
        public bool isMasterUser { get; set; }
    }
}
