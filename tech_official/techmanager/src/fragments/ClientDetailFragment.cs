
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
        private readonly client c;

        public ClientDetailFragment(client c)
		{
            this.c = c;
		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.ClientDetail, container, false);

			TextView ClientName = rootView.FindViewById<TextView> (Resource.Id.ClientName);
            TextView ContactName = rootView.FindViewById<TextView>(Resource.Id.ContactName);
			TextView ContactEmail = rootView.FindViewById<TextView> (Resource.Id.ContactEmail);

			ClientName.Text = "Client Name: " + c.name;
            ContactName.Text = "Contact Name: " + c.contactName;
            ContactEmail.Text = "Contact Email: " + c.contactEmail;
			return rootView;
		}
	}
}


