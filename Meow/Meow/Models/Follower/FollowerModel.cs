using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meow.Code.Model;

namespace Meow.Models.Follower
{
    public class FollowerModel
    {
        public List<Cat> CatsBeingFollowed;
        public List<Cat> CatsFollowing;
    }
}