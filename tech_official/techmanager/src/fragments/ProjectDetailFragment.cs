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
	public class ProjectDetailFragment:Fragment
	{

		public const string ARG_NUMBER = "id_number";

		public ProjectDetailFragment ()
		{
			// Empty constructor required for fragment subclasses
		}

		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new ProjectDetailFragment();
			Bundle args = new Bundle ();
			args.PutInt (ProjectDetailFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}



	}
}
