import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TicketComponent } from './components/ticket/ticket.component';
import { HttpClientModule } from '@angular/common/http';
import { QRCodeModule } from 'angularx-qrcode';
import { TicketDetailsComponent } from './components/ticket-details/ticket-details.component'
import { AuthModule } from '@auth0/auth0-angular'
@NgModule({
  declarations: [
    AppComponent,
    TicketComponent,
    TicketDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    QRCodeModule,
    AuthModule.forRoot({
      domain: 'dev-fbr2yb3xvzwpcqbi.us.auth0.com',
      clientId: '84z25j4UljFTKmIo0vGyBMuQX7KUXoFr',
      authorizationParams: {
        redirect_uri: 'http://localhost:4200',
        audience: 'https://dev-fbr2yb3xvzwpcqbi.us.auth0.com/api/v2/'
      },
      useRefreshTokens: true,
      cacheLocation: 'localstorage'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
