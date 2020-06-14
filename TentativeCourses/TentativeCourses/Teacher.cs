using System;
using System.Collections.Generic;
using System.Text;

namespace TentativeCourses
{
    public class Teacher
    {
        public List<Schedule> Days { get; set; }
        public Teacher(List<Schedule> _days)
        {
            Days = _days;
        }
    }
}
