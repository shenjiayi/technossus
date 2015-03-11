using Android.App;
using Android.Content.Res;
using Android.Views;
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
		//data here 
		//make sure they are in alphebetical order for now
		List<employee> allpeople;
		List<employee> peoplepartial;
		List<client> allclient;
		List<client> clientpartial;
		List<post> allpost;
		List<project> allproject;

		protected override void OnCreate (Bundle savedInstanceState)
		{

			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_navigation_drawer);

			mDrawerTitle = this.Title;
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mDrawerList = FindViewById<RecyclerView> (Resource.Id.left_drawer);

			// set a custom shadow that overlays the main content when the drawer opens
			mDrawerLayout.SetDrawerShadow (Resource.Drawable.drawer_shadow, GravityCompat.Start);
			// improve performance by indicating the list if fixed size.
			mDrawerList.HasFixedSize = true;
			mDrawerList.SetLayoutManager (new LinearLayoutManager (this));

			// set up the drawer's list view with items and click listener
			allproject = LoadProjectData ();
			string[] menu_item = new string[allproject.Count+5];
			menu_item[0] = "Dashboard";
			for (int i = 0; i < allproject.Count; ++i) {
				menu_item [i + 1] = allproject [i].name;}
			menu_item[allproject.Count+1] = "People";
			menu_item[allproject.Count+2] = "Clients";
			menu_item[allproject.Count+3] = "Projects";
			menu_item[allproject.Count+4] = "Log out";

			mDrawerList.SetAdapter (new MenuAdapter(menu_item,this));

			// enable ActionBar app icon to behave as menu_item toggle nav drawer
			this.ActionBar.SetDisplayHomeAsUpEnabled (true);
			this.ActionBar.SetHomeButtonEnabled (true);

			// ActionBarDrawerToggle ties together the the proper interactions
			// between the sliding drawer and the action bar app icon

			mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout,
				Resource.Drawable.ic_drawer, 
				Resource.String.drawer_open, 
				Resource.String.drawer_close);

			mDrawerLayout.SetDrawerListener (mDrawerToggle);
			if (savedInstanceState == null) //first launch
				selectItem (0);
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
			if (mDrawerToggle.OnOptionsItemSelected (item)) 
			{
				return true;
			}
			// Handle action buttons
			switch (item.ItemId)
			{
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
			List<post> data;

			// Dash Board
			if (position == 0) 
			{
				// Load Data
				allpost = LoadDashProjectData();

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				ActionBar.NavigationMode = ActionBarNavigationMode.Standard;
				var fragmentManger = this.FragmentManager;
				var ft = fragmentManger.BeginTransaction ();
				ft.Replace (Resource.Id.content_frame, new PostFragment (allpost));
				ft.Commit ();

				Title = "Dashboard";
				mDrawerLayout.CloseDrawer (mDrawerList);
			} 
			// Project Pages
			else if (position > 0 && position < allproject.Count + 1) 
			{
				// Load Data
				//allproject;

				//Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				data = filterpost (allproject [position - 1].name, allpost);
				addTab ("Info", new ProjectInfoFragment (allproject [position - 1]));
				addTab ("Dashboard", new PostFragment (data));
				addTab ("Teammate", new PeopleFragment (allproject [position - 1].teamMember));
				Title = allproject [position - 1].name;
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// People Screen
			else if (position == allproject.Count + 1)
			{
				// Load Data
				allpeople = LoadPeopleData("ALL");
				peoplepartial = LoadPeopleData("PARTIAL");

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All people", new PeopleFragment (allpeople));
				addTab ("Your Teammates", new PeopleFragment (peoplepartial));
				Title = "People";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Client Screen
			else if (position == allproject.Count + 2) 
			{
				// Load Data
				allclient = LoadClientData("ALL");
				clientpartial = LoadClientData("PARTIAL");

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ClientFragment (allclient));
				addTab ("Your Clients", new ClientFragment (clientpartial));
				Title = "Clients";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Project Screen
			else if (position == allproject.Count + 3) 
			{
				// Load Data
				//allproject;
				allpost = LoadDashProjectData();

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Project", new ProjectFragment (allproject,allpost));
				addTab ("Your Project", new ProjectFragment (allproject,allpost));
				Title = "Projects";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Logout
			else if (position == allproject.Count + 4) 
			{
				StartActivity (typeof(MainActivity));
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
                    people_list.Add(new employee(j["id"], null, j["name"], j["avail"], j["technology"]));
				}
			}

            List<employee> ordered_people_list = people_list.OrderBy(x => x.name).ToList();

            return ordered_people_list;
		}
			

		private List<post> LoadDashProjectData()
		{
			List<post> post = new List<post>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "myprojects.json");
			var list = (JsonArray) data["myprojectsdash"];

			foreach (JsonObject j in list)
			{
				post.Add(new post(j["id"], j["photo"], j["content"], j["name"], j["projectname"], j["companyname"], j["date"]));
			}

			List<post> ordered_post_list = post.OrderBy(x => x.name).ToList();

			return ordered_post_list;
		}


		private List<project> LoadProjectData()
		{

			List<project> projects = new List<project>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "myprojects.json");
			var list = (JsonArray) data["myprojectsside"];

			foreach (JsonObject j in list)
			{
				var teamMembers = new List<employee>();
				foreach (JsonValue member in j["team_member"])
				{
					teamMembers.Add(FindEmployee(member));
				}

				var technologies = new List<string>();
				foreach (JsonValue technology in j["technology"])
				{
					technologies.Add(technology);
				}

				projects.Add(new project(j["id"], j["projectname"], j["comp"], j["start"], j["end"], teamMembers.OrderBy(x => x.name).ToList(), technologies.OrderBy(x => x).ToList(), ""));
			}

			List<project> ordered_proj_list = projects.OrderBy(x => x.name).ToList();

			return ordered_proj_list;
		}

		private employee FindEmployee(string name)
		{
			List<employee> employeeList = LoadPeopleData("ALL");
			foreach (employee e in employeeList)
			{
				if (e.name.Equals(name))
				{
					return e;
				}
			}

			return null;
		}
	}

}


