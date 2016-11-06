using System;

namespace RestCoach
{
    public class WorkSession
    {
   
        public int Id { get; set; }

        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int TimeLeftInMinutes { get; set; }
        public int percentageOfWork { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }


        public bool Stressed { get; set; }
        public bool Tired { get; set; }
        public bool Comfortable { get; set; }
        public bool Noise { get; set; }
        public bool OnDesk { get; set; }
        public int overallState { get; set; }

        public string stressImage { get; set; }
        public string tiredImage { get; set; }
        public string comforImage { get; set; }
        public string noiseImage { get; set; }
        public string deskImage { get; set; }

    }
}
