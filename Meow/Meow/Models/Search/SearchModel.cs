using System;
using System.Collections.Generic;
using Meow.Code.Model;

namespace Meow.Models.Search
{
    public class SearchModel
    {
        public string SearchString { get; set; }
        public List<MeowMessage> Meows { get; set; }
        public List<Cat> CatsFollowing { get; set; }
    }
}