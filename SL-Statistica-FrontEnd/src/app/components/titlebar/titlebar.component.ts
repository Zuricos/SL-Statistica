import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-titlebar',
  templateUrl: './titlebar.component.html',
  styleUrls: ['./titlebar.component.scss']
})

export class TitlebarComponent {
  title: string = "Swisslos Statistica";
}
