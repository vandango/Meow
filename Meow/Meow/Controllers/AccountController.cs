using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Code.DAL;
using Meow.Models.Account;

namespace Meow.Controllers
{
    public class AccountController : Controller
    {
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
            var cat = new Code.Model.Cat()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Created = DateTime.Now
            };
            _context.AddCat(cat);
            _context.Save();

            return Redirect($"/Account/Profile/{cat.Id}");
        }
    }
}