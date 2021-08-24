using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_API.DTO
{
    public class GoogleConfigModel
    {
        public const string GoogleConfig = "GoogleConfig";
        public string Key { get; set; }
        public string Secret { get; set; }
    }
}
