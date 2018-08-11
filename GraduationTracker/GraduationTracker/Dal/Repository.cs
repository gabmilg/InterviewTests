using System;
using System.Linq;
using GraduationTracker.Entities;

namespace GraduationTracker.Dal
{
    public class Repository : IRepository
    {
        private static Requirement[] _requirements;

        private static Student[] _students;

        private static Diploma[] _diplomas;

        static Repository()
        {
            _requirements = new[]
           {
                new Requirement{Id = 100, MinimumMark=50, CourseId = 1, Credits=1 },
                new Requirement{Id = 102, MinimumMark=50, CourseId = 2, Credits=1 },
                new Requirement{Id = 103, MinimumMark=50, CourseId = 3, Credits=1 },
                new Requirement{Id = 104, MinimumMark=50, CourseId = 4, Credits=1 }
            };

            _diplomas = new[]
            {
                new Diploma
                {
                    Id = 1,
                    Credits = 4,
                    RequirementIds = new []{100,102,103,104}
                }
            };

            _students = new[]
            {
                new Student
                {
                    Id = 1,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = 95},
                        new Course {Id = 2, Name = "Science", Mark = 95},
                        new Course {Id = 3, Name = "Literature", Mark = 95},
                        new Course {Id = 4, Name = "Physichal Education", Mark = 95}
                    }
                },
                new Student
                {
                    Id = 2,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = 80},
                        new Course {Id = 2, Name = "Science", Mark = 80},
                        new Course {Id = 3, Name = "Literature", Mark = 80},
                        new Course {Id = 4, Name = "Physichal Education", Mark = 80}
                    }
                },
                new Student
                {
                    Id = 3,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = 50},
                        new Course {Id = 2, Name = "Science", Mark = 50},
                        new Course {Id = 3, Name = "Literature", Mark = 50},
                        new Course {Id = 4, Name = "Physichal Education", Mark = 50}
                    }
                },
                new Student
                {
                    Id = 4,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = 40},
                        new Course {Id = 2, Name = "Science", Mark = 40},
                        new Course {Id = 3, Name = "Literature", Mark = 40},
                        new Course {Id = 4, Name = "Physichal Education", Mark = 40}
                    }
                }

            };
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public Diploma GetDiploma(int id)
        {
            return _diplomas.FirstOrDefault(d => d.Id == id);
        }

        public Requirement GetRequirement(int id)
        {
            return _requirements.FirstOrDefault(r => r.Id == id);
        }


        public Diploma[] GetDiplomas(Func<Diploma, bool> predicate)
        {
            return _diplomas.Where(predicate).ToArray();
        }

        public Requirement[] GetRequirements(Func<Requirement, bool> predicate)
        {
            return _requirements.Where(predicate).ToArray();
        }

        public Student[] GetStudents(Func<Student, bool> predicate)
        {
            return _students.Where(predicate).ToArray();
        }
    }
}
