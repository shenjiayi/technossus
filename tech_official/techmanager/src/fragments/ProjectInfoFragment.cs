
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
		project projectinfo;

		public ProjectInfoFragment(project data){
			projectinfo = data;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
    		View rootView = inflater.Inflate (Resource.Layout.ProjectInfo, container, false);
//    		List <employee> teamMember = new List<employee> () {
//    			new employee (0, null, "Jone", "2014/3/2","Java"),
//    			new employee (0, null, "James", "2014/4/6","Java"),
//    			new employee (0, null, "Kate", "2015/3/1","Java")
//    		};
//
//    		List <string> technology = new List<string> { "java", "c#", "html" };
//
//    		project project1 = new project (0, "Web Design", "Technossus","2014/02/24", "2015/07/23", teamMember, technology,"Design a website");




    		TextView ClientName = rootView.FindViewById<TextView> (Resource.Id.ClientName);
    		TextView ContactEmail = rootView.FindViewById<TextView> (Resource.Id.ContactEmail);
    		TextView StartData = rootView.FindViewById<TextView> (Resource.Id.StartData);
    		TextView EndData = rootView.FindViewById<TextView> (Resource.Id.EndData);
            TextView DaysLeft = rootView.FindViewById<TextView>(Resource.Id.DaysLeft);
    		TextView Technology = rootView.FindViewById<TextView> (Resource.Id.Technology);
    		TextView Desciption = rootView.FindViewById<TextView> (Resource.Id.Desciption);

			ClientName.Text = "Client Name: "+ projectinfo.client;
			StartData.Text = "Start Date: "+ projectinfo.startDate.Date.ToString("d");
			EndData.Text = "End Date: "+ projectinfo.endDate.Date.ToString("d");
			DaysLeft.Text = "Days Left: " + (projectinfo.endDate - DateTime.Today).TotalDays;
			Technology.Text = "Technology:\n" + computeTechnologyString(projectinfo);
			Desciption.Text = "Desciption:\n" + projectinfo.description;

    		return rootView;
    	}

        private string computeTechnologyString(project p)
        {
            if (p == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < p.technology.Count; i++)
            {
                sb.Append(p.technology[i]);
                if (i < p.technology.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }
	}
}



