using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Identity_API.Helper
{
    public class TimeZones
    {
        public static string GetCurrentDatTime()
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 

            //timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");

            //Get date and time in US Mountain Standard Time 
            //  dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //Print out the date and time

            // TimeZoneInfo easternStandardTime;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
                dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                return dateTime.ToString("MM/dd/yyyy HH:mm:ss");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dhaka");
                dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                return dateTime.ToString("MM/dd/yyyy HH:mm:ss");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "I don't know how to do a lookup on a Mac TimeZone";
            }
            else
            {
                return "I don't know how to do a lookup TimeZone";
            }


        }
    }
}
