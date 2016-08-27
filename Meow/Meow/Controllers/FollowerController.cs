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

        public ActionResult Index()
        {
            long uId = this.getCurrentUserId();
            var myFollowerIds = from f in _context.Follower() where f.IsBeingFollowed.Equals(uId) select f.IsFollowing;
            var myFollowerCats = from c in _context.Cats() where myFollowerIds.ToList().Contains(c.Id) select c;

            var myFollowingIds = from f in _context.Follower() where f.IsFollowing.Equals(uId) select f.IsBeingFollowed;
            var myFollowingCats = from c in _context.Cats() where myFollowingIds.ToList().Contains(c.Id) select c;

            return View(new FollowerModel() { CatsBeingFollowed = myFollowerCats.ToList(), CatsFollowing = myFollowingCats.ToList() });
        }
        public ActionResult Unfollow(long id, string returnUrl)
        {
            var FollowerToDelete = from f in _context.Follower() where f.IsBeingFollowed.Equals(id) && f.IsFollowing.Equals(this.getCurrentUserId()) select f;
            Follower Follower = FollowerToDelete.ToList()[0];
            _context.Follower().Remove(Follower);
            _context.SaveChanges();
            return Redirect(returnUrl??"/");
        }
        public ActionResult Follow(long id, string returnUrl)
        {
            Follower Follower = new Follower { IsBeingFollowed = id, IsFollowing = this.getCurrentUserId() };
            _context.Follower().Add(Follower);
            _context.SaveChanges();
            return Redirect(returnUrl??"/");
        }

        private long getCurrentUserId()
        {
            Cat currentCat = (Cat)Session[Constants.CURRENT_CAT_KEY];
            return currentCat.Id;
        }
    }
}