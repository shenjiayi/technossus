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

		List<post> allpost = new List<post> () {
			new post (0, null,"Added Jone Smith to the project", "Peter Anteater", "Web Design", "technossus", "2014/2/12"),
			new post (0, null,"Looking forward to working on the project", "Jone Smith", "Web Design", "technossus", "2014/2/14"),
			new post (0, null,"Meeting tommorow", "Peter Anteater", "Web Design", "technossus", "2014/3/12"),
			new post (0, null,"Just commit my changes", "Kate Chen", "Web Design", "technossus", "2014/5/12")
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

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			switch (position) {
				case 0: // Project 1
					break;
				case 1: // Project 2
					break;
				case 2: // Project 3
					Activity.ActionBar.RemoveAllTabs ();
					this.Activity.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
					addTab ("Info", new ProjectInfoFragment (allproject[position]));
					addTab ("Dashboard", new PostFragment (allpost));
					addTab ("Teammate", new PeopleFragment (allproject[position].teamMember));
					break;
			}

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
	}
}