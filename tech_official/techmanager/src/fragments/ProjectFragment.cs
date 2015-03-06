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
	public class ProjectFragment : Fragment
	{
		public const string ARG_NUMBER = "id_number";



		private SearchView _searchView;

		private ProjectAdapter ProjectAdapter;

		List<project> allproject;


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


	}
}