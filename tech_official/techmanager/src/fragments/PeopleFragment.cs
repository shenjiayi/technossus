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
	public class PeopleFragment:Android.App.ListFragment
	{

		public const string ARG_NUMBER = "id_number";

		private SearchView _searchView;

		private PeopleAdapter PeopleAdapter;

		List<employee> allemployee;

		public PeopleFragment(List<employee> data){
			allemployee = data;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.people, container, false);
			ListView employeelist;

			employeelist = rootView.FindViewById<ListView> (Resource.Id.employeelist);
			PeopleAdapter = new PeopleAdapter (this.Activity, allemployee);
			employeelist.Adapter = PeopleAdapter;
			SetHasOptionsMenu (true);
			return rootView;
		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
            Fragment fragment = new PeopleDetailFragment(PeopleAdapter.GetEmployeeFromPos(position));
			Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			transaction.Replace (Resource.Id.content_frame, fragment);
			transaction.AddToBackStack (null);
			transaction.Commit ();
		}


		public override void OnCreateOptionsMenu (IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.navigation_drawer, menu);

			var item = menu.FindItem(Resource.Id.action_search);

			var searchView = MenuItemCompat.GetActionView(item);
			_searchView = searchView.JavaCast<SearchView>();

			_searchView.QueryTextChange += (s, e) => PeopleAdapter.Filter.InvokeFilter(e.NewText);

			_searchView.QueryTextSubmit += (s, e) =>
			{
				// Handle enter/search button on keyboard here
				Toast.MakeText(this.Activity, "Searched for: " + e.Query, ToastLength.Short).Show();
				e.Handled = true;
			};

			MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(PeopleAdapter));

		}

	}
}

