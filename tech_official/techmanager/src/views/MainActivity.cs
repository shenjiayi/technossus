using System;
using System.Json;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;


namespace NavigationDrawer
{

	[Activity (Label = "Technossus", MainLauncher = true)]
	public class MainActivity : Activity
	{

		private Button BtnLogin;
		private EditText TxtUsername;
		private EditText TxtPassword;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			TxtUsername = FindViewById<EditText> (Resource.Id.InputUsername);
			TxtPassword = FindViewById<EditText> (Resource.Id.InputPassword);
			BtnLogin = FindViewById<Button> (Resource.Id.BtnLogin);
			BtnLogin.Click += BtnLogin_Click; 

		}

		void BtnLogin_Click(object sender,EventArgs e)
		{
			string username = TxtUsername.Text;
			string password = TxtPassword.Text;
			BtnLogin.Text = "Logging";
			ActLikeRequest (username, password);
		}

		private void ActLikeRequest(string username, string password)
		{
			bool invalidRequest = true;
            var jl = new JsonLoader();
            JsonValue data = jl.LoadData(this, "login.json");
            var list = (JsonArray) data["valid_users"];

            foreach (JsonObject j in list)
            {
                string un = j["username"];
                string pw = j["password"];
                
                if (username.Equals(un) && password.Equals(pw))
                {
                    invalidRequest = false;
                    Intent intent = new Intent(this, typeof(NavigationDrawerActivity));
                    this.StartActivity(intent);
                }
            }
            
            if (invalidRequest)
            {
                Toast.MakeText(this, "Invalid account! Please try again.", ToastLength.Long).Show();
            }

		}
	}
}
