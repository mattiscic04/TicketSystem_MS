using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ITicketRepository    {
        void Book(Ticket t);
        IQueryable<Ticket> GetTickets();
    }
}
