using DAL.Models;
using DAL.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services {
    public class TicketService {

        private TicketRepository _ticketRepository;
        public TicketService(TicketRepository ticketRepository) {
            _ticketRepository = ticketRepository;
        }
        public async Task<List<Ticket>> GetAllTickets() {
            return (List<Ticket>)await _ticketRepository.GetAll();
        }

        public async Task<string> AddTicket(OIBDTO newTicket) {
            var addedTicket = await _ticketRepository.Create(newTicket);
            return addedTicket.Id.ToString();
        }

        public async Task<TicketInfoDTO> GetTicketById(Guid id) {
            var ticketDetails = await _ticketRepository.GetTicket(id);

            var ticketInfoDTO = new TicketInfoDTO {
                Id = ticketDetails.Id,
                DateCreated = ticketDetails.DateCreated,
                Vatin = ticketDetails.Oib.Vatin,
                Firstname = ticketDetails.Oib.Firstname,
                Lastname = ticketDetails.Oib.Lastname
            };

            return ticketInfoDTO;
        }
    }
}
