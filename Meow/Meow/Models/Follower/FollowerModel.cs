using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meow.Code.Model;

namespace Meow.Models.Follower
{
    public class FollowerModel
    {
        public List<Cat> CatsBeingFollowed { get; set; }
        public List<Cat> CatsFollowing { get; set; }
    }
}