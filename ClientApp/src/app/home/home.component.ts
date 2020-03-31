import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public http: HttpClient;
  public baseUrl: string;

  public calculation: Calculation;

  public inputNumbers: string;

  public OnInput(event: any) {
    if (event.target.validity.valid) {
      this.inputNumbers = event.target.value;

      this.http.get<Calculation>(this.baseUrl + 'calculator?nums=' + this.inputNumbers).subscribe(result => {
        this.calculation = result;
      }, error => console.error(error));
    }
    else {
      alert("Invalid input. Please try again.");
    }
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
}

interface Calculation {
  mean: number;
  median: number;
  mode: number;
}
