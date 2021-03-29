using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSupportTickets.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string DepartmentName { get; set; }
        public string RequestedBy { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedTimeStamp { get; set; }
    }
}
