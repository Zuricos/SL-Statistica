import { Component } from '@angular/core';

@Component({
  selector: 'app-lotterydraw',
  templateUrl: './lotterydraw.component.html',
  styleUrl: './lotterydraw.component.scss'
})
export class LotterydrawComponent {
  numbers: number[] = [];
  luckyNumber: number = 0;

  constructor() {
    this.generateNumbers();
  }

  generateNumbers() {
    while(this.numbers.length < 6) {
      let num = Math.floor(Math.random() * 50) + 1;
      if(this.numbers.indexOf(num) === -1) this.numbers.push(num);
    }
    this.luckyNumber = Math.floor(Math.random() * 50) + 1;
  }
}
