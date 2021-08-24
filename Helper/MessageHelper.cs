using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Identity_API.Helper
{
    public class MessageHelper
    {
        [DataMember]
        public string Message { get; set; }
        public int statuscode { get; set; }
        public long Key { get; set; }
    }
}
