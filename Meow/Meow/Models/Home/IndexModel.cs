﻿using System.Collections.Generic;
using Meow.Code.Model;

namespace Meow.Models.Home
{
	public class IndexModel
	{
		public Cat Single { get; set; }
		public List<Cat> List { get; set; }
	}
}