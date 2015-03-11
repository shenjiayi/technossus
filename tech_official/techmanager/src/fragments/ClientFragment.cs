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
	public class ClientFragment : ListFragment
	{


		public const string ARG_NUMBER = "id_number";

		SearchView _searchView;

		ClientAdapter ClientAdapter;

		List<Client> allclient;


		public ClientFragment(List<Client> data){
			allclient = data;
		}
			
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.client, container, false);
			ListView clientlist;
			clientlist = rootView.FindViewById<ListView> (Resource.Id.clientlist);
			ClientAdapter = new ClientAdapter(this.Activity,allclient);
			clientlist.Adapter = ClientAdapter;
			SetHasOptionsMenu (true);
			return rootView;
		}
			
		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Fragment fragment = new ClientDetailFragment(ClientAdapter.getClientFromPos(position));
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
	}
}

