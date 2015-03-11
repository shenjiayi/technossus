using System;
using System.Collections.Generic;

namespace NavigationDrawer
{
	public class Project
	{
		public long id{ get; set; }
		public string name{ get; set; }
		public DateTime startDate{get; set;}
		public DateTime endDate{ get ; set; }
		public List<Employee> teamMember { get; set; }
		public List<string> technology { get; set; }
		public string client{ get; set; }
		public string description{ get; set; }


		public Project (long id, string name,string client, string startDate, string endDate, List<Employee> teamMember, List<string> technology, string description)
		{
			this.id = id;
			this.name = name;
			this.client = client;
            this.startDate = DateUtil.convertToDateTime(startDate);
            this.endDate = DateUtil.convertToDateTime(endDate);
			this.technology = technology;
			this.teamMember= teamMember;
			this.technology = technology;
			this.description = description;
		}
	}
}

