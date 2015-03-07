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


		string [] menu_item = {"Dashboard", "Web Design","Mobile App","Database design","People", "Clients", "Projects"};

		List<employee> allemployee = new List<employee> () {
			new employee (0, null, "Alice", "2015/3/1","Jave"),new employee (0, null, "Anteater", "2015/3/1","Jave"),new employee (0, null, "Ben", "2015/3/1","Jave"),	
			new employee (0, null, "James", "2014/4/6","Jave"), new employee (0, null, "Jone", "2014/3/2","Jave"),new employee (0, null, "Kate", "2015/3/1","Jave"),
			new employee (0, null, "Kitty", "2015/3/1","Jave"),new employee (0, null, "Peter", "2015/3/1","Jave"), 
			new employee (0, null, "Sam", "2015/3/1","Jave"),new employee (0, null, "Smith", "2015/3/1","Jave"), 

		};


		public ProjectFragment(List<project> data){
			allproject = data;
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

//		public override void OnListItemClick (ListView l, View v, int position, long id)
//		{
//			base.OnListItemClick (l, v, position, id);
//			switch (position) {
//				case 0: // Project 1
//					Activity.ActionBar.RemoveAllTabs ();
//				this.Activity.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
//					addTab ("Info", new ProjectInfoFragment ());
//					addTab ("Dashboard", new PostFragment ());
//					addTab ("Teammate", new PeopleFragment (allemployee));
//
////					Title = menu_item [position];
//					break;
//				case 1: // Project 2
//					break;
//				case 2: // Project 3
//					break;
//			}
//
//		}

		void addTab (string tabText, Fragment view)
		{
			var tab = this.Activity.ActionBar.NewTab ();
			tab.SetText (tabText);
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				var framgent = this.FragmentManager.FindFragmentById (Resource.Id.content_frame);
				if (framgent != null)
					e.FragmentTransaction.Remove (framgent);
				e.FragmentTransaction.Add (Resource.Id.content_frame, view);
			};
			tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Remove(view);
			};
			this.Activity.ActionBar.AddTab (tab);
		}

	}
}