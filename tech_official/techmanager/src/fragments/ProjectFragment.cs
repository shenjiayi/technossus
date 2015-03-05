using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


//Ambiguities
using Fragment = Android.App.Fragment;
using System.Collections.Generic;

namespace NavigationDrawer
{
	public class ProjectFragment : Fragment
	{
		public const string ARG_NUMBER = "id_number";

		public ProjectFragment()
		{
		}


		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new ProjectFragment();
			Bundle args = new Bundle ();
			args.PutInt (ProjectFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.project, container, false);
			ListView projectlist;
			List <employee> teamMember = new List<employee> () {
				new employee (0, null, "Jone", "2014/3/2","Java"),
				new employee (0, null, "James", "2014/4/6","c++"),
				new employee (0, null, "Kate", "2015/3/1","Python")
			};
			List <string> technology = new List<string> { "java", "c#", "html" };

			List <project> allproject = new List<project> () {
				new project (0, "Project Name", "SpaceX", "2014/03/24", "2015/07/23", teamMember, technology,""),
				new project (0, "Mobile App", "UCI", "2014/06/24", "2015/05/23", teamMember, technology,""),
				new project (0, "Web Design", "Technossus","2014/02/24", "2015/07/23", teamMember, technology,""),
				new project (0, "Database", "Apple", "2014/03/27", "2015/01/23", teamMember, technology,"")
			};


			ProjectAdapter ProjectAdapter;
			projectlist = rootView.FindViewById<ListView> (Resource.Id.projectlist);
			ProjectAdapter = new ProjectAdapter(this.Activity,allproject);
			projectlist.Adapter = ProjectAdapter;
			return rootView;


		}


	}
}