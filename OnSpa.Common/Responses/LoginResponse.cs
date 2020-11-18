using OnSpa.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public User User { get; set; }
    }
}
