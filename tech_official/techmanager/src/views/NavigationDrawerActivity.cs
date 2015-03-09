﻿using System;

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
using System.Json;
using System.Linq;


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

        // Use this to find relevant data for user.  For testing purposes
        private readonly string default_user = "John Doe";


		//make sure they are in alphebetical order for now
		List<employee> allpeople;
		List<employee> peopelpartial;
		List<client> allclient;
		List<client> clientpartial;


		string [] menu_item = {"Dashboard", "Mobile App","Database Design","Web Design","People", "Clients", "Projects","Log out"};

		//project 1 data
		static List <employee> teamMember1 = new List<employee> () {
			new employee (0, null, "Anteater", "2015/2/20","c++"),
			new employee (0, null, "James", "2015/1/20","Java"),
		};
		static List <employee> teamMember2 = new List<employee> () {
			new employee (0, null, "Anteater","2015/2/20","c++"),
			new employee (0, null, "Carrie", "2015/5/20","Java"),
		};
		static List <employee> teamMember3 = new List<employee> () {
			new employee (0, null, "Anteater", "2015/2/20","c++"),
			new employee (0, null, "James", "2015/1/20","Java"),
		};

		static List <string> technology1 = new List<string> { "SQL", ".NET", "html" };
		static List <string> technology2 = new List<string> { "java", "c#", "html" };
		static List <string> technology3 = new List<string> { "C++", "c#", "css", "Linex" };



		List <project> allproject = new List<project> () {
			new project (0, "Database Design", "Apple", "2014/03/27", "2014/09/23", teamMember1, technology1,""),
			new project (0, "Mobile App", "Dell", "2015/02/24", "2015/05/23", teamMember2, technology2,""),
			new project (0, "Web Design", "SpaceX","2014/02/24", "2015/07/23", teamMember3, technology3,"Design a website")
		};


		List<post> allpost = new List<post> () {
			new post (0, null,"Added Carrie to the project", "Peter Anteater", "Web Design", "SpaceX", "2014/2/12"),
			new post (0, null,"Added James to the project", "Peter Anteater", "Mobile App", "Dell", "2014/2/12"),
			new post (0, null,"Looking forward to working on the project", "Jone Smith", "Web Design", "SpaceX", "2014/2/14"),
			new post (0, null,"Meeing at 9:30", "Peter Anteater", "Mobile App", "Dell", "2014/2/15"),
			new post (0, null,"Added Ada the project", "Peter Anteater", "Database Design", "SpaceX", "2014/3/27"),
			new post (0, null,"Meeting tommorow", "Peter Anteater", "Web Design", "SpaceX", "2014/3/12"),
			new post (0, null,"Just commit my changes", "Kate Chen", "Web Design", "SpaceX", "2014/5/12")
		};




		protected override void OnCreate (Bundle savedInstanceState)
		{

			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_navigation_drawer);

			mDrawerTitle = this.Title;
			//			mMenuTitles = this.Resources.GetStringArray (Resource.Array.menu_array);
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mDrawerList = FindViewById<RecyclerView> (Resource.Id.left_drawer);

			// set a custom shadow that overlays the main content when the drawer opens
			mDrawerLayout.SetDrawerShadow (Resource.Drawable.drawer_shadow, GravityCompat.Start);
			// improve performance by indicating the list if fixed size.
			mDrawerList.HasFixedSize = true;
			mDrawerList.SetLayoutManager (new LinearLayoutManager (this));

			// set up the drawer's list view with items and click listener
			mDrawerList.SetAdapter (new MenuAdapter (menu_item, this));
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
			allpeople = LoadPeopleData("ALL");
			peopelpartial = LoadPeopleData("PARTIAL");
			allclient = LoadClientData("ALL");
			clientpartial =LoadClientData("PARTIAL");
			List<post> data;

			switch (position) {
			case 0: // main dashboard
				ActionBar.RemoveAllTabs ();
				var fragmentManger = this.FragmentManager;
				var ft = fragmentManger.BeginTransaction ();
				ft.Replace (Resource.Id.content_frame, new PostFragment(allpost));
				ft.Commit ();

				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;
			case 1: // Project 1
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				data = filterpost (menu_item [position], allpost);
				addTab ("Info", new ProjectInfoFragment (allproject[0]));
				addTab ("Dashboard", new PostFragment (data));
				addTab ("Teammate", new PeopleFragment (allproject[0].teamMember));
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;
			case 2: // Project 2
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				data = filterpost (menu_item [position], allpost);
				addTab ("Info", new ProjectInfoFragment (allproject[1]));
				addTab ("Dashboard", new PostFragment (data));
				addTab ("Teammate", new PeopleFragment (allproject[1].teamMember));
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;


			case 3: // Project 3
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				data = filterpost (menu_item [position], allpost);
				addTab ("Info", new ProjectInfoFragment (allproject[2]));
				addTab ("Dashboard", new PostFragment (data));
				addTab ("Teammate", new PeopleFragment (allproject[2].teamMember));
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;


			case 4: // People Screen
				ActionBar.RemoveAllTabs ();
				ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All people", new PeopleFragment (allpeople));
				addTab ("Teammates", new PeopleFragment (peopelpartial));

				// update selected item title, then close the drawer
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;

			case 5: // Client Screen
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ClientFragment (allclient));
				addTab ("Your Clients", new ClientFragment (clientpartial));

				// update selected item title, then close the drawer
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;

			case 6: // Project Screen
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ProjectFragment (allproject,allpost));
				addTab ("Your Clients", new ProjectFragment (allproject,allpost));

				// update selected item title, then close the drawer
				Title = menu_item [position];
				mDrawerLayout.CloseDrawer (mDrawerList);
				break;

			case 7: //logout 
				base.OnBackPressed ();
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
			this.ActionBar.AddTab (tab);
		}

		public override void OnBackPressed ()
		{
			if (FragmentManager.BackStackEntryCount > 0) {
				FragmentManager.PopBackStack();
			} else {
				return;
			}
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

		public List<post> filterpost (string projectname, List<post> allpost)
		{
			List<post> result = new List<post>{};
			foreach (post item in allpost){
				if (item.project == projectname)
					result.Add (item);
			}
			return result;
		}

        private List<client> LoadClientData(string arg)
        {
            List<client> client_list = new List<client>();
            var jl = new JsonLoader();
            JsonValue data = jl.LoadData(this, "clients.json");
            var list = (JsonArray) data["client_list"];

            foreach (JsonObject j in list)
            {
                // Two cases, either all will be let through, or only those that are your client (FULL/PARTIAL)
                if (arg.Equals("ALL") || j["your_client"])
                {
                    client_list.Add(new client(j["id"], null, j["company"], j["contact_name"], j["contact_email"]));
                }
            }

            List<client> ordered_client_list = client_list.OrderBy(x => x.name).ToList();

            return ordered_client_list;
        }

		private List<employee> LoadPeopleData(string arg)
		{
			List<employee> people_list = new List<employee>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "people.json");
			var list = (JsonArray) data["people_list"];

			foreach (JsonObject j in list)
			{
				// Two cases, either all will be let through, or only those that are your client (FULL/PARTIAL)
				if (arg.Equals("ALL") || j["your_people"])
				{
					people_list.Add(new employee(j["id"], null, j["name"], j["avail"], null));
				}
			}

            List<employee> ordered_people_list = people_list.OrderBy(x => x.name).ToList();

            return ordered_people_list;
		}

	}
}


