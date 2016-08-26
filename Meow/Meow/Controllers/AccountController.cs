using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Code.DAL;
using Meow.Code.Model;
using Meow.Models.Account;
using log4net;

namespace Meow.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog LOG = LogManager.GetLogger("AccountController");

        private IMeowContext _context = new MeowContext();

        public AccountController() {}

        public AccountController(IMeowContext context)
        {
            _context = context;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterCat
        public ActionResult RegisterCat()
        {
            return View();
        }

        // POST: RegisterCat
        [HttpPost]
        public ActionResult RegisterCat(RegisterCatModel model)
        {
            var cat = new Cat()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Created = DateTime.Now
            };
            _context.AddCat(cat);
            try
            {
                _context.Save();
                return Redirect($"/Account/ProfileCat/{cat.Id}");
            } catch (AccountException e)
            {
                LOG.Error(e.Message, e);
                return Redirect("/Account/UnableToCreateAccount");
            }

            
        }

        // GET: ProfileCat
        public ActionResult ProfileCat(long? id)
        {
            if (id == null)
            {
                return Redirect("/Account/NotFound");
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
                return Redirect("/Account/NotFound");
            }
        }
    }
}
 