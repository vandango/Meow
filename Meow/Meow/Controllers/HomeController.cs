using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Home;
using Meow.Code.DAL;
using Meow.Code.Model;

namespace Meow.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
        // GET: Home
        public ActionResult Index()
        {

            Session["CurrentCat"] = _context.Cats.FirstOrDefault(c => c.Id == 1);

            Cat currentCat = (Cat)Session["CurrentCat"];

            var ids = _context.Follower.Where(f => f.IsFollowing == currentCat.Id).ToList();



            var model = new IndexModel();
            model.List = _context.Cats.ToList();

            
            var meows = _context.Meows.Where(s => s.Cat.Id == currentCat.Id);
            meows.OrderByDescending(s => s.Created);
            model.Messages = meows.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            var meowMessage = new MeowMessage()
            {
                Text = model.Text,
                Cat = _context.Cats.FirstOrDefault(c => c.Id == 1),
                Created = DateTime.Now
            };
   
            _context.Meows.Add(meowMessage);
            _context.SaveChanges();



            return Index();
        }
    }
}