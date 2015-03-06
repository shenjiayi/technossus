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
	public class ProjectAdapter:BaseAdapter<project>,IFilterable
	{
		private List<project> _allproject;
		private List<project> _partial;
		private Activity _activity;



		public ProjectAdapter(Activity a,IEnumerable<project> project)
		{
			_allproject = project.OrderBy(s => s.name).ToList();
			_activity = a;

			Filter = new ProjectFilter(this);
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
			return position;
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


		public override project this[int position]
		{
			get { return _allproject[position]; }
		}


		public Filter Filter { get; private set; }


		private class ProjectFilter : Filter
		{
			private readonly ProjectAdapter _adapter;
			public ProjectFilter(ProjectAdapter adapter)
			{
				_adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<project>();
				if (_adapter._partial == null)
					_adapter._partial = _adapter._allproject;

				if (constraint == null) return returnObj;

				if (_adapter._partial != null && _adapter._partial.Any())
				{
					// Compare constraint to all names lowercased. 
					// It they are contained they are added to results.
					results.AddRange(
						_adapter._partial.Where(
							project => project.name.ToLower().Contains(constraint.ToString().ToLower())));
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
					_adapter._allproject = values.ToArray<Object>()
						.Select(r => r.ToNetObject<project>()).ToList();


				_adapter.NotifyDataSetChanged();

				// Don't do this and see GREF counts rising
				constraint.Dispose();
				results.Dispose();
			}
		}


	}
}
