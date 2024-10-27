import { Injectable } from '@angular/core';
import { OIBRequest } from '../models/OIBRequest';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketDetails } from '../models/TicketDetails';
import { environment } from '../environments/environments.development';
@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private http: HttpClient) { }



  getAllTicketsGenerated(): Observable<number> {
    return this.http.get<number>(environment.restAPI + 'Ticket');
  }

  async createTicket(newTicket: OIBRequest) {
    var response = await this.http.post(environment.restAPI + 'Ticket', newTicket, {
      observe: 'response',
      responseType: 'text'
    }).toPromise();

    return response?.body;
  }


  getTicketById(id: string): Observable<TicketDetails> {
    let actualToken = ""
    const rawTokenData = localStorage.getItem('@@auth0spajs@@::84z25j4UljFTKmIo0vGyBMuQX7KUXoFr::@@user@@');
    if (rawTokenData == null) {
      actualToken = ""
    }
    const tokenWithParseComma = rawTokenData?.split(',');
    actualToken = this.removeChar(tokenWithParseComma![0]);


    const headers = new HttpHeaders({
      'Authorization': `Bearer ${actualToken}`
    });
    const options = {
      headers: headers,
      withCredentials: true
    };

    return this.http.get<TicketDetails>(environment.restAPI + 'Ticket/' + `${id}`, options);

  }
  removeChar(str: string) {
    return str.substring(13, str.length - 1);
  }

}
