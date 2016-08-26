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
        public string Text { get; set; }
        public virtual Cat Cat { get; set;}
        public DateTime Created { get; set; }

    }
 
}