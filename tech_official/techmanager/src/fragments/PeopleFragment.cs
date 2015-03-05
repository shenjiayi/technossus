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

		public const string ARG_NUMBER = "id_number";

        private List<employee> allemployee = new List<employee> () {
            new employee (1, null, "Jone", "2014/3/2","Java,C++,Python"), new employee (2, null, "James", "2014/4/6","Java,C++,Python"), new employee (3, null, "Kate", "2015/3/1","Java,C++,Python"),
            new employee (4, null, "Smith", "2015/3/1","Java,C++,Python"), new employee (5, null, "Peter", "2015/3/1","Java,C++,Python"), new employee (6, null, "Alice", "2015/3/1","Java,C++,Python"),
            new employee (7, null, "Kitty", "2015/3/1","Java,C++,Python"), new employee (8, null, "Sam", "2015/3/1","Java,C++,Python"), new employee (9, null, "Ben", "2015/3/1","Java,C++,Python"),    
            new employee (10, null, "Anteater", "2015/3/1","Java,C++,Python")
        };

        // Deprecated constructor
		public PeopleFragment ()
		{
			// Empty constructor required for fragment subclasses
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

