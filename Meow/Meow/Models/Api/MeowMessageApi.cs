using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Models.Api
{
	public class MeowMessageApi
	{
		public long Id { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }

		public long CatId { get; set; }
		public string CatUsername { get; set; }
		public DateTime CatCreated { get; set; }
		public string CatEmail { get; set; }
	}
}