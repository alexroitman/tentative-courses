using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TentativeCourses
{

    public class CoursesGenerator
    {
        public static CourseResultGenerator Generate(List<Student> students, List<Teacher> teachers)
        {
            List<Course> courses = new List<Course>();
            List<Student> unassignedStudent = new List<Student>();
            List<Course> possibleCourses = GeneratePossibleCourses(teachers);
            foreach (Student student in students)
            {
                Course course = possibleCourses.FirstOrDefault(oneCourse => Validate(oneCourse, student));//this filter just the course with  the course conditions
                if (course != null)
                {
                    course.AddStudent(student);
                }
                else//if there are any schedule for this student
                {

                    unassignedStudent.Add(student);
                }
            }
            courses = possibleCourses.Where(course => course.Students.Count != 0).ToList();
            return new CourseResultGenerator() { Courses = courses, Rejected = unassignedStudent };
        }
        /// <summary>Get a list of teachers and schedules for schedules that match with the student.
        /// <paramref name="teachers"/>
        /// <paramref name="student"/>
        /// </summary>
        private static List<Course> GeneratePossibleCourses(List<Teacher> teachers)
        {
            return teachers.SelectMany(teacher => teacher.Days.Select(day => new Course(teacher, new List<StudentInCourse>(), day))).ToList();
        }

        private static bool Validate(Course course, Student student)
        {
            return student.Days.Any(day => (day.isSameMoment(course.Day)) || day.DiffersOneHour(course.Day)) && course.Students.Count < 6 && (course.Modality == Modality.NonAssigned || course.Modality == student.Modality) && (course.Level == LevelOfKnoweldge.NonAssigned || course.Level == student.Level);
        }


        
    }
}
