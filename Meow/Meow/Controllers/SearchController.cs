using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Search;
using Meow.Code.DAL;

namespace Meow.Controllers
{
    public class SearchController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            var meows = from m in _context.Meows select m;
            string SearchString = model.SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                //TODO Change from Cats to Meows
                meows = meows.Where(m => m.Text.Contains(SearchString));
            }
            return View(new SearchModel() {Meows = meows.ToList()});
        }
    }
}