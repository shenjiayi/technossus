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
	[Activity (Label = "@string/app_name", Icon = "@drawable/ic_launcher")]
	public class NavigationDrawerActivity : Activity, MenuAdapter.OnItemClickListener
	{
		private DrawerLayout mDrawerLayout;
		private RecyclerView mDrawerList;
		private ActionBarDrawerToggle mDrawerToggle;

		private string mDrawerTitle;
		private String[] mMenuTitles;

		//data here 
		//make sure they are in alphebetical order for now
		List<employee> allemployee = new List<employee> () {
			new employee (0, null, "Alice", "2015/3/1","Jave"),new employee (0, null, "Anteater", "2015/3/1","Jave"),new employee (0, null, "Ben", "2015/3/1","Jave"),	
			new employee (0, null, "James", "2014/4/6","Jave"), new employee (0, null, "Jone", "2014/3/2","Jave"),new employee (0, null, "Kate", "2015/3/1","Jave"),
			new employee (0, null, "Kitty", "2015/3/1","Jave"),new employee (0, null, "Peter", "2015/3/1","Jave"), 
			new employee (0, null, "Sam", "2015/3/1","Jave"),new employee (0, null, "Smith", "2015/3/1","Jave"), 

		};

		List<employee> employeepartial = new List<employee> () {
			new employee (0, null, "Alice", "2015/3/1","Jave"),new employee (0, null, "Anteater", "2015/3/1","Jave"),new employee (0, null, "Ben", "2015/3/1","Jave"),	
			new employee (0, null, "James", "2014/4/6","Jave"), new employee (0, null, "Jone", "2014/3/2","Jave")
		};

		List<client> allclient = new List<client> ()
		{
			new client(0,null,"Apple","Mark Smith","marksmith@spaceX.com"),
			new client(0,null,"CompanyName","Peter Anteater","peteranteater@uci.edu"),
			new client(0,null,"Dell","Alice bLALA","alice@uci.edu"),
			new client(0,null,"SpaceX","Mark Smith","marksmith@spaceX.com")};

		List<client> clientpartial = new List<client> ()
		{
			new client(0,null,"CompanyName","Peter Anteater","peteranteater@uci.edu"),
			new client(0,null,"SpaceX","Mark Smith","marksmith@spaceX.com")};

		//project 1 data
		static List <employee> teamMember1 = new List<employee> () {
			new employee (0, null, "James", "2014/4/6","c++"),
			new employee (0, null, "Jone", "2014/3/2","Java"),
			new employee (0, null, "Kate", "2015/3/1","Python")
		};
		static List <string> technology1 = new List<string> { "java", "c#", "html" };

		List <project> allproject1 = new List<project> () {
			new project (0, "Database", "Apple", "2014/03/27", "2015/01/23", teamMember1, technology1,""),
			new project (0, "Mobile App", "UCI", "2014/06/24", "2015/05/23", teamMember1, technology1,""),
			new project (0, "Project Name", "SpaceX", "2014/03/24", "2015/07/23", teamMember1, technology1,""),
			new project (0, "Web Design", "Technossus","2014/02/24", "2015/07/23", teamMember1, technology1,"")

		};




		protected override void OnCreate (Bundle savedInstanceState)
		{

			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_navigation_drawer);

			mDrawerTitle = this.Title;
			mMenuTitles = this.Resources.GetStringArray (Resource.Array.planets_array);
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mDrawerList = FindViewById<RecyclerView> (Resource.Id.left_drawer);

			// set a custom shadow that overlays the main content when the drawer opens
			mDrawerLayout.SetDrawerShadow (Resource.Drawable.drawer_shadow, GravityCompat.Start);
			// improve performance by indicating the list if fixed size.
			mDrawerList.HasFixedSize = true;
			mDrawerList.SetLayoutManager (new LinearLayoutManager (this));

			// set up the drawer's list view with items and click listener
			mDrawerList.SetAdapter (new MenuAdapter (mMenuTitles, this));
			// enable ActionBar app icon to behave as action to toggle nav drawer
			this.ActionBar.SetDisplayHomeAsUpEnabled (true);
			this.ActionBar.SetHomeButtonEnabled (true);

			// ActionBarDrawerToggle ties together the the proper interactions
			// between the sliding drawer and the action bar app icon

			mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout,
				Resource.Drawable.ic_drawer, 
				Resource.String.drawer_open, 
				Resource.String.drawer_close);

			mDrawerLayout.SetDrawerListener (mDrawerToggle);
		}

		internal class MyActionBarDrawerToggle : ActionBarDrawerToggle
		{
			NavigationDrawerActivity owner;

			public MyActionBarDrawerToggle (NavigationDrawerActivity activity, DrawerLayout layout, int imgRes, int openRes, int closeRes)
				: base (activity, layout, imgRes, openRes, closeRes)
			{
				owner = activity;
			}

			public override void OnDrawerClosed (View drawerView)
			{
				owner.ActionBar.Title = owner.Title;
				owner.InvalidateOptionsMenu ();
			}

			public override void OnDrawerOpened (View drawerView)
			{
				owner.ActionBar.Title = owner.mDrawerTitle;
				owner.InvalidateOptionsMenu ();
			}
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			return false;
		}

		/* Called whenever we call invalidateOptionsMenu() */
		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			// If the nav drawer is open, hide action items related to the content view
			bool drawerOpen = mDrawerLayout.IsDrawerOpen (mDrawerList);
			menu.FindItem (Resource.Id.action_search).SetVisible (!drawerOpen);
			return base.OnPrepareOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			// The action bar home/up action should open or close the drawer.
			// ActionBarDrawerToggle will take care of this.
			if (mDrawerToggle.OnOptionsItemSelected (item)) {
				return true;
			}
			// Handle action buttons
			switch (item.ItemId) {
			default:
				return false;
			}
		}

		/* The click listener for RecyclerView in the navigation drawer */
		public void OnClick (View view, int position)
		{
			selectItem (position);
		}

		private void selectItem (int position)
		{
			switch (position) {
			case 0: // main dashboard
				break;
			case 1: // Project 1
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("Info", new ProjectInfoFragment ());
				addTab ("Dashboard", new PostFragment ());
				addTab ("Teammate", new PeopleFragment (allemployee));

				Title = mMenuTitles [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;
			case 2: // Project 2
				break;
			case 3: // Project 3
				break;
			case 4: // People Screen
				// update the main content by replacing fragments
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All people", new PeopleFragment (allemployee));
				addTab ("Teammates", new PeopleFragment (employeepartial));

				// update selected item title, then close the drawer
				Title = mMenuTitles [position];
				mDrawerLayout.CloseDrawer (mDrawerList);

				break;
			case 5: // Client Screen
				// update the main content by replacing fragments
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ClientFragment (allclient));
				addTab ("Your Clients", new ClientFragment (clientpartial));

				// update selected item title, then close the drawer
				Title = mMenuTitles [position];
				mDrawerLayout.CloseDrawer (mDrawerList);

				break;
			case 6: // Project Screen
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ProjectFragment (allproject1));
				addTab ("Your Clients", new ProjectFragment (allproject1));

				// update selected item title, then close the drawer
				Title = mMenuTitles [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;
			}
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutInt ("tab", this.ActionBar.SelectedNavigationIndex);
			base.OnSaveInstanceState (outState);
		}

		void addTab (string tabText, Fragment view)
		{
			var tab = this.ActionBar.NewTab ();
			tab.SetText (tabText);
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				var framgent = this.FragmentManager.FindFragmentById (Resource.Id.content_frame);
				if (framgent != null)
					e.FragmentTransaction.Remove (framgent);
				e.FragmentTransaction.Add (Resource.Id.content_frame, view);
			};
			tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Remove(view);
			};

			this.ActionBar.AddTab (tab);
		}




		private void SetTitle (string title)
		{
			this.Title = title;
			this.ActionBar.Title = title;
		}

		protected override void OnTitleChanged (Java.Lang.ICharSequence title, Android.Graphics.Color color)
		{
			//base.OnTitleChanged (title, color);
			this.ActionBar.Title = title.ToString ();
		}


		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			// Sync the toggle state after onRestoreInstanceState has occurred.
			mDrawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			// Pass any configuration change to the drawer toggls
			mDrawerToggle.OnConfigurationChanged (newConfig);
		}


	}
}


