
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



namespace NavigationDrawer
{
	public class ClientFragmentPartial : Android.App.ListFragment
	{
		public const string ARG_NUMBER = "id_number";
		List<client> allclient = new List<client> ()
		{
			new client(0,null,"SpaceX","Mark Smith","marksmith@spaceX.com"),new client(0,null,"CompanyName","Peter Anteater","peteranteater@uci.edu")};



		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new ClientFragmentPartial();
			Bundle args = new Bundle ();
			args.PutInt (PeopleFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.client, container, false);
			ListView clientlist;

			ClientAdapter ClientAdapter;
			clientlist = rootView.FindViewById<ListView> (Resource.Id.clientlist);
			ClientAdapter = new ClientAdapter(this.Activity,allclient);
			clientlist.Adapter = ClientAdapter;
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


	}
}

