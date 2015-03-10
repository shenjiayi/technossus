using System;
using System.Json;
using Android.App;

namespace NavigationDrawer
{
    // Created this class to make it more intuitive to load json files.
    public class JsonLoader
    {

        public JsonLoader()
        {
        }

        public JsonValue LoadData(Activity a, string fileName)
        {
            System.IO.StreamReader strm = new System.IO.StreamReader (a.Assets.Open(fileName));
            string response = strm.ReadToEnd ();
            return JsonValue.Parse (response);
        }

    }
}

