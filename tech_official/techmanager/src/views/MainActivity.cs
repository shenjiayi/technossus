using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace NavigationDrawer
{

	[Activity (Label = "Login", MainLauncher = true)]
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
			//the account is valid
			string username = TxtUsername.Text;
			string password = TxtPassword.Text;
			BtnLogin.Text = "Logging";
			ActLIikeRequest (username, password);
		}

		private void ActLIikeRequest(string username, string password)
		{
			//Thread.Sleep (3000);
			if (username == "tech") {
				Intent intent = new Intent (this, typeof(NavigationDrawerActivity));
				this.StartActivity (intent);
			} else {
				FragmentTransaction transaction = FragmentManager.BeginTransaction ();
				test simplepage = new test();
				simplepage.Show (transaction, "hello world");
			}
		}

	}
}
