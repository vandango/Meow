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
        private readonly IMeowContext _context = new MeowContext();
        // GET: Home
        public ActionResult Index()
        {

            Cat currentCat = (Cat)Session[Constants.CURRENT_CAT_KEY];
            if (currentCat == null)
            {
                return Redirect("/Home/Login");
            }

            var followers = _context.Follower().Where(f => f.IsFollowing == currentCat.Id).ToList();

            List<long> ids = new List<long>();

            foreach (Follower follower in followers)
            {
                ids.Add(follower.IsFollowing);
            }
            ids.Add(currentCat.Id);

            var model = new IndexModel();

            var meows = _context.Meows().Where(s => ids.Contains(s.Cat.Id));
            meows.OrderByDescending(s => s.Created);
            model.Messages = meows.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            var currentCatId = ((Cat)Session[Constants.CURRENT_CAT_KEY]).Id;
            var meowMessage = new MeowMessage()
            {
                Text = model.Text,

                Cat = _context.Cats().FirstOrDefault(c => c.Id == currentCatId),
                Created = DateTime.Now
            };
   
            _context.Meows().Add(meowMessage);
            _context.SaveChanges();



            return Index();
        }


        public ActionResult Login()
        {
            return View();
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