import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketComponent } from './components/ticket/ticket.component';
import { TicketDetailsComponent } from './components/ticket-details/ticket-details.component'

const routes: Routes = [
  { path: '', redirectTo: '/ticket-generation', pathMatch: 'full' },
  { path: 'ticket-generation', component: TicketComponent },
  { path: 'ticket-details/:id', component: TicketDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
