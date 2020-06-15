using System;
using System.Collections.Generic;
using System.Text;

namespace TentativeCourses
{
    public class StudentInCourse
    {
        public StudentInCourse(Student student, bool isConfirmed=true)
        {
            Student = student;
            this.isConfirmed = isConfirmed;
        }

        public Student Student { get; set; }
        public bool isConfirmed { get; set; }
    }
}
