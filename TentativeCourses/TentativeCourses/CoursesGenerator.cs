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
            foreach (Student student in students)
            {
                List<(Teacher, Schedule)> possibleTeachers = GetTeachersWithStudentSchedules(teachers, student);
                if (possibleTeachers.Any())
                {
                    List<Course> possibleCourses = GetPossibleCourse(courses, possibleTeachers);
                    Course course = possibleCourses.FirstOrDefault(oneCourse => Validate(oneCourse, student));//this filter just the course with  the course conditions
                    if (course != null)
                    {
                        course.AddStudent(student);
                    }
                    else//if there are any course that satisfy all the contitions
                    {

                        unassignedStudent.Add(student);
                    }
                }
                else//if there are any schedule for this student
                {
                    unassignedStudent.Add(student);
                }
            }
            return new CourseResultGenerator() { Courses = courses,Rejected = unassignedStudent };
        }
        /// <summary>Get a list of teachers and schedules for schedules that match with the student.
        /// <paramref name="teachers"/>
        /// <paramref name="student"/>
        /// </summary>
        private static List<(Teacher, Schedule)> GetTeachersWithStudentSchedules(List<Teacher> teachers, Student student)
        {
            List<(Teacher, Schedule)> possibleTeachers = teachers.SelectMany(teacher => student.Days.Select(day => (Teacher: teacher, Schedule: day))).ToList();
            possibleTeachers = possibleTeachers.Where(tuple => student.Days.Any(day => day.isSameMoment(tuple.Item2))).ToList();
            return possibleTeachers;
        }

        private static bool Validate(Course course, Student s)
        {
            return course.Students.Count < 6 && (course.Modality == Modality.NonAssigned || course.Modality == s.Modality) && (course.Level == LevelOfKnoweldge.NonAssigned || course.Level == s.Level);
        }


        /// <summary>
        /// Get (and create if necessary) the list of the possible existent courses that fit in the teacher schedule
        /// </summary>
        /// <param name="courses"></param>
        /// <param name="teachersInDays"></param>
        /// <returns></returns>
        private static List<Course> GetPossibleCourse(List<Course> courses, List<(Teacher, Schedule)> teachersInDays)
        {
            List<Course> possiblesCourses = new List<Course>();

            foreach ((Teacher, Schedule) teacherInDay in teachersInDays)
            {
                Course c = courses.Where(course => course.Teacher == teacherInDay.Item1 && course.Day.isSameMoment(teacherInDay.Item2)).FirstOrDefault();
                if (c == null)
                {
                    c = new Course(teacherInDay.Item1, new List<Student>(), teacherInDay.Item2);
                    courses.Add(c);
                }
                possiblesCourses.Add(c);
            }
            return possiblesCourses;
        }
    }
}
