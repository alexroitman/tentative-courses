using System;
using System.Collections.Generic;

namespace TentativeCourses
{
    public class Course
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students{ get; set; }
        public Schedule Day{ get; set; }
        public Modality Modality { get; set; }
        public LevelOfKnoweldge Level { get; set; }

        public Course(Teacher _teacher, List<Student> _student, Schedule _day)
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
            Students.Add(s);

        }


    }
}