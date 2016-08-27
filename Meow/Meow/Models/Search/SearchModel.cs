using System;
using System.Collections.Generic;
using Meow.Code.Model;

namespace Meow.Models.Search
{
    public class SearchModel
    {
        public string SearchString { get; set; }
        //TODO Change from <Cat> to <Meow>
        public List<MeowMessage> Meows { get; set; }
        public List<Cat> CatsFollowing { get; set; }
    }
}