using System;
using System.Collections.Generic;

namespace NavigationDrawer
{
	public class project
	{
		public long id{ get; set; }
		public string name{ get; set; }
		public string startDate{get; set;}
		public string endDate{ get ; set; }
		public List<employee> teamMember { get; set; }
		public List<string> technology { get; set; }
		public string client{ get; set; }
		public string description{ get; set; }


		public project (long id, string name,string client, string startDate, string endDate, List<employee> teamMember, List<string> technology, string description)
		{
			this.id = id;
			this.name = name;
			this.client = client;
			this.startDate = startDate;
			this.endDate = endDate;
			this.technology = technology;
			this.teamMember= teamMember;
			this.technology = technology;
			this.description = description;
		}
	}
}

