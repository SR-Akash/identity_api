﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.DTO
{
    public class UserLoginDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public bool isMasterUser { get; set; }
        public AuthDTO auth { get; set; }
    }
}
