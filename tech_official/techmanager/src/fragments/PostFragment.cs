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
	public class PostFragment : Fragment
	{
		public const string ARG_NUMBER = "id_number";
		List<post> allpost = new List<post> () {
			new post (0, null,"Added Jone to the project", "Peter Anteater", "Web Design", "technossus", "2014/2/12"),
			new post (0, null,"Added Jone to the project", "Peter Anteater", "Web Design", "technossus", "2014/2/12"),
			new post (0, null,"Added Jone to the project", "Peter Anteater", "Web Design", "technossus", "2014/2/12"),
			new post (0, null,"Added Jone to the project", "Peter Anteater", "Web Design", "technossus", "2014/2/12")
		};


		public PostFragment ()
		{
			// Empty constructor required for fragment subclasses
		}


		public static Fragment NewInstance (int position)
		{
			Fragment fragment = new PostFragment();
			Bundle args = new Bundle ();
			args.PutInt (PostFragment.ARG_NUMBER, position);
			fragment.Arguments = args;
			return fragment;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.post, container, false);
			ListView postlist;


			PostAdapter PostAdapter;
			postlist = rootView.FindViewById<ListView> (Resource.Id.postlist);
			PostAdapter = new PostAdapter(this.Activity,allpost);
			postlist.Adapter = PostAdapter;
			return rootView;

		}


	}
}

