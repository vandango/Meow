using Meow.Code.DAL;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;
using Meow.Code.Model;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace Meow.Controllers.Api
{
	public class MeowController : ApiController
	{
		private readonly MeowContext _meowContext = new MeowContext();

		private bool MeowExists(int id)
		{
			return _meowContext.Meows.Count(e => e.Id == id) > 0;
		}

		// GET: api/Meow
		public IQueryable<MeowMessage> GetMeowMessages()
		{
			return from m in _meowContext.Meows select m;
		}

		// GET: api/Meow/5
		[ResponseType(typeof(MeowMessage))]
		public IHttpActionResult GetMeow(int id)
		{
			var meow = _meowContext.Meows.FirstOrDefault((p) => p.Id == id);
			if(meow == null)
			{
				return NotFound();
			}
			return Ok(meow);
		}

		// POST: api/Meow
		[ResponseType(typeof(MeowMessage))]
		public IHttpActionResult PostMeow(MeowMessage meow)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_meowContext.Meows.Add(meow);
			_meowContext.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = meow.Id }, meow);
		}

		// PUT: api/Meow/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutMeow(int id, MeowMessage meow)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if(id != meow.Id)
			{
				return BadRequest();
			}

			_meowContext.Entry(meow).State = EntityState.Modified;

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
		[ResponseType(typeof(MeowMessage))]
		public IHttpActionResult DeleteMeow(int id)
		{
			MeowMessage meow = _meowContext.Meows.Find(id);
			if(meow == null)
			{
				return NotFound();
			}

			_meowContext.Meows.Remove(meow);
			_meowContext.SaveChanges();

			return Ok(meow);
		}
	}
}