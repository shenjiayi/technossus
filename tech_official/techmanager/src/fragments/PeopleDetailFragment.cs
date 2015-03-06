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
		string _name;
		DateTime _available;
		string _technology;

		public PeopleDetailFragment(string name, DateTime available, string technology)
		{
			_name = name;
			_available = available;
			_technology = technology;
		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.PeopleDetail, container, false);

			TextView PeopleName = rootView.FindViewById<TextView> (Resource.Id.PeopleName);
			TextView Technology = rootView.FindViewById<TextView> (Resource.Id.Technology);
			TextView Available = rootView.FindViewById<TextView> (Resource.Id.Available);

			PeopleName.Text = "Name: "+ _name;
			Technology.Text = "Technology: " + _technology;
            Available.Text = "Available Date: " + _available.Date.ToString("d");

			return rootView;
		}
	}
}


