using System;
using System.Collections.Generic;

namespace NavigationDrawer
{
	public class PeopleModel
	{
		private List<employee> employeeList;

		public PeopleModel ()
		{
			employeeList = new List<employee> ();
		}

		// This is the parameterized search of the model class, use overloaded methods for convience
		public List<employee> Search(long id, String name, String available, String technology)
		{
			List<employee> filteredList = new List<employee>();

			foreach (employee e in employeeList)
			{
				if ((id != -1 && e.id == id) // Check Id
					|| (name != null && e.name.Equals(name, StringComparison.OrdinalIgnoreCase)) // Check Name
					|| (available != null && e.available.Equals(available, StringComparison.OrdinalIgnoreCase)) // Check Availibility
					|| (technology != null && e.technology.IndexOf(technology, StringComparison.OrdinalIgnoreCase) >= 0)) // Check Technology
				{
					filteredList.Add(e);
				}
			}

			return filteredList;
		}

		// General search that uses the search term on all fields, first will check if string is a number
		public List<employee> Search(String s)
		{
			long id;
			try
			{
				id = Convert.ToInt64(s);
				return Search(id, null, null, null);
			}
			catch(FormatException e)
			{
			}
			return Search(-1, s, s, s);
		}


		public void setEmployeeList(List<employee> employeeList)
		{
			this.employeeList = employeeList;
		}

		public List<employee> getFullEmployeeList()
		{
			return employeeList;
		}

		// TODO Delete this method when we are able to manipulate real data
		public void loadMockData()
		{
			List<employee> allemployee = new List<employee> () {
				new employee (1, null, "Jone", "2014/3/2","Java,C++,Python"), new employee (2, null, "James", "2014/4/6","Java,C++,Python"), new employee (3, null, "Kate", "2015/3/1","Java,C++,Python"),
				new employee (4, null, "Smith", "2015/3/1","Java,C++,Python"), new employee (5, null, "Peter", "2015/3/1","Java,C++,Python"), new employee (6, null, "Alice", "2015/3/1","Java,C++,Python"),
				new employee (7, null, "Kitty", "2015/3/1","Java,C++,Python"), new employee (8, null, "Sam", "2015/3/1","Java,C++,Python"), new employee (9, null, "Ben", "2015/3/1","Java,C++,Python"),	
				new employee (10, null, "Anteater", "2015/3/1","Java,C++,Python")
			};

			setEmployeeList(allemployee);
		}

	}
}

