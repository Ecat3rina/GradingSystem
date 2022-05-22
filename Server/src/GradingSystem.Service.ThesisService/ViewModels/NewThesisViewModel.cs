using System;
using System.ComponentModel.DataAnnotations;

namespace GradingSystem.Service.ThesisService.ViewModels
{
    public class NewThesisViewModel
    {
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        public int NumberOfPages { get; set; }
        public string Filename { get; set; }
        public byte[] Filestream { get; set; }

    }
}
