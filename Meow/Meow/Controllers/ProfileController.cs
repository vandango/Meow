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
        public ActionResult ProfileCat(long? id)
        {
            if (id == null)
            {
                return Redirect("/static-html/NotFound.html");
            }

            Cat cat = _context.Find(id ?? -1);
            //greife ins backend
            if (cat != null)
            {
                var model = new ProfileCatModel()
                {
                    CreatedAt = cat.Created,
                    Email = cat.Email,
                    Password = cat.Password,
                    Username = cat.Username
                };
            
                return View(model);
            } else
            {
                return Redirect("/static-html/NotFound.html");
            }
        }

    }
}
 