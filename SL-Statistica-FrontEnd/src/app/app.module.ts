import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LotterydrawComponent } from './components/lotterydraw/lotterydraw.component';
import { TitlebarComponent } from './components/titlebar/titlebar.component';

@NgModule({
  declarations: [
    AppComponent,
    LotterydrawComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TitlebarComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
