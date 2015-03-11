using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Java.Lang;
using Object = Java.Lang.Object;

namespace NavigationDrawer
{
	public class ClientAdapter:BaseAdapter<Client>,IFilterable
	{
		private List<Client> allclient;
		private List<Client> partial;
		private Activity activity;

		public ClientAdapter(Activity a,IEnumerable<Client> clients)
		{
			allclient = clients.OrderBy(s => s.name).ToList();
			partial = null;
			activity = a;

			Filter = new ClientFilter(this);
		}

		public override int Count {
			get 
			{
				return allclient.Count;
			}
		}

		public Client getClientFromPos(int position)
		{
			return allclient[position];
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
			var view = convertView ?? activity.LayoutInflater.Inflate (Resource.Layout.ClientLayout, parent,false);
			var name = view.FindViewById<TextView> (Resource.Id.name);

			var contactName = view.FindViewById<TextView> (Resource.Id.contactName);
			var contactEmail = view.FindViewById<TextView> (Resource.Id.contactEmail);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			name.Text = allclient [position].name;
			contactName.Text = "Contact: "+ allclient [position].contactName;
			contactEmail.Text = "Email: " + allclient [position].contactEmail;

			if (allclient [position].photo == null)
			{
				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				contactImage.SetImageResource (Resource.Drawable.contactImage);

			} 
			else 
			{
				var contactUri = ContentUris.WithAppendedId (ContactsContract.Contacts.ContentUri, allclient [position].id);
				var contactPhotoUri = Android.Net.Uri.WithAppendedPath (contactUri, Contacts.Photos.ContentDirectory);
				contactImage.SetImageURI (contactPhotoUri);
			}
			return view;
		}


		public override Client this[int position]
		{
			get { return allclient[position]; }
		}

		public Filter Filter { get; private set; }


		private class ClientFilter : Filter
		{
			private readonly ClientAdapter adapter;
			public ClientFilter(ClientAdapter adapter)
			{
				this.adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<Client>();
				if (adapter.partial == null)
					adapter.partial = adapter.allclient;

				if (constraint == null) return returnObj;

				if (adapter.partial != null && adapter.partial.Any())
				{
                    string lowerQuery = constraint.ToString().ToLower();

					// Compare constraint to all fields of client
					results.AddRange(
						adapter.partial.Where(
                            client => QueryClient(client, lowerQuery)
                        ));
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
					adapter.allclient = values.ToArray<Object>()
						.Select(r => r.ToNetObject<Client>()).ToList();
						
				adapter.NotifyDataSetChanged();
				constraint.Dispose();
				results.Dispose();
			}

			// Return true if the client matches all of the query tokens of the constraint
            private bool QueryClient(Client c, string query)
            {
                string[] tokens = query.Trim().Split(' ');
                foreach (string q in tokens)
                {
                    if (!QueryTokenClient(c,q))
                    {
                        return false;
                    }
                }

                return true;
            }

			// Helper function to check the client fields for query string
            private bool QueryTokenClient(Client c, string query)
            {
				return ((c.name != null && c.name.ToLower().Contains(query)) 
					|| (c.contactName != null && c.contactName.ToLower().Contains(query)) 
					|| (c.contactEmail != null && c.contactEmail.ToLower().Contains(query)));
            }
		}

	}
}
