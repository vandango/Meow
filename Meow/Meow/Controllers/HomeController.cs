using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Home;
using Meow.Code.DAL;

namespace Meow.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
        // GET: Home
        public ActionResult Index()
        {
            var model = new IndexModel();
            model.List = _context.Cats.ToList();
            return View(model);
        }
    }
}