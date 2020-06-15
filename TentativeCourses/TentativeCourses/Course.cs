using System;
using System.Collections.Generic;
using System.Linq;

namespace TentativeCourses
{
    public class Course
    {
        public Teacher Teacher { get; set; }
        public List<StudentInCourse> Students{ get; set; }
        public Schedule Day{ get; set; }
        public Modality Modality { get; set; }
        public LevelOfKnoweldge Level { get; set; }

        public Course(Teacher _teacher, List<StudentInCourse> _student, Schedule _day)
        {
            Teacher = _teacher;
            Students = _student;
            Day = _day;
            Modality = Modality.NonAssigned;
            Level = LevelOfKnoweldge.NonAssigned;
        }
        public void ChangeModality(Modality m)
        {
            Modality = m;
        }
        public void ChangeLevel(LevelOfKnoweldge l)
        {
            Level= l;
        }

        internal void AddStudent(Student s)
        {
            ChangeLevel(s.Level);
            ChangeModality(s.Modality);
            bool isConfirm = s.Days.Any(day => day.isSameMoment(Day));
            Students.Add(new StudentInCourse(s,isConfirm));

        }


    }
}