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

namespace NavigationDrawer
{
	public class PeopleFragment:Android.App.ListFragment
	{
        private PeopleModel model;

		public const string ARG_NUMBER = "id_number";

        private List<employee> allemployee;

        // Deprecated constructor
		public PeopleFragment ()
		{
			// Empty constructor required for fragment subclasses
            model = null;
		}

        // TESTING
        public PeopleFragment (PeopleModel model)
        {
            this.model = model;
            allemployee = model.getFullEmployeeList();
        }

		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new PeopleFragment();
			Bundle args = new Bundle ();
			args.PutInt (PeopleFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.people, container, false);
			ListView employeelist;

			PeopleAdapter PeopleAdapter;
			employeelist = rootView.FindViewById<ListView> (Resource.Id.employeelist);
            PeopleAdapter = new PeopleAdapter (this.Activity, allemployee);
			employeelist.Adapter = PeopleAdapter;

			return rootView;
		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Fragment fragment = new PeopleDetailFragment(allemployee[position].name,allemployee[position].available,allemployee[position].technology);
			Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			transaction.Replace (Resource.Id.content_frame, fragment);
			transaction.AddToBackStack (null);
			transaction.Commit ();
		}

	}
}

