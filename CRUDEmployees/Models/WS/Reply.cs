using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDEmployees.Models.WS
{
    public class Reply
    {
        public int result { get; set; }
        public object data { get; set; }
        public string Message { get; set; }
    }
}