using System;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class Thesis
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public int FinalScore { get; set; }
        public Evaluator[] Evaluators { get; set; }
        public int[] Scores { get; set; }
        public DateTime GradationDate { get; set; }

    }
}
