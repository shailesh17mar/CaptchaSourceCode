using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptchaService.Models
{
    public class CaptchaResponse
    {
        public string Referer { get; set; }
        public string Key { get; set; }
        public byte[] Image { get; set; }
    }
}