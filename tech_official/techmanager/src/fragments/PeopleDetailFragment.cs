using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;


//Ambiguities
using Fragment = Android.App.Fragment;

namespace NavigationDrawer
{
	public class PeopleDetailFragment : Fragment
	{
        private readonly Employee e;

		public PeopleDetailFragment(Employee e)
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


