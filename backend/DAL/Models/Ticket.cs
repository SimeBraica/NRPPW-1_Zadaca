using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Ticket {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int Vatin { get; set; }
        public OIB Oib { get; set; }
    }
}
