using System;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using System.Collections.Generic;
using SearchView = Android.Widget.SearchView;



//Ambiguities
using Fragment = Android.App.Fragment;


namespace NavigationDrawer
{
	public class ProjectFragment :Android.App.ListFragment
	{
		public const string ARG_NUMBER = "id_number";



		private SearchView _searchView;

		private ProjectAdapter ProjectAdapter;

		List<project> allproject;

		List<post> allpost;


		public ProjectFragment(List<project> data, List<post> data1){
			allproject = data;
			allpost = data1;
		}




		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.project, container, false);
			ListView projectlist;
			projectlist = rootView.FindViewById<ListView> (Resource.Id.projectlist);
			ProjectAdapter = new ProjectAdapter(this.Activity,allproject);
			projectlist.Adapter = ProjectAdapter;
			SetHasOptionsMenu (true);
			return rootView;
		}

		public override void OnCreateOptionsMenu (IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.navigation_drawer, menu);

			var item = menu.FindItem(Resource.Id.action_search);

			var searchView = MenuItemCompat.GetActionView(item);
			_searchView = searchView.JavaCast<SearchView>();

			_searchView.QueryTextChange += (s, e) => ProjectAdapter.Filter.InvokeFilter(e.NewText);

			_searchView.QueryTextSubmit += (s, e) =>
			{
				// Handle enter/search button on keyboard here
				Toast.MakeText(this.Activity, "Searched for: " + e.Query, ToastLength.Short).Show();
				e.Handled = true;
			};

			MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(ProjectAdapter));

		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{


			base.OnListItemClick (l, v, position, id);
			List<post> data;
			Activity.ActionBar.RemoveAllTabs ();
			Activity.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			data = filterpost (allproject[position].name, allpost);
			addTab ("Info", new ProjectInfoFragment (allproject[position]));
			addTab ("Dashboard", new PostFragment (data));
			addTab ("Teammate", new PeopleFragment (allproject[position].teamMember));
			Activity.Title =allproject[position].name;

		}
			
		void addTab (string tabText, Fragment view)
		{
			var tab = this.Activity.ActionBar.NewTab ();
			tab.SetText (tabText);
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Replace(Resource.Id.content_frame,view);
			};
			this.Activity.ActionBar.AddTab (tab);
		}

		public List<post> filterpost (string projectname, List<post> allpost)
		{
			List<post> result = new List<post>{};
			foreach (post item in allpost){
				if (item.project == projectname)
					result.Add (item);
			}
			return result;
		}
	}
}