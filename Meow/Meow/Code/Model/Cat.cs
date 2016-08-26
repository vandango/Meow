using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Meow.Code.Model
{
    public class Cat
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
        public virtual List<Meow> Meow { get; set; }

    }
}