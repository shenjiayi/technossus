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
		List<Employee> allPeople;
		List<Employee> partialPeople;
		List<Client> allClient;
		List<Client> partialClient;
		List<Post> allPost;
		List<Project> allProject;

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
			allProject = LoadProjectData ();
			string[] menu_item = new string[allProject.Count+5];
			menu_item[0] = "Dashboard";
			for (int i = 0; i < allProject.Count; ++i) {
				menu_item [i + 1] = allProject [i].name;}
			menu_item[allProject.Count+1] = "People";
			menu_item[allProject.Count+2] = "Clients";
			menu_item[allProject.Count+3] = "Projects";
			menu_item[allProject.Count+4] = "Log out";

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

			// Dash Board
			if (position == 0) 
			{
				// Load Data
				allPost = LoadDashProjectData();

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				ActionBar.NavigationMode = ActionBarNavigationMode.Standard;
				var fragmentManger = this.FragmentManager;
				var ft = fragmentManger.BeginTransaction ();
				ft.Replace (Resource.Id.content_frame, new PostFragment (allPost));
				ft.Commit ();

				Title = "Dashboard";
				mDrawerLayout.CloseDrawer (mDrawerList);
			} 
			// Project Pages
			else if (position > 0 && position < allProject.Count + 1) 
			{
				// Load Data
				List<Post> data;

				//Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				data = filterpost (allProject [position - 1].name, allPost);
				addTab ("Info", new ProjectInfoFragment (allProject [position - 1]));
				addTab ("Dashboard", new PostFragment (data));
				addTab ("Teammate", new PeopleFragment (allProject [position - 1].teamMember));
				Title = allProject [position - 1].name;
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// People Screen
			else if (position == allProject.Count + 1)
			{
				// Load Data
				allPeople = LoadPeopleData("ALL");
				partialPeople = LoadPeopleData("PARTIAL");

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All people", new PeopleFragment (allPeople));
				addTab ("Your Teammates", new PeopleFragment (partialPeople));
				Title = "People";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Client Screen
			else if (position == allProject.Count + 2) 
			{
				// Load Data
				allClient = LoadClientData("ALL");
				partialClient = LoadClientData("PARTIAL");

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Clients", new ClientFragment (allClient));
				addTab ("Your Clients", new ClientFragment (partialClient));
				Title = "Clients";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Project Screen
			else if (position == allProject.Count + 3) 
			{
				// Load Data
				allPost = LoadDashProjectData();

				// Update User Interface
				ActionBar.RemoveAllTabs ();
				this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
				addTab ("All Project", new ProjectFragment (allProject,allPost));
				addTab ("Your Project", new ProjectFragment (allProject,allPost));
				Title = "Projects";
				mDrawerLayout.CloseDrawer (mDrawerList);
			}
			// Logout
			else if (position == allProject.Count + 4) 
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

		public List<Post> filterpost (string projectname, List<Post> allpost)
		{
			List<Post> result = new List<Post>{};
			foreach (Post item in allpost){
				if (item.project == projectname)
					result.Add (item);
			}
			return result;
		}

        private List<Client> LoadClientData(string arg)
        {
			List<Client> clientList = new List<Client>();
            var jl = new JsonLoader();
            JsonValue data = jl.LoadData(this, "clients.json");
            var list = (JsonArray) data["client_list"];

            foreach (JsonObject j in list)
            {
                // Two cases, either all will be let through, or only those that are your client (FULL/PARTIAL)
                if (arg.Equals("ALL") || j["your_client"])
                {
                    clientList.Add(new Client(j["id"], null, j["company"], j["contact_name"], j["contact_email"]));
                }
            }

			List<Client> orderedClientList = clientList.OrderBy(x => x.name).ToList();

            return orderedClientList;
        }

		private List<Employee> LoadPeopleData(string arg)
		{
			List<Employee> peopleList = new List<Employee>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "people.json");
			var list = (JsonArray) data["people_list"];

			foreach (JsonObject j in list)
			{
				// Two cases, either all will be let through, or only those that are your team member (FULL/PARTIAL)
				if (arg.Equals("ALL") || j["your_people"])
				{
                    peopleList.Add(new Employee(j["id"], null, j["name"], j["avail"], j["technology"]));
				}
			}

			List<Employee> orderedPeopleList = peopleList.OrderBy(x => x.name).ToList();

            return orderedPeopleList;
		}
			

		private List<Post> LoadDashProjectData()
		{
			List<Post> post = new List<Post>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "myprojects.json");
			var list = (JsonArray) data["myprojectsdash"];

			foreach (JsonObject j in list)
			{
				post.Add(new Post(j["id"], j["photo"], j["content"], j["name"], j["projectname"], j["companyname"], j["date"]));
			}

			// Should order list by time stamp, but currently it is by name
			List<Post> orderedPostList = post.OrderBy(x => x.name).ToList();

			return orderedPostList;
		}

		// Generate Project data
		private List<Project> LoadProjectData()
		{

			List<Project> projects = new List<Project>();
			var jl = new JsonLoader();
			JsonValue data = jl.LoadData(this, "myprojects.json");
			var list = (JsonArray) data["myprojectsside"];

			foreach (JsonObject j in list)
			{
				// Generate team member list by querying each employee
				var teamMembers = new List<Employee>();
				foreach (JsonValue member in j["team_member"])
				{
					teamMembers.Add(FindEmployee(member));
				}

				var technologies = new List<string>();
				foreach (JsonValue technology in j["technology"])
				{
					technologies.Add(technology);
				}

				projects.Add(new Project(j["id"], j["projectname"], j["comp"], j["start"], j["end"], teamMembers.OrderBy(x => x.name).ToList(), technologies.OrderBy(x => x).ToList(), j["description"]));
			}

			List<Project> orderedProjectList = projects.OrderBy(x => x.name).ToList();

			return orderedProjectList;
		}

		// Helper function to load the employee associated with a project
		private Employee FindEmployee(string name)
		{
			List<Employee> employeeList = LoadPeopleData("ALL");
			foreach (Employee e in employeeList)
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


