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
        List<Teacher> teacherList = new List<Teacher>();
        Teacher teacher;
        [TestInitialize]
        public void Setup()
        {
            List<Schedule> MoWeFri17 = new List<Schedule>();
            MoWeFri17.Add(new Schedule(DayOfWeek.Monday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Wednesday, new TimeSpan(17, 0, 0)));
            MoWeFri17.Add(new Schedule(DayOfWeek.Friday, new TimeSpan(17, 0, 0)));
            teacher = new Teacher(MoWeFri17);

            teacherList.Add(teacher);
        }

        [TestMethod]
        public void OneStudentIndividualForOneTeacher()
        {
            Schedule monday17 = new Schedule(System.DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Student student = new Student(Modality.Individual, LevelOfKnoweldge.Beginner, monday17);
            List<Student> studentList = new List<Student>();
            studentList.Add(student);

            CourseResultGenerator result = CoursesGenerator.Generate(studentList, teacherList);

            Assert.AreEqual(teacher, result.Courses.First().Teacher);
            Assert.AreEqual(student, result.Courses.First().Students.Select(s=>s.Student).First());



        }
        [TestMethod]
        public void CompleteCourseOneRejected()
        {
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

            CourseResultGenerator result = CoursesGenerator.Generate(studentList, teacherList);

            Assert.AreEqual(6, result.Courses.First().Students.Count);
            Assert.AreEqual(1, result.Rejected.Count);

        }



        [TestMethod]
        public void CourseMixedLevelShoudHaveOnlyIntermediate()
        {
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

           CourseResultGenerator result= CoursesGenerator.Generate(studentList, teacherList);
            Assert.AreEqual(3, result.Courses.First().Students.Count);
            Assert.AreEqual(LevelOfKnoweldge.Intermediate, result.Courses.First().Level);
            Assert.IsTrue(result.Courses.First().Students.All(student=>student.Student.Level== LevelOfKnoweldge.Intermediate));
        }

        [TestMethod]
        public void SecondAlternativeForCompleteCourse()
        {
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

            CourseResultGenerator result = CoursesGenerator.Generate(studentList, teacherList);
            Assert.AreEqual(6, result.Courses.First().Students.Count);
            Assert.AreEqual(2, result.Courses.Count);
            Assert.AreEqual(0, result.Rejected.Count);
        }


        [TestMethod]
        public void DiferentHoursStudentsNoConfirm()
        {
            Schedule monday16 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(16, 0, 0));
            Schedule monday17 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(17, 0, 0));
            Schedule monday18 = new Schedule(DayOfWeek.Monday, new System.TimeSpan(18, 0, 0));


            Student student1 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday17);
            Student student2 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday16);
            Student student3 = new Student(Modality.Group, LevelOfKnoweldge.Beginner, monday18);


            List<Student> studentList = new List<Student>();
            studentList.Add(student1);
            studentList.Add(student2);
            studentList.Add(student3);

            CourseResultGenerator result = CoursesGenerator.Generate(studentList, teacherList);

            Assert.AreEqual(3, result.Courses.First().Students.Count);
            Assert.AreEqual(2, result.Courses.First().Students.Where(s=>s.isConfirmed==false).ToList().Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInvalidDate()
        {
            new Schedule(DayOfWeek.Sunday, new TimeSpan(17, 0, 0));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInvalidTime()
        {
            new Schedule(DayOfWeek.Sunday, new TimeSpan(20, 0, 0));
        }
    }
}
