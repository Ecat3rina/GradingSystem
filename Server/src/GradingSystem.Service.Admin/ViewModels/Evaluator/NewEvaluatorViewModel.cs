using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels
{
    public class NewEvaluatorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
    }
}
