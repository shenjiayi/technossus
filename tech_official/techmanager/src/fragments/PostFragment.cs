using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;

//Ambiguities
using Fragment = Android.App.Fragment;
using System.Collections.Generic;


namespace NavigationDrawer
{
	public class PostFragment : Fragment
	{
		public const string ARG_NUMBER = "id_number";
		List<Post> allpost;

		public PostFragment(List<Post> data){
			allpost = data;
		}

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

