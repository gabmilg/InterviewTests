namespace GraduationTracker.Entities
{
    public class Diploma
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        public int[] RequirementIds { get; set; }
    }
}
