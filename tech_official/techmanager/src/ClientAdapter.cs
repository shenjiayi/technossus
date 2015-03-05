using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Java.Lang;
using Object = Java.Lang.Object;

namespace NavigationDrawer
{
	public class ClientAdapter:BaseAdapter<client>,IFilterable
	{
		private List<client> _allclient;
		private List<client> _partial;
		private Activity _activity;



		public ClientAdapter(Activity a,IEnumerable<client> clients)
		{
			_allclient = clients.OrderBy(s => s.contactName).ToList();
			_activity = a;

			Filter = new CilentFilter(this);

		}


		public override int Count {
			get {
				return _allclient.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			string response;
			System.IO.StreamReader strm = new System.IO.StreamReader (_activity.Assets.Open ("clients.json"));
			response = strm.ReadToEnd ();
			var obj = JsonObject.Parse (response);
			JsonArray names = (JsonArray) obj ["names"];
			JsonArray companies = (JsonArray) obj ["companies"];
			JsonArray emails = (JsonArray) obj ["emails"];


			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.ClientLayout, parent,false);
			var name = view.FindViewById<TextView> (Resource.Id.name);

			var contactName = view.FindViewById<TextView> (Resource.Id.contactName);
			var contactEmail = view.FindViewById<TextView> (Resource.Id.contactEmail);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			name.Text = companies.ElementAt (position);//_allclient [position].name;
			contactName.Text = "Contact: " + names.ElementAt (position);//_allclient [position].contactName;
			contactEmail.Text = "Email: " + emails.ElementAt (position);//_allclient [position].contactEmail;

			if (_allclient [position].photo == null) {

				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				contactImage.SetImageResource (Resource.Drawable.contactImage);

			} else {
				//not sure about this part

				var contactUri = ContentUris.WithAppendedId (ContactsContract.Contacts.ContentUri, _allclient [position].id);
				var contactPhotoUri = Android.Net.Uri.WithAppendedPath (contactUri, Contacts.Photos.ContentDirectory);

				contactImage.SetImageURI (contactPhotoUri);
			}
			return view;
		}


		public override client this[int position]
		{
			get { return _allclient[position]; }
		}

		public Filter Filter { get; private set; }


		private class CilentFilter : Filter
		{
			private readonly ClientAdapter _adapter;
			public CilentFilter(ClientAdapter adapter)
			{
				_adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<client>();
				if (_adapter._partial == null)
					_adapter._partial = _adapter._allclient;

				if (constraint == null) return returnObj;

				if (_adapter._partial != null && _adapter._partial.Any())
				{
					// Compare constraint to all names lowercased. 
					// It they are contained they are added to results.
					results.AddRange(
						_adapter._partial.Where(
							cilent => cilent.name.ToLower().Contains(constraint.ToString().ToLower())));
				}

				// Nasty piece of .NET to Java wrapping, be careful with this!
				returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
				returnObj.Count = results.Count;

				constraint.Dispose();

				return returnObj;
			}

			protected override void PublishResults(ICharSequence constraint, FilterResults results)
			{
				using (var values = results.Values)
					_adapter._allclient = values.ToArray<Object>()
						.Select(r => r.ToNetObject<client>()).ToList();


				_adapter.NotifyDataSetChanged();

				// Don't do this and see GREF counts rising
				constraint.Dispose();
				results.Dispose();
			}
		}

	}
}
