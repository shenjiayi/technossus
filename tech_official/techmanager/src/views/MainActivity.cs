using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Json;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
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
            JsonValue data = jl.LoadData(this, "login2.json");
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


//          JsonArray un = (JsonArray) obj ["username"];
//          JsonArray pw = (JsonArray)obj ["password"];


//          //Thread.Sleep (3000);
//          for(int i = 0; i < un.Count(); i++){
//              if (username == un.ElementAt (i)) {
//                  if (password == pw.ElementAt (i)) {
//                      test = false;
//                      Intent intent = new Intent (this, typeof(NavigationDrawerActivity));
//                      this.StartActivity (intent);
//                  } 
//              }       //test = false;
//          } 
//
//          if (test == true) {
//              Toast.MakeText (this, "Invalid account! Please try again.", ToastLength.Long).Show ();
//          }
            //FragmentTransaction transaction = FragmentManager.BeginTransaction ();
            //test simplepage = new test ();
            //simplepage.Show (transaction, "hello world");

		}
	}
}
