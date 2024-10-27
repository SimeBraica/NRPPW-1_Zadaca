using Auth0.AspNetCore.Authentication;
using BAL.Services;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase {

        public TicketService _ticketService;
        public TicketController(TicketService ticketService) {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> Ticket() {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets.Count);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TicketInfoDTO>> TicketById(Guid id) {
            var token = Request.Headers.Authorization;
            if (token == "") {
                return Forbid();
            }

            var ticket = await _ticketService.GetTicketById(id);
            return Ok(ticket);

        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(OIBDTO newTicket) {
            if(string.IsNullOrWhiteSpace(newTicket.Firstname) || string.IsNullOrWhiteSpace(newTicket.Lastname)) {
                return StatusCode(400, "Missing info about the OIB");
            }
            var _newTicket = await _ticketService.AddTicket(newTicket);
            if (_newTicket == null) {
                return BadRequest();
            }
            return Ok(_newTicket);
        }




    }
}
