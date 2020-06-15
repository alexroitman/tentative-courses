using System;
using System.Collections.Generic;
using System.Text;

namespace TentativeCourses
{
    public class Schedule
    {
        public TimeSpan Time { get; set; }

        public DayOfWeek Day { get; set; }
        public Schedule(DayOfWeek _day, TimeSpan _time)
        {
            if (_day == DayOfWeek.Saturday || _day == DayOfWeek.Sunday || (_time.Hours < 9 && _time.Hours > 19))
            {
                throw new ArgumentOutOfRangeException();
            }
            Time = _time;
            Day = _day;
        }
        public bool isSameMoment(Schedule day)
        {
            return Day == day.Day && Time.Hours == day.Time.Hours && Time.Minutes == day.Time.Minutes && Time.Seconds == day.Time.Seconds;
        }

        internal bool DiffersOneHour(Schedule day)
        {
            return Day == day.Day && (Time.Hours == day.Time.Hours - 1 || Time.Hours == day.Time.Hours + 1) && Time.Minutes == day.Time.Minutes && Time.Seconds == day.Time.Seconds;
        }
    }
}
