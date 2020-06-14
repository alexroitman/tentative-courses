using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using TentativeCourses;

namespace TentativeCoursesTest
{
    [TestClass]
    public class TentativeCoursesTest
    {
        [TestMethod]
        public void OneStudentIndividualForOneTeacher()
        {
            List<Schedule> MoWeFri17 = new List<Schedule>();
            MoWeFri17.Add(new Schedule(DayOfWeek.Monday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Wednesday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Friday, new TimeSpan(17, 0, 0)));
            Teacher teacher = new Teacher(MoWeFri17);
            List<Teacher> teacherList = new List<Teacher>();
            teacherList.Add(teacher);




            Schedule monday17 = new Schedule(System.DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Student student = new Student(Modality.Individual,LevelOfKnoweldge.Beginner,monday17);
            List<Student> studentList = new List<Student>();
            studentList.Add(student);


           // Course myCourse = new Course(teacher,new List<Student>().Add( student), monday17);
            Assert.AreEqual(teacher, CoursesGenerator.Generate(studentList,teacherList).courses.First().Teacher);
            Assert.AreEqual(student, CoursesGenerator.Generate(studentList,teacherList).courses.First().Students.First());
            
            

        }
        [TestMethod]
        public void CompleteCourseOneRejected()
        {
            List<Schedule> MoWeFri17 = new List<Schedule>();
            MoWeFri17.Add(new Schedule(DayOfWeek.Monday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Wednesday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Friday, new TimeSpan(17, 0, 0)));
            Teacher teacher = new Teacher(MoWeFri17);
            List<Teacher> teacherList = new List<Teacher>();
            teacherList.Add(teacher);




            Schedule monday17 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Student student1 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student2 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student3 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student4 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student5 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student6 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student7 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);

            List<Student> studentList = new List<Student>();
            studentList.Add(student1);
            studentList.Add(student2);
            studentList.Add(student3);
            studentList.Add(student4);
            studentList.Add(student5);
            studentList.Add(student6);
            studentList.Add(student7);


            // Course myCourse = new Course(teacher,new List<Student>().Add( student), monday17);
            Assert.AreEqual(6, CoursesGenerator.Generate(studentList, teacherList).courses.First().Students.Count);
            Assert.AreEqual(1, CoursesGenerator.Generate(studentList, teacherList).rejected.Count);

        }



        [TestMethod]
        public void CourseMixedLevelShoudHaveOnlyIntermediate()
        {
            List<Schedule> MoWeFri17 = new List<Schedule>();
            MoWeFri17.Add(new Schedule(DayOfWeek.Monday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Wednesday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Friday, new TimeSpan(17, 0, 0)));
            Teacher teacher = new Teacher(MoWeFri17);
            List<Teacher> teacherList = new List<Teacher>();
            teacherList.Add(teacher);




            Schedule monday17 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Student student1 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student2 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student3 = new Student(Modality.Group, LevelOfKnoweldge.Advanced, monday17);
            Student student4 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student5 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student6 = new Student(Modality.Group, LevelOfKnoweldge.Advanced, monday17);

            List<Student> studentList = new List<Student>();
            studentList.Add(student1);
            studentList.Add(student2);
            studentList.Add(student3);
            studentList.Add(student4);
            studentList.Add(student5);
            studentList.Add(student6);


            // Course myCourse = new Course(teacher,new List<Student>().Add( student), monday17);
            Assert.AreEqual(3, CoursesGenerator.Generate(studentList, teacherList).courses.First().Students.Count);
            Assert.AreEqual(LevelOfKnoweldge.Intermediate, CoursesGenerator.Generate(studentList, teacherList).courses.First().Level);
        }

        [TestMethod]
        public void SecondAlternativeForCompleteCourse()
        {
            List<Schedule> MoWeFri17 = new List<Schedule>();
            MoWeFri17.Add(new Schedule(DayOfWeek.Monday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Wednesday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Friday, new TimeSpan(17, 0, 0)));
            Teacher teacher = new Teacher(MoWeFri17);
            List<Teacher> teacherList = new List<Teacher>();
            teacherList.Add(teacher);




            Schedule monday17 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Schedule wednesday17 = new Schedule(DayOfWeek.Wednesday, new System.TimeSpan(17, 0, 0));
            List<Schedule> studentSchedule = new List<Schedule>();
            studentSchedule.Add(monday17);
            studentSchedule.Add(wednesday17);

            Student student1 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student2 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student3 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student4 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student5 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student6 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, monday17);
            Student student7 = new Student(Modality.Group, LevelOfKnoweldge.Intermediate, studentSchedule);

            List<Student> studentList = new List<Student>();
            studentList.Add(student1);
            studentList.Add(student2);
            studentList.Add(student3);
            studentList.Add(student4);
            studentList.Add(student5);
            studentList.Add(student6);
            studentList.Add(student7);


            // Course myCourse = new Course(teacher,new List<Student>().Add( student), monday17);
            Assert.AreEqual(6, CoursesGenerator.Generate(studentList, teacherList).courses.First().Students.Count);
            Assert.AreEqual(2, CoursesGenerator.Generate(studentList, teacherList).courses.Count);
            Assert.AreEqual(0, CoursesGenerator.Generate(studentList, teacherList).rejected.Count);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInvalidDate()
        {
            new Schedule(DayOfWeek.Sunday, new TimeSpan(17, 0, 0));
        }
    }
}
