using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Meow.Code.Model
{
    public class Follower
    {
        public long Id { get; set; }
        public string IsFollowing { get; set; }
        public string IsBeingFollowed { get; set; }
    }
}