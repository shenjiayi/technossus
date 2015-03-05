using System;
using System.Collections.Generic;

namespace NavigationDrawer
{
	public class employee
	{
		public long id { get; set; }
		public string photo{ get; set; }
		public string name{ get; set; }
		public string available{get;set;}
		public string technology{ get ; set; }

		public employee(long id, string photo,string name,string available,string technology)
		{
			this.id = id;
			this.photo = photo;
			this.name = name;
			this.available = available;
			this.technology = technology;
		}

	}
}

