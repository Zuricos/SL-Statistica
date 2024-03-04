import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';

@Component({
  selector: 'app-titlebar',
  templateUrl: './titlebar.component.html',
  styleUrls: ['./titlebar.component.scss'],
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, MatIconModule],
})

export class TitlebarComponent {
  title: string = "Swisslos Statistica";
}
