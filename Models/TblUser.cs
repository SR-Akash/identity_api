using System;
using System.Collections.Generic;

#nullable disable

namespace Identity_API.Models
{
    public partial class TblUser
    {
        public long IntUserId { get; set; }
        public string StrUserName { get; set; }
        public string StrPassword { get; set; }
        public string StrEmail { get; set; }
        public string StrPhone { get; set; }
        public bool IsMasterUser { get; set; }
        public bool IsActive { get; set; }
        public DateTime DteInsertTime { get; set; }
    }
}
