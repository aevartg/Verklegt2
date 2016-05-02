using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
	public class InputOutputModels
	{
		public int Id { get; set; }
		public int IdMilestone { get; set; }
		public string Input { get; set; }
		public string Output { get; set; }
	}
}