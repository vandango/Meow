using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Code.Model
{
    public class Topic
    {
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual Cat Owner { get; set; }
        public virtual  List<MeowMessage> Meows { get; set; }


    }
}