using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace getAPIUsers.Models
{
    public class TokenCreate
    {
        public string bearerString { get; set; }
        public DateTime createDate { get; set; }
        public int secondLength { get; set; }
    }
}