using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meow.Code.Model;

namespace Meow.Models.Home
{
    public class MessagesModel
    {
        public string Text { get; set; }
        public Cat Autor {get;set;}
    }
}