using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedBACControlSystem.DAL.Entities
{
    public class ThesisPage
    {
        public Guid Id { get; set; }
        public Guid ThesisId { get; set; }
        public string FileName { get; set; }
        public int PageNr { get; set; }

        //BLOBID
    }
}
