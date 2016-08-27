using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Code.DAL;
using Meow.Code.Model;
using Meow.Models.Profile;
using log4net;

namespace Meow.Controllers
{
    [HandleError(ExceptionType = typeof(AccountCreationException), View = "/_Shared/_Error")]
    public class ProfileController : Controller
    {
        private static readonly ILog LOG = LogManager.GetLogger("ProfileController");

        private IMeowContext _context = new MeowContext();

        public ProfileController() {}

        public ProfileController(IMeowContext context)
        {
            _context = context;
        }

        // GET: ProfileCat
        public ActionResult ProfileCat(string id)
        {
            if (id == null)
            {
                return Redirect("/static-html/NotFound.html");
            }

            // if id instance of Long -> user Find by PrimaryKey
            // otherwise: Find By Username
            Cat profileCat = null;
            try
            {
                long numericalId = Convert.ToInt64(id);
                profileCat = _context.Find(numericalId);
            }
            catch (FormatException)
            {
                profileCat = _context.FindByUsername(id);
            }

            //greife ins backend
            if (profileCat != null)
            {
                var currentCat = (Cat)Session[Constants.CURRENT_CAT_KEY];
                var cats = new List<Cat>();
               
                if (currentCat.Id != profileCat.Id)
                {
                    var followers = _context.Follower().Where(f => f.IsFollowing == currentCat.Id).ToList();

                    List<long> ids = new List<long>();

                    foreach (Follower follower in followers)
                    {
                        ids.Add(follower.IsBeingFollowed);
                    }

                    cats = _context.Cats().Where(c => ids.Contains(c.Id)).ToList();
                }
                

                var model = new ProfileCatModel()
                {
                    CreatedAt = profileCat.Created,
                    Email = profileCat.Email,
                    Password = profileCat.Password,
                    Username = profileCat.Username,
                    Id = profileCat.Id,
                    CatsFollowing = cats
                };
            
                return View(model);
            } else
            {
                return Redirect("/static-html/NotFound.html");
            }
        }

    }
}
 