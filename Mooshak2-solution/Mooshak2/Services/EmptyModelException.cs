using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
	public class EmptyModelException : Exception
	{
		public string Message { get; set; }

		public override string ToString()
		{
			return Message;
		}
	}
}