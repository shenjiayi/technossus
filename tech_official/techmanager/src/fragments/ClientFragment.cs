using System;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;



//Ambiguities
using Fragment = Android.App.Fragment;
using System.Collections.Generic;

using SearchView = Android.Widget.SearchView;

namespace NavigationDrawer
{
	public class ClientFragment : Android.App.ListFragment
	{


		public const string ARG_NUMBER = "id_number";

		private SearchView _searchView;

		private ClientAdapter ClientAdapter;

		List<client> allclient = new List<client> ()
		{
			new client(0,null,"Apple","Mark Smith","marksmith@spaceX.com"),new client(0,null,"Dell","Alice bLALA","alice@uci.edu"),
			new client(0,null,"SpaceX","Mark Smith","marksmith@spaceX.com"),new client(0,null,"CompanyName","Peter Anteater","peteranteater@uci.edu")};



		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new ClientFragment();
			Bundle args = new Bundle ();
			args.PutInt (ClientFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}



		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.client, container, false);
			ListView clientlist;
			clientlist = rootView.FindViewById<ListView> (Resource.Id.clientlist);
			ClientAdapter = new ClientAdapter(this.Activity,allclient);
			clientlist.Adapter = ClientAdapter;
			this.SetHasOptionsMenu (true);
			return rootView;

		}


		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Fragment fragment = new ClientDetailFragment(allclient[position].contactName,allclient[position].contactEmail);
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

			_searchView.QueryTextChange += (s, e) => ClientAdapter.Filter.InvokeFilter(e.NewText);

			_searchView.QueryTextSubmit += (s, e) =>
			{
				// Handle enter/search button on keyboard here
				Toast.MakeText(this.Activity, "Searched for: " + e.Query, ToastLength.Short).Show();
				e.Handled = true;
			};

			MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(ClientAdapter));

		}
		
		private class SearchViewExpandListener 
			: Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
		{
			private readonly IFilterable _adapter;

			public SearchViewExpandListener(IFilterable adapter)
			{
				_adapter = adapter;
			}

			public bool OnMenuItemActionCollapse(IMenuItem item)
			{
				_adapter.Filter.InvokeFilter("");
				return true;
			}

			public bool OnMenuItemActionExpand(IMenuItem item)
			{
				return true;
			}
		}
	}
}

