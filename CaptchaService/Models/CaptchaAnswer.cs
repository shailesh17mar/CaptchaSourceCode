using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptchaService.Models
{
    public class CaptchaAnswer
    {
        public string Answer { get; set; }
        public string Key { get; set; }
    }
}