using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Search;
using Meow.Code.DAL;
using Meow.Code.Model;

namespace Meow.Controllers
{
    public class SearchController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
        // GET: Search
        public ActionResult Index(string id)
        {
            string searchString = id;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Cat currentCat = (Cat)Session[Constants.CURRENT_CAT_KEY];
                var myFollowingIds = from f in _context.Followers where f.IsFollowing.Equals(currentCat.Id) select f.IsBeingFollowed;
                var myFollowingCats = (
                    from c in _context.Kitties
                    where myFollowingIds.ToList().Contains(c.Id)
                    select c
                    ).ToList();

                var meows = (
                    from m in _context.Meows()
                    where m.Text.Contains(searchString)
                    select m
                    ).ToList();

                return View(new SearchModel()
                {
                    Meows = meows,
                    SearchString = searchString,
                    CatsFollowing = myFollowingCats
                });
            }

            return View(new SearchModel());
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            return Index(model.SearchString);
        }
    }
}