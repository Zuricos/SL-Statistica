import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatCardModule } from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LotterydrawComponent } from './components/lotterydraw/lotterydraw.component';
import { LdDatecardComponent } from './components/ld-datecard/ld-datecard.component';
import { TitlebarComponent } from './components/titlebar/titlebar.component';

@NgModule({
  declarations: [
    AppComponent,
    LotterydrawComponent,
    LdDatecardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TitlebarComponent,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
