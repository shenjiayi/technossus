using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Provider;

using Java.Lang;
using Object = Java.Lang.Object;
using System;

namespace NavigationDrawer
{
	public class PeopleAdapter:BaseAdapter<Employee>,IFilterable
	{
		List<Employee> allemployee;
		List<Employee> partial;
		Activity activity; 

		public PeopleAdapter(Activity a, IEnumerable<Employee> data)
		{
			allemployee = data.OrderBy(s => s.name).ToList();
			partial = null;
			activity = a;

			Filter = new PeopleFilter(this);
		}
            
		public override int Count {
			get {
				return allemployee.Count;
			}
		}

        public Employee GetEmployeeFromPos(int position)
        {
            return allemployee[position];
        }

		public override Object GetItem (int position)
		{
			return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			var view = convertView ?? activity.LayoutInflater.Inflate (Resource.Layout.EmployeeLayout, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.name);
			var contactAvailable = view.FindViewById<TextView> (Resource.Id.available);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.picture);

			contactName.Text = allemployee [position].name;
            contactAvailable.Text = "Available: "+allemployee [position].available.Date.ToString("d");

			if (allemployee [position].photo == null) 
			{
				contactImage = view.FindViewById<ImageView> (Resource.Id.picture);
				Random temp = new Random ();
				int index = temp.Next (1, 6);
				if (index == 1) {
					contactImage.SetImageResource (Resource.Drawable.contactImage);
				}
				if (index == 2) {
					contactImage.SetImageResource (Resource.Drawable.people1);
				}
				if (index == 3 ){
					contactImage.SetImageResource (Resource.Drawable.people2);
				}
				if (index == 4 ){
					contactImage.SetImageResource (Resource.Drawable.people3);
				}
				if (index == 5){
					contactImage.SetImageResource (Resource.Drawable.people4);
				}

			} 
			else 
			{
				var contactUri = ContentUris.WithAppendedId (ContactsContract.Contacts.ContentUri, allemployee [position].id);
				var contactPhotoUri = Android.Net.Uri.WithAppendedPath (contactUri, Contacts.Photos.ContentDirectory);

				contactImage.SetImageURI (contactPhotoUri);
			}

			return view;
		}


		public override Employee this[int position]
		{
			get { return allemployee[position]; }
		}

		public Filter Filter { get; private set; }

		private class PeopleFilter : Filter
		{
			private readonly PeopleAdapter adapter;

			public PeopleFilter(PeopleAdapter adapter)
			{
				this.adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<Employee>();
				if (adapter.partial == null)
					adapter.partial = adapter.allemployee;

				if (constraint == null) return returnObj;

				if (adapter.partial != null && adapter.partial.Any())
				{
                    string lowerQuery = constraint.ToString().ToLower();

					// Compare constraint to all fields of Employee
					results.AddRange(
						adapter.partial.Where(
                            employee => QueryEmployee(employee, lowerQuery)
                        ));
				}
					
				returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
				returnObj.Count = results.Count;
				constraint.Dispose();

				return returnObj;
			}

			protected override void PublishResults(ICharSequence constraint, FilterResults results)
			{
				using (var values = results.Values)				
					adapter.allemployee = values.ToArray<Object>()
						.Select(r => r.ToNetObject<Employee>()).ToList();
						
				adapter.NotifyDataSetChanged();
				constraint.Dispose();
				results.Dispose();
			}

            // Overall Query method, returns list of employee that satisfies all tokens of query
            private bool QueryEmployee(Employee e, string query)
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

            private bool QueryTokenEmployee(Employee e, string query)
            {
				return ((e.name != null && e.name.ToLower().Contains(query))
					|| (e.technology !=null && e.technology.ToLower().Contains(query))
					|| DateUtil.isDuringMonth(e.available, query));
            }
		}
	}
}

