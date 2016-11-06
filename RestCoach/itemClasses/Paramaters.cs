using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestCoach
{
    public class Paramaters
    {
      

        public static int WorkMin { get; set; }
        public static int WorkHours { get; set; }
        public static int WorkSeconds { get; set; }


        public static int PrefWorkMin { get; set; }
        public static int PrefWorkHour { get; set; }

        public static int PrefSnoozeMinutes { get; set; }
        public static int PrefSnoozeHours { get; set; }

        public static Boolean WorkingState { get; set; }
        public static bool initialized=false;

        public static void savePreferencesData()
        {
            IsolatedStorageHelper.SaveObject("PrefWorkHour", Paramaters.PrefWorkHour);
            IsolatedStorageHelper.SaveObject("PrefWorkMin", Paramaters.PrefWorkMin);
            IsolatedStorageHelper.SaveObject("PrefSnoozeHours", Paramaters.PrefSnoozeHours);
            IsolatedStorageHelper.SaveObject("PrefSnoozeMinutes", Paramaters.PrefSnoozeMinutes);
            IsolatedStorageHelper.SaveObject("initialized", Paramaters.initialized);
        }
        public static void loadPreferencesData()
        {
            PrefWorkMin= IsolatedStorageHelper.GetObject<int>("PrefWorkMin");
            PrefWorkHour= IsolatedStorageHelper.GetObject<int>("PrefWorkHour");

            PrefSnoozeMinutes= IsolatedStorageHelper.GetObject<int>("PrefSnoozeMinutes");
            PrefSnoozeHours =IsolatedStorageHelper.GetObject<int>("PrefSnoozeHours");

            initialized = true;

       


        }
        public static void saveTime(int hours , int minutes,int seconds)
        {
            IsolatedStorageHelper.SaveObject("WorkHours", hours);
            IsolatedStorageHelper.SaveObject("WorkMin", minutes);
            IsolatedStorageHelper.SaveObject("WorkSec", minutes);

        }
        public static void loadTime() {
            WorkSeconds = IsolatedStorageHelper.GetObject<int>("WorkSec");
            WorkMin = IsolatedStorageHelper.GetObject<int>("WorkMin");
            WorkHours = IsolatedStorageHelper.GetObject<int>("WorkHours");
        }



    }
}

