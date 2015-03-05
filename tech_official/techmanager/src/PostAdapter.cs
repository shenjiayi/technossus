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
	public class PostAdapter:BaseAdapter
	{
		private List<post> _allposts;
		private Activity _activity;


		public PostAdapter(Activity a,List<post> data)
		{
			_activity = a;
			_allposts = data;
		}

		public override int Count {
			get {
				return _allposts.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return _allposts [position].id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.PostLayout, parent, false);
			var name = view.FindViewById<TextView> (Resource.Id.name);
			var content = view.FindViewById<TextView> (Resource.Id.content);
			var project = view.FindViewById<TextView> (Resource.Id.project);
			var date = view.FindViewById<TextView> (Resource.Id.date);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			name.Text = _allposts [position].name;
			content.Text = _allposts [position].content;
			date.Text = _allposts [position].date;
			project.Text = _allposts [position].project;


			if (_allposts [position].photo == null) {

				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				contactImage.SetImageResource (Resource.Drawable.contactImage);

			} else {


			}
			return view;
		}

	}

}


