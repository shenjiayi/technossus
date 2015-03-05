
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
	public class ClientDetailFragment : Fragment
	{
		string _name;
		string _email;

		public ClientDetailFragment(string name, string email)
		{
			_name = name;
			_email = email;
		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.ClientDetail, container, false);

			TextView ClientName = rootView.FindViewById<TextView> (Resource.Id.ClientName);
			TextView ContactEmail = rootView.FindViewById<TextView> (Resource.Id.ContactEmail);

			ClientName.Text = "Client Name: "+ _name;
			ContactEmail.Text = "Contact Email: " + _email;

			return rootView;
		}
	}
}


