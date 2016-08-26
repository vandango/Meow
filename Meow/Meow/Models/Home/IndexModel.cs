using System.Collections.Generic;
using Meow.Code.Model;

namespace Meow.Models.Home
{
	public class IndexModel
	{
		public Cat Single { get; set; }
		public List<Cat> List { get; set; }
        public List<MeowMessage> Messages { get; set; }
        public string Text { get; set; }
    }
}