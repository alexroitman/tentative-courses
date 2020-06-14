using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TentativeCourses
{
    public class Student
    {
        public Modality Modality { get; set; }
        public LevelOfKnoweldge Level { get; set; }
        public List<Schedule> Days { get; set; }
        public Student(Modality _modality,LevelOfKnoweldge _level,List<Schedule> _days)
        {
            Modality = _modality;
            Level = _level;
            Days = _days;
        }

        

        public Student(Modality _modality, LevelOfKnoweldge _level, Schedule _day)
        {
            Modality = _modality;
            Level = _level;
            Days = new List<Schedule>();
            Days.Add( _day);
        }
    }
}
