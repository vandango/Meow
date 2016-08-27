using System;
using Meow.Code.DAL;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;
using Meow.Code.Model;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Net;
using Meow.Models.Api;

namespace Meow.Controllers.Api
{
	public class MeowsController : ApiController
	{
		private readonly MeowContext _meowContext = new MeowContext();

		private bool MeowExists(int id)
		{
			return _meowContext.Meows().Count(e => e.Id == id) > 0;
		}

		// GET: api/Meows
		public IQueryable<MeowMessageApi> GetMeowMessages()
		{
			return from m 
                   in _meowContext.Meows()
                   select new MeowMessageApi()
                   {
                       Id = m.Id,
                       Text = m.Text,
                       Created = m.Created,
                       CatId = m.Cat.Id,
                       CatUsername = m.Cat.Username,
                       CatEmail = m.Cat.Email,
                       CatCreated = m.Cat.Created
                   };
		}

		// GET: api/Meow/5
		[ResponseType(typeof(MeowMessageApi))]
		public IHttpActionResult GetMeow(int id)
		{
			var meow = _meowContext.Meows().FirstOrDefault((p) => p.Id == id);
			if(meow == null)
			{
				return NotFound();
			}
			return Ok(new MeowMessageApi()
            {
                Id = meow.Id,
                Text = meow.Text,
                Created = meow.Created,
                CatId = meow.Cat.Id,
                CatUsername = meow.Cat.Username,
                CatEmail = meow.Cat.Email,
                CatCreated = meow.Cat.Created
            });
		}

		// POST: api/Meow
		[ResponseType(typeof(MeowMessageApi))]
		public IHttpActionResult PostMeow(MeowMessageApi meow)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

		    var cat = _meowContext.Cats().FirstOrDefault(c => c.Id == meow.CatId);
		    if (cat == null)
		    {
		        return NotFound();
		    }

			_meowContext.Meows().Add(new MeowMessage()
			{
			    Text = meow.Text,
                Cat = cat,
                Created = DateTime.Now
			});
			_meowContext.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = meow.Id }, meow);
		}

		// PUT: api/Meow/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutMeow(int id, MeowMessageApi meow)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if(id != meow.Id)
			{
				return BadRequest();
			}

            var existingMeow = _meowContext.Meows().Find(id);
            if(existingMeow == null)
            {
                return NotFound();
            }

            // change only the content of a meow message
		    existingMeow.Text = meow.Text;

            _meowContext.Entry(existingMeow).State = EntityState.Modified;

            try
			{
				_meowContext.SaveChanges();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!MeowExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// DELETE: api/Meow/5
		[ResponseType(typeof(MeowMessageApi))]
		public IHttpActionResult DeleteMeow(int id)
		{
            var meow = _meowContext.Meows().Find(id);
            if(meow == null)
            {
                return NotFound();
            }

			_meowContext.Meows().Remove(meow);
			_meowContext.SaveChanges();

            return Ok(meow);
        }
	}
}