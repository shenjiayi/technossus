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

using Java.Lang;
using Object = Java.Lang.Object;

namespace NavigationDrawer
{
	public class PeopleAdapter:BaseAdapter<employee>,IFilterable
	{
		private List<employee> _allemployee;
		private Activity _activity; 
		private List<employee> _partial;



		public PeopleAdapter(Activity a, IEnumerable<employee> data)
		{
			_allemployee = data.OrderBy(s => s.name).ToList();
			_activity = a;

			Filter = new PeopleFilter(this);
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
			//			return position;
		}



		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.EmployeeLayout, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.name);
			var contactAvailable = view.FindViewById<TextView> (Resource.Id.available);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			contactName.Text = _allemployee [position].name;
            contactAvailable.Text = "Available: "+_allemployee [position].available.Date.ToString("d");

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


		public override employee this[int position]
		{
			get { return _allemployee[position]; }
		}

		public Filter Filter { get; private set; }


		private class PeopleFilter : Filter
		{
			private readonly PeopleAdapter _adapter;
			public PeopleFilter(PeopleAdapter adapter)
			{
				_adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<employee>();
				if (_adapter._partial == null)
					_adapter._partial = _adapter._allemployee;

				if (constraint == null) return returnObj;

				if (_adapter._partial != null && _adapter._partial.Any())
				{
                    string lowerQuery = constraint.ToString().ToLower();

					// Compare constraint to all fields of Employee
					results.AddRange(
						_adapter._partial.Where(
                            employee => QueryEmployee(employee, lowerQuery)
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
					_adapter._allemployee = values.ToArray<Object>()
						.Select(r => r.ToNetObject<employee>()).ToList();


				_adapter.NotifyDataSetChanged();

				// Don't do this and see GREF counts rising
				constraint.Dispose();
				results.Dispose();
			}

            // Overall Query method, returns list of employee that satisfies all tokens of query
            private bool QueryEmployee(employee e, string query)
            {
                string[] tokens = query.Trim().Split(' ');
                foreach (string q in tokens)
                {
                    // If employee does not contain a token, return false
                    if (!QueryTokenEmployee(e, q))
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool QueryTokenEmployee(employee e, string query)
            {
                return (e.name.ToLower().Contains(query) || e.technology.ToLower().Contains(query) || DateUtil.isDuringMonth(e.available, query));
            }
		}
	}
}

