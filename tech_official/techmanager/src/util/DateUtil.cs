using System;
using System.Text.RegularExpressions;

namespace NavigationDrawer
{
    public class DateUtil
    {
        // Validating Regex for date.  Based on format in convertToDateTime method
        private readonly static string dateRegex = "(?<year>\\d{1,4})\\/(?<month>\\d{1,2})\\/(?<day>\\d{1,2})";

        private DateUtil()
        {
            // Do not instantiate this class.  Only to be used for static methods.
        }

        // Given in the format Year/Month/Day. Ex: 2015/1/20 is January 20, 2015
        public static DateTime convertToDateTime(string s)
        {
            // Check string versus valid regex
            Regex r = new Regex(dateRegex);
            var match = r.Match(s);

            if (match.Success)
            {
                // Can easily rearrange the way we interpret or parse the date, just change the assignments
                //string[] dateArray = s.Split('/');
                string year = match.Groups["year"].Value;
                string month = match.Groups["month"].Value;
                string day = match.Groups["day"].Value;

                DateTime dateObject = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                return dateObject;
            }
            else
            {
                // This should really throw an error if did not appropriately match
                return new DateTime(1, 1, 1);
            }
        }

        public static bool isDuringMonth(DateTime date, string month)
        {
            string dateMonth = date.Date.ToString("MMM");
            if (month.ToLower().Contains(dateMonth.ToLower()))
            {
                return true;
            }
            else
                return false;
        }
    }
}

