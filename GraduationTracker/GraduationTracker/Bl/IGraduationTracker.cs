using GraduationTracker.Entities;
using System;

namespace GraduationTracker.Bl
{
    public interface IGraduationTracker
    {
        Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student);
    }
}
