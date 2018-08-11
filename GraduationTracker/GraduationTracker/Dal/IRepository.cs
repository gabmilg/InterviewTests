using GraduationTracker.Entities;
using System;

namespace GraduationTracker.Dal
{
    public interface IRepository
    {
        Student GetStudent(int id);

        Student[] GetStudents(Func<Student, bool> predicate);

        Diploma GetDiploma(int id);

        Requirement GetRequirement(int id);

        Requirement[] GetRequirements(Func<Requirement, bool> predicate);

        Diploma[] GetDiplomas(Func<Diploma, bool> predicate);
    }
}
