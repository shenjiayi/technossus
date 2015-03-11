using Android.App;
using Android.OS;

//Ambiguities
using Fragment = Android.App.Fragment;

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
