using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestCoach
{
   public class Notification
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Boolean Seen { get; set; }
        public String Type { get; set; }
        public String ImagePath { get; set; }

        //advices later will be parsed from webservice
       
       


        public Notification() { }
        public Notification(String type, String titre ,  String desc) {

            Title = titre;
            Type = type;
            Description = desc;

        }


      

    }

    

}
