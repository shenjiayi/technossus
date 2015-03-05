
using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


//Ambiguities
using Fragment = Android.App.Fragment;
using System.Collections.Generic;



namespace NavigationDrawer
{
	public class ProjectInfoFragment : Fragment
	{
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
		View rootView = inflater.Inflate (Resource.Layout.ProjectInfo, container, false);
		List <employee> teamMember = new List<employee> () {
			new employee (0, null, "Jone", "2014/3/2","Java"),
			new employee (0, null, "James", "2014/4/6","Java"),
			new employee (0, null, "Kate", "2015/3/1","Java")
		};
		List <string> technology = new List<string> { "java", "c#", "html" };


		project project1 = new project (0, "Web Design", "Technossus","2014/02/24", "2015/07/23", teamMember, technology,"Design a website");


		TextView ClientName = rootView.FindViewById<TextView> (Resource.Id.ClientName);
		TextView ContactEmail = rootView.FindViewById<TextView> (Resource.Id.ContactEmail);
		TextView StartData = rootView.FindViewById<TextView> (Resource.Id.StartData);
		TextView EndData = rootView.FindViewById<TextView> (Resource.Id.EndData);
		TextView Technology = rootView.FindViewById<TextView> (Resource.Id.Technology);
		TextView Desciption = rootView.FindViewById<TextView> (Resource.Id.Desciption);

		ClientName.Text = "Client Name: "+ project1.client;
		StartData.Text = "Start Data: "+ project1.startDate;
		EndData.Text = "End Data: "+ project1.endDate;
		Desciption.Text = "Desciption: " + project1.description;

		return rootView;
		}


	}
}



