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

        /// <summary>Get a list of teachers and schedules for schedules that match with the student.
        /// <paramref name="teachers"/>
        /// </summary>
        internal List<(Teacher, Schedule)> GetPossiblesTeachers(List<Teacher> teachers)
        {
            List<(Teacher, Schedule)> possiblesTeacher = new List<(Teacher, Schedule)>();
            foreach (Teacher t in teachers)
            {
                foreach (Schedule d in t.Days)
                {
                    if (Days.Any(day => day.isSameMoment(d)))
                    {
                        possiblesTeacher.Add((t, d));
                    }
                }
            }
            return possiblesTeacher;
        }
    }
}
