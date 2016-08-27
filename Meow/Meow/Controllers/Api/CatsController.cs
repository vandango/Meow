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
	public class CatsController : ApiController
	{
		private readonly MeowContext _meowContext = new MeowContext();

		private bool CatExists(int id)
		{
			return _meowContext.Cats.Count(e => e.Id == id) > 0;
		}

		// GET: api/Cats
		public IQueryable<Cat> GetCats()
		{
			return from c in _meowContext.Cats select c;
		}

		// GET: api/Cat/5
		[ResponseType(typeof(Cat))]
		public IHttpActionResult GetProduct(int id)
		{
			var cat = _meowContext.Cats.FirstOrDefault((p) => p.Id == id);
			if(cat == null)
			{
				return NotFound();
			}
			return Ok(cat);
		}

		// POST: api/Cats
		[ResponseType(typeof(Cat))]
		public IHttpActionResult PostCat(Cat cat)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_meowContext.Cats.Add(cat);
			_meowContext.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = cat.Id }, cat);
		}

		// PUT: api/Cat/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCat(int id, Cat cat)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if(id != cat.Id)
			{
				return BadRequest();
			}

			_meowContext.Entry(cat).State = EntityState.Modified;

			try
			{
				_meowContext.SaveChanges();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!CatExists(id))
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

		// DELETE: api/Cats/5
		[ResponseType(typeof(Cat))]
		public IHttpActionResult DeleteCat(int id)
		{
			Cat cat = _meowContext.Cats.Find(id);
			if(cat == null)
			{
				return NotFound();
			}

			_meowContext.Cats.Remove(cat);
			_meowContext.SaveChanges();

			return Ok(cat);
		}
	}
}