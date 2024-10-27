import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { TicketDetails } from '../../models/TicketDetails';

@Component({
  selector: 'app-ticket-details',
  templateUrl: './ticket-details.component.html',
  styleUrl: './ticket-details.component.css'
})
export class TicketDetailsComponent implements OnInit {
  constructor(private route: ActivatedRoute,
              private ticketService: TicketService) { }

  id: string | null = "";
  dateCreated!: Date;
  vatin: number = 0;
  firstName: string = "";
  lastName: string = ""

  ticketDetails!: TicketDetails;
  idFromQuery!: string;

  ngOnInit() {
    this.idFromQuery = this.route.snapshot.paramMap.get('id')!;

    if (this.idFromQuery == null) {
      return;
    }

    this.ticketService.getTicketById(this.idFromQuery).subscribe(
      (data: TicketDetails) => {
        this.ticketDetails = data;
        this.id = this.ticketDetails.id;
        this.dateCreated = this.ticketDetails.dateCreated;
        this.vatin = this.ticketDetails.vatin;
        this.firstName = this.ticketDetails.firstname;
        this.lastName = this.ticketDetails.lastname;
      },
      (error) => {
        console.error('Error fetching ticket details:', error);
      }
    );
  }
}
