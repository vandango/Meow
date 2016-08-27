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
            return View();
        }
        public ActionResult Index(FollowerModel model)
        {
            var otherCats = from c in _context.Cats select c;
            //TODO Ersetzen mit getCurrentCat
            otherCats = otherCats.Where(c => !c.Id.Equals(1));
            return View(new FollowerModel() {catsBeingFollowed = otherCats.ToList() });
        }
    }
}