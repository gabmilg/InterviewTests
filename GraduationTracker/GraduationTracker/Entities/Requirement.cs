namespace GraduationTracker.Entities
{
    public class Requirement
    {
        public int Id { get; set; }
        public int MinimumMark { get; set; }
        public int Credits { get; set; }
        public int CourseId { get; set; }
    }
}
