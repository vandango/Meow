using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Models.Home;
using Meow.Code.DAL;
using Meow.Code.Model;
using Meow.Models.Profile;

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

            var followers = _context.Follower.Where(f => f.IsFollowing == currentCat.Id).ToList();

            List<long> ids = new List<long>();

            foreach (Follower follower in followers)
            {
                ids.Add(follower.IsFollowing);
            }
            ids.Add(currentCat.Id);

            var model = new IndexModel();

            var meows = _context.Meows.Where(s => ids.Contains(s.Cat.Id));
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

        [HttpPost]
        public ActionResult Login(ProfileCatModel model)
        {
            var loggedInCat = _context.FindByCredentials(model.Username, model.Password);
            if (loggedInCat != null)
            {
                //put verified cat in sessioncontext
                Session.Add(Constants.CURRENT_CAT_KEY, loggedInCat);
                return Redirect("/");
            } else
            {
                return View();
            }
        }
    }
}