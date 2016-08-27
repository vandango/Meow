using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Follower;
using Meow.Code.DAL;
using Meow.Code.Model;

namespace Meow.Controllers
{
    public class FollowerController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
        //TODO Ersetzen mit getCurrentCat.getId o.ä.
        private long ownId = 3;

        public ActionResult Index()
        {
            var myFollowerIds = from f in _context.Follower where f.IsBeingFollowed.Equals(ownId) select f.IsFollowing;
            var myFollowerCats = from c in _context.Cats where myFollowerIds.ToList().Contains(c.Id) select c;

            var myFollowingIds = from f in _context.Follower where f.IsFollowing.Equals(ownId) select f.IsBeingFollowed;
            var myFollowingCats = from c in _context.Cats where myFollowingIds.ToList().Contains(c.Id) select c;

            return View(new FollowerModel() { CatsBeingFollowed = myFollowerCats.ToList(), CatsFollowing = myFollowingCats.ToList() });
        }
        public ActionResult Unfollow(long id)
        {
            var FollowerToDelete = from f in _context.Follower where f.IsBeingFollowed.Equals(id) && f.IsFollowing.Equals(ownId) select f;
            Follower Follower = FollowerToDelete.ToList()[0];
            _context.Follower.Remove(Follower);
            _context.SaveChanges();
            return Redirect("/Follower");
        }
    }
}