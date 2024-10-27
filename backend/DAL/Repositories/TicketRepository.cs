using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories {
    public class TicketRepository : Repository<Ticket> {
        public TicketRepository(Zadaca1Context context) : base(context) {
        }

        public async Task<IEnumerable<Ticket>> GetAll() {
            return await Context.Tickets.ToListAsync();
        }

        public async Task<Ticket> Create(OIBDTO newTicket) {

            var isExistingOIB = await Context.OIBs.FirstOrDefaultAsync(a => a.Vatin == newTicket.Vatin);

            if (isExistingOIB == null) {
                OIB _oib = new OIB {
                    Vatin = newTicket.Vatin,
                    Firstname = newTicket.Firstname,
                    Lastname = newTicket.Lastname,
                    Tickets = new List<Ticket>()
                };
                await Context.OIBs.AddAsync(_oib);
            }

            var numberOfTicketsPerOIB = Context.Tickets.Count(t => t.Vatin == newTicket.Vatin);
            if (numberOfTicketsPerOIB >= 3) {
                return null;
            }
            Ticket _ticket = new Ticket {
                DateCreated = DateTime.Now.ToUniversalTime(),
                Vatin = newTicket.Vatin
            };
            await Context.AddAsync(_ticket);
            await Context.SaveChangesAsync();
            return _ticket;
        }

        public async Task<Ticket> GetTicket(Guid id) {
            var ticket = await Context.Tickets
                .Include(a => a.Oib)
                .FirstOrDefaultAsync(ticket => ticket.Id == id);
            return ticket;
        }

    }
}
