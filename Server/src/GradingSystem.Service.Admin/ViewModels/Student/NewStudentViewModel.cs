using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.ViewModels.Student
{
    public class NewStudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string IDNP { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public Guid GroupId { get; set; }
    }
}
