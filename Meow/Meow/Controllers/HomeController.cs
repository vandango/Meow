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
            var model = new IndexModel();
            model.List = _context.Cats.ToList();
            model.Messages = _context.Meows.ToList();
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