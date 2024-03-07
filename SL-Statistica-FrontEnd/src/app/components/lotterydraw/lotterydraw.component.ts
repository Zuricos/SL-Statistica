import { Component } from '@angular/core';

@Component({
  selector: 'app-lotterydraw',
  templateUrl: './lotterydraw.component.html',
  styleUrl: './lotterydraw.component.scss'
})
export class LotterydrawComponent {
  numbers: number[] = [];
  luckyNumber: number = 0;
  date: Date = new Date();

  constructor() {
    this.generateNumbers();
  }

  generateNumbers() {
    while(this.numbers.length < 6) {
      let num = Math.floor(Math.random() * 42) + 1;
      if(this.numbers.indexOf(num) === -1) this.numbers.push(num);
    }
    this.numbers.sort((a, b) => a - b);
    this.luckyNumber = Math.floor(Math.random() * 6) + 1;
  }
}
