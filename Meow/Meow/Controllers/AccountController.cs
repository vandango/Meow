using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meow.Code.DAL;
using Meow.Code.Model;
using Meow.Models.Account;

namespace Meow.Controllers
{
    public class AccountController : Controller
    {
        private readonly MeowContext _context = new MeowContext();
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
            _context.Cats.Add(cat);
            _context.SaveChanges();

            return Redirect($"/Account/ProfileCat/{cat.Id}");
        }

        // GET: ProfileCat
        public ActionResult ProfileCat(long id)
        {
            //greife ins backend
            Cat cat =_context.Cats.Find(id);
 
            var model = new ProfileCatModel()
            {
                CreatedAt = cat.Created,
                Email = cat.Email,
                Password = cat.Password,
                Username = cat.Username
            };
            
            return View(model);
        }
    }
}