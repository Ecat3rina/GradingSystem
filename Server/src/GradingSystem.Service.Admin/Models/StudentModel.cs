using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNP { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public Guid GroupId { get; set; }
    }
}
