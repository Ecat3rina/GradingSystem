using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.ViewModels
{
    public class ThesisViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public string Filename { get; set; }
        public int NumberOfPages { get; set; }

        public byte[] FileContent { get; set; }
    }
}
