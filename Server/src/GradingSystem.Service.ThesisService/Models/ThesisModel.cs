using System;

namespace GradingSystem.Service.ThesisService.Models
{
    public class ThesisModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public string Filename { get; set; }
        public int NumberOfPages { get; set; }

        public byte[] FileContent { get; set; }
    }
}
