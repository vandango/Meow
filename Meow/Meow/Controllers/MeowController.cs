using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Controllers
{
    public class MeowController
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