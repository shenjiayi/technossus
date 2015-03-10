using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


//Ambiguities
using Fragment = Android.App.Fragment;
using System.Collections.Generic;


namespace NavigationDrawer
{
	public class PeopleDetailFragment : Fragment
	{
        private readonly employee e;

		public PeopleDetailFragment(employee e)
		{
            this.e = e;
		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.PeopleDetail, container, false);

			TextView PeopleName = rootView.FindViewById<TextView> (Resource.Id.PeopleName);
			TextView Technology = rootView.FindViewById<TextView> (Resource.Id.Technology);
			TextView Available = rootView.FindViewById<TextView> (Resource.Id.Available);

			PeopleName.Text = "Name: "+ e.name;
			Technology.Text = "Technology: " + e.technology;
            Available.Text = "Available Date: " + e.available.Date.ToString("d");

			return rootView;
		}
	}
}


