using Android.App;
using Android.Views;
using Android.Widget;
using Fragment = Android.App.Fragment;
using System.Collections.Generic;


namespace NavigationDrawer
{
	public class PostAdapter:BaseAdapter
	{
		private readonly List<Post> allposts;
		private readonly Activity activity;

		public PostAdapter(Activity a,List<Post> data)
		{
			activity = a;
			allposts = data;
		}

		public override int Count {
			get {
				return allposts.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return allposts [position].id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var view = convertView ?? activity.LayoutInflater.Inflate (Resource.Layout.PostLayout, parent, false);
			var name = view.FindViewById<TextView> (Resource.Id.name);
			var content = view.FindViewById<TextView> (Resource.Id.content);
			var project = view.FindViewById<TextView> (Resource.Id.project);
			var date = view.FindViewById<TextView> (Resource.Id.date);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			name.Text = allposts [position].name;
			content.Text = allposts [position].content;
			date.Text = allposts [position].date;
			project.Text = allposts [position].project;

			if (allposts [position].photo == null) 
			{
				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				contactImage.SetImageResource (Resource.Drawable.contactImage);
			}

			return view;
		}

	}

}


