using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITSupportTickets.Models;

namespace ITSupportTickets.Data
{
    public class ITSupportTicketsContext : DbContext
    {
        public ITSupportTicketsContext (DbContextOptions<ITSupportTicketsContext> options)
            : base(options)
        {
        }

        public DbSet<ITSupportTickets.Models.Ticket> Ticket { get; set; }
    }
}
