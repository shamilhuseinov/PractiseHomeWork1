using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Data.Contexts
{
	public static class DbContent
	{
		static DbContent()
		{
			Groups = new List<Group>();
		}
		public static List<Group> Groups { get; set; }

	}
}

