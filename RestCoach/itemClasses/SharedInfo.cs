using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestCoach
{
    class SharedInfo
    {



        public static List<String> advicesALL = new List<String> { "No one knows anyone else in the way you do.", "When you're at a concert or event, enjoy the moment, enjoy being there. Try leaving your camera in your pocket.", "Always bet on black.", "If you are feeling down, try holding a pencil between your top lip and your nose for five minutes.", "Life is better when you sing about bananas.", "When hugging, hug with both arms and apply reasonable, affectionate pressure.", "Measure twice, cut once.", "You have as many hours in a day as the people you admire most.", "Everybody makes mistakes.", "Accept advice.", "Build something out of LEGO.", "A common regret in life is wishing one had the courage to be ones true self.", "Don't take life too seriously.", "Never run with scissors.", "Never run with scissors.", "If you cannot unscrew the lid of a jar, try placing a rubber band around its circumference for extra grip.", "Winter is coming.", "When in doubt, just take the next small step." };
        public static List<String> advices = new List<String> { "" };


        public static List<Notification> myNotifications = new List<Notification>();
        public static List<WorkSession> myWorkSessions = new List<WorkSession>();

        public static void saveSessions()
        {
            try
            { IsolatedStorageHelper.SaveObject("WorkSessions", myWorkSessions);

            }
            catch (Exception) { }
        }
        public static void loadSessions()
        {
            try
            {
               myWorkSessions = IsolatedStorageHelper.GetObject<List<WorkSession>>("WorkSessions");

            }
            catch (Exception) { }

        }

    }

}
