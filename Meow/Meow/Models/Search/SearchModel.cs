using System;
using System.Collections.Generic;
using Meow.Code.Model;

namespace Meow.Models.Search
{
    public class SearchModel
    {
        public String Schlagwort { get; set; }
        public List<Cat> Cats { get; set; }
    }
}