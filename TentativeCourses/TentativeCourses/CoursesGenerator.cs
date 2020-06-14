using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace TentativeCourses
{

    public class CoursesGenerator
    {
        public static ResultCourseGenerator Generate(List<Student> students, List<Teacher> teachers)
        {
            List<Course> courses = new List<Course>();
            List<Student> studentsNoAssign = new List<Student>();
            foreach (Student s in students)
            {
                List<(Teacher, Schedule)> possiblesTeachers = s.GetPossiblesTeachers(teachers);
                if (possiblesTeachers.Any())
                {
                    List<Course> possibleCourses = GetPossibleCourse(courses, possiblesTeachers);
                    
                    if (possibleCourses.Any())
                    {
                        possibleCourses= possibleCourses.Where(x => Validate(x, s)).ToList();//this filter just the course with  the course conditions
                        if (possibleCourses.Any())
                        {
                            possibleCourses.First().AddStudent(s);
                            
                        }
                        else
                        {//if there are any course that satisfy all the contitions
                            studentsNoAssign.Add(s);
                        }
                    }
                    else
                    {
                        studentsNoAssign.Add(s);
                    }
                }
                else//if there are any schedule for this student
                {
                    studentsNoAssign.Add(s);
                }
            }
            return new ResultCourseGenerator() { courses = courses, rejected = studentsNoAssign };
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
