using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;



namespace NavigationDrawer
{
	public class ProjectAdapter:BaseAdapter
	{
		private List<project> _allproject;
		private Activity _activity;


		public ProjectAdapter(Activity a,List<project> data)
		{
			_activity = a;
			_allproject = data;
		}

		public override int Count {
			get {
				return _allproject.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return _allproject [position].id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.ProjectLayout, parent, false);
			var name = view.FindViewById<TextView> (Resource.Id.name);
			var ClientName = view.FindViewById<TextView> (Resource.Id.clientName);

			name.Text = _allproject [position].name;
			ClientName.Text = "Client: "+ _allproject [position].client;

			return view;

		}
	}
}
