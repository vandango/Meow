using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Follower;
using Meow.Code.DAL;

namespace Meow.Controllers
{
    public class FollowerController : Controller
    {
        private readonly MeowContext _context = new MeowContext();

        // GET: List all users (being followed and not being followed)
        public ActionResult Index()
        {
            //TODO Ersetzen mit getCurrentCat.getId o.ä.
            long ownId = 1;
            var myFollowerIds = from f in _context.Follower where f.IsBeingFollowed.Equals(ownId) select f.IsFollowing;
            var myFollowerCats = from c in _context.Cats where myFollowerIds.ToList().Contains(c.Id) select c;

            var myFollowingIds = from f in _context.Follower where f.IsFollowing.Equals(ownId) select f.IsBeingFollowed;
            var myFollowingCats = from c in _context.Cats where myFollowingIds.ToList().Contains(c.Id) select c;

            return View(new FollowerModel() { CatsBeingFollowed = myFollowerCats.ToList(), CatsFollowing = myFollowingCats.ToList() });
        }
        public ActionResult Follow(FollowerModel model)
        {
            return View(model);
        }
    }
}