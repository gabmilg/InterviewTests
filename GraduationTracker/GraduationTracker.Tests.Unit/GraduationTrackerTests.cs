using System;
using GraduationTracker.Dal;
using GraduationTracker.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void Test_Has_Credits()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var students = new[]
            {
               new Student
               {
                   Id = 1,
                   Courses = new []
                   {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new []
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
               },
            new Student
            {
                Id = 3,
                Courses = new []
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                }
            },
            new Student
            {
                Id = 4,
                Courses = new []
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                }
            }
        };

            var graduated = students.Select(student => tracker.HasGraduated(diploma, student).Item1).ToList();

            Assert.IsFalse(graduated.Any(c => c));
        }

        [TestMethod]
        public void Test_Has_AllRequirement()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=100 ,Credits = 1},
                        new Course{Id = 2, Name = "Science", Mark=100 ,Credits = 1},
                        new Course{Id = 3, Name = "Literature", Mark=100 , Credits = 1},
                        new Course{Id = 4, Name = "Physichal Education", Mark=100, Credits = 1}
                    }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsTrue(result.Item1);
        }

        [TestMethod]
        public void Test_Failed_When_Dont_Has_All_Requirements()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 ,Credits = 1},
                    new Course{Id = 2, Name = "Science", Mark=100 ,Credits = 1},
                    new Course{Id = 3, Name = "Literature", Mark=100 , Credits = 1},
                }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void Test_Remedial_Standing_When_Average_Below_50()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=20 ,Credits = 1},
                    new Course{Id = 2, Name = "Science", Mark=50 ,Credits = 1},
                    new Course{Id = 3, Name = "Literature", Mark=80 , Credits = 1},
                    new Course{Id = 4, Name = "Physichal Education", Mark=30, Credits = 1}
                }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == Standing.Remedial);
        }

        [TestMethod]
        public void Test_Average_Standing_When_Average_Below_80_And_Above_50()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 ,Credits = 1},
                    new Course{Id = 2, Name = "Science", Mark=80 ,Credits = 1},
                    new Course{Id = 3, Name = "Literature", Mark=70 , Credits = 1},
                    new Course{Id = 4, Name = "Physichal Education", Mark=69, Credits = 1}
                }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsTrue(result.Item1);
            Assert.IsTrue(result.Item2 == Standing.Average);
        }

        [TestMethod]
        public void Test_SumaCumLaude_Standing_When_Average_Below_95_And_Above_80()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 ,Credits = 1},
                    new Course{Id = 2, Name = "Science", Mark=100 ,Credits = 1},
                    new Course{Id = 3, Name = "Literature", Mark=90 , Credits = 1},
                    new Course{Id = 4, Name = "Physichal Education", Mark=80, Credits = 1}
                }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsTrue(result.Item1);
            Assert.IsTrue(result.Item2 == Standing.SumaCumLaude);
        }

        [TestMethod]
        public void Test_MagnaCumLaude_Standing_When_Average_above_95()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 ,Credits = 1},
                    new Course{Id = 2, Name = "Science", Mark=100 ,Credits = 1},
                    new Course{Id = 3, Name = "Literature", Mark=95 , Credits = 1},
                    new Course{Id = 4, Name = "Physichal Education", Mark=90, Credits = 1}
                }

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsTrue(result.Item1);
            Assert.IsTrue(result.Item2 == Standing.MagnaCumLaude);
        }

        [TestMethod]
        public void Test_Diploma_Is_Null_Throws_Argument_Null_Exception()
        {
            var tracker = new Bl.GraduationTracker(new Repository());


            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=20 ,Credits = 1},
                }

            };

            TestHelperExtensions.Throw<ArgumentNullException>(() => tracker.HasGraduated(null, student),
                $"Value cannot be null.\r\nParameter name: {nameof(Diploma).ToLower()}");
        }

        [TestMethod]
        public void Test_Student_Failed_When_has_No_Courses()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = null

            };

            var result = tracker.HasGraduated(diploma, student);

            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == Standing.Remedial);
        }

        [TestMethod]
        public void Test_Student_Is_Null_Throws_Argument_Null_Exception()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = new[] { 100, 102, 103, 104 }
            };

            TestHelperExtensions.Throw<ArgumentNullException>(() => tracker.HasGraduated(diploma, null),
                $"Value cannot be null.\r\nParameter name: {nameof(Student).ToLower()}");
        }

        [TestMethod]
        public void Test_Diploma_Requirements_Is_Null_Throws_Argument_Null_Exception()
        {
            var tracker = new Bl.GraduationTracker(new Repository());

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                RequirementIds = null
            };

            var student = new Student
            {
                Id = 1,
                Courses = new[]
                {
                    new Course{Id = 1, Name = "Math", Mark=20 ,Credits = 1},
                }

            };

            TestHelperExtensions.Throw<ArgumentException>(() => tracker.HasGraduated(diploma, student),
                $"{nameof(Requirement)}");
        }


    }
}
