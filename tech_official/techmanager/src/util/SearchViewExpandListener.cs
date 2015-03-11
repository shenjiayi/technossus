using Android.Views;
using Android.Widget;
using Android.Support.V4.View;


namespace NavigationDrawer
{
	public class SearchViewExpandListener 
			: Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
		{
			private readonly IFilterable _adapter;

			public SearchViewExpandListener(IFilterable adapter)
			{
				_adapter = adapter;
			}

			public bool OnMenuItemActionCollapse(IMenuItem item)
			{
				_adapter.Filter.InvokeFilter("");
				return true;
			}

			public bool OnMenuItemActionExpand(IMenuItem item)
			{
				return true;
			}
		}
}

