using System;
using System.Linq;
using GraduationTracker.Dal;
using GraduationTracker.Entities;

namespace GraduationTracker.Bl
{
    public class GraduationTracker
    {
        private readonly IRepository _repository;

        public GraduationTracker(IRepository repository)
        {
            _repository = repository;
        }

        public Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student)
        {
            if (diploma == null)
                throw new ArgumentNullException(nameof(diploma));

            if (diploma.RequirementIds == null)
                throw new ArgumentException(nameof(Requirement));

            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var hasAllRequirements = false;
            var requirements = _repository.GetRequirements(r => diploma.RequirementIds.Contains(r.Id));

            //Not checking requirements result for null or length == 0 , assuming that database has foreight keys for id's

            var diplomaResult = student.Courses != null ?
                requirements.Join(student.Courses, r => r.CourseId, c => c.Id, (r, c) => new
                {
                    CourseId = c.Id,
                    Credit = c.Mark > r.MinimumMark ? c.Credits : 0,
                    c.Mark
                }).ToList() : null;

            if (diplomaResult != null && diplomaResult.Count() == requirements.Length)
                hasAllRequirements = true;

            var averageMark = diplomaResult?.Average(d => d.Mark) ?? 0;
            var credits = diplomaResult?.Sum(c => c.Credit) ?? 0;
            var standing = GetStandingsBasedOnAverageMark(averageMark);
            var isGraduated = credits == diploma.Credits && standing != Standing.Remedial && hasAllRequirements;

            return new Tuple<bool, Standing>(isGraduated, standing);
        }

        private static Standing GetStandingsBasedOnAverageMark(double averageMark)
        {
            if (averageMark < 50)
                return Standing.Remedial;

            if (averageMark < 80)
                return Standing.Average;

            if (averageMark < 95)
                return Standing.SumaCumLaude;

            return Standing.MagnaCumLaude;
        }
    }
}
