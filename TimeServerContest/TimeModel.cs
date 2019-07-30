using System;

namespace TimeServerContest
{
    public class TimeModel
    {
        public DateTime Time { get; set; } 
        public TimeModel()
        {
            Time = DateTime.UtcNow;
        }
    }
}