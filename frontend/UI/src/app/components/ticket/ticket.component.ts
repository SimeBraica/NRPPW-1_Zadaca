import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../services/ticket.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  Vatin?: number;
  Firstname?: string;
  Lastname?: string;
  qrcode: string = '';

  allTicketsGenerated: number = 0;
  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.getAllTicketsGenerated().subscribe(
      (data: number) => {
        this.allTicketsGenerated = data;
      },
      (error) => {
        console.error('Error fetching ticket details:', error);
      }
    );
  }
  async createNewTicket() {
    const newTicket = {
      Vatin: this.Vatin,
      Firstname: this.Firstname,
      Lastname: this.Lastname
    };

    try {
      const response = await this.ticketService.createTicket(newTicket);
      this.qrcode = "https://nrppw-1-zadaca.onrender.com/ticket-details/" + (response ? response.toString() : '');
    } catch (error) {
      console.error('Error creating ticket:', error);
    }
  }
}
