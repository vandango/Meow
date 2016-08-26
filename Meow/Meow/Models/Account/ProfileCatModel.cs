using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Models.Account
{
    public class ProfileCatModel
    {
        public string Username { get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}