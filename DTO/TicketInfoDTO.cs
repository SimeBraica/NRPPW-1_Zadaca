using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO {
    public record TicketInfoDTO {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int Vatin { get; set; }
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
    }
}
