using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Models
{
    public class GroupExamsModel
    {
        public string GroupName { get; set; }
        public List<string> Exams { get; set; }
    }
}
