import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5000'; 

  constructor(private http: HttpClient) { }

  getLatestDraw(): Observable<any> {
    return this.http.get(`${this.baseUrl}/LatestDraw`);
  }

  getDraws(parameters: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/`, { params: parameters });
  }

  updateDB(year: number, webScrapper: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/UpdateDB`, { year, webScrapper });
  }
}

export interface LotteryDrawDto {
  numberOne: number;
  numberTwo: number;
  numberThree: number;
  numberFour: number;
  numberFive: number;
  numberSix: number;
  luckyNumber: number;
  date: Date;
  jackPot?: number;
}
