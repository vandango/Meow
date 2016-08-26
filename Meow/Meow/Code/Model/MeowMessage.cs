using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meow.Code.Model;

namespace Meow.Code.Model
{
    public class MeowMessage
    {
        public long Id { get; set; }
        public string text { get; set; }
        public virtual Cat cat { get; set;}
        public DateTime CreatedAdd { get; set; }

    }
 
}