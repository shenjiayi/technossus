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
	public class PeopleAdapter:BaseAdapter
	{
		private List<employee> _allemployee;
		private Activity _activity; 



		public PeopleAdapter(Activity a, List<employee> data)
		{
			_activity = a;
			_allemployee = data;
		}



		public override int Count {
			get {
				return _allemployee.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return _allemployee [position].id;
		}



		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.EmployeeLayout, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.name);
			var contactAvailable = view.FindViewById<TextView> (Resource.Id.available);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			contactName.Text = _allemployee [position].name;
			contactAvailable.Text = "Available:"+_allemployee [position].available;

			if (_allemployee [position].photo == null) {

				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				contactImage.SetImageResource (Resource.Drawable.contactImage);

			} else {

				//not sure about this part

				var contactUri = ContentUris.WithAppendedId (ContactsContract.Contacts.ContentUri, _allemployee [position].id);
				var contactPhotoUri = Android.Net.Uri.WithAppendedPath (contactUri, Contacts.Photos.ContentDirectory);

				contactImage.SetImageURI (contactPhotoUri);
			}
			return view;
		}

	}

}

