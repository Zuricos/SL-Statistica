import { Component } from '@angular/core';

import { LotterydrawComponent } from '../lotterydraw/lotterydraw.component';

@Component({
  selector: 'app-ld-datecard',
  templateUrl: './ld-datecard.component.html',
  styleUrl: './ld-datecard.component.scss'
})
export class LdDatecardComponent {
  date: Date = new Date();
  lotterydraws: LotterydrawComponent[] = [];
  isCollapsed: boolean = true;

  constructor() {
    this.insertDraws(this.generateDraws());
  }
  
  insertDraws(draws: LotterydrawComponent[]) {
    this.lotterydraws = draws;
  }

  generateDraws() : LotterydrawComponent[] {
    var draws: LotterydrawComponent[] = [];
    for (let i = 0; i < 10; i++) {
      let draw = new LotterydrawComponent();
      draws.push(draw);
    }
    return draws;
  }

  // toggle the collapsed state
  toggleCollapsed() {
    this.isCollapsed = !this.isCollapsed;
  }
}
