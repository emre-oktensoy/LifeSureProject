using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeSureProject.Models
{
    public class InstagramApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public InstagramProfile Data { get; set; }
    }

}