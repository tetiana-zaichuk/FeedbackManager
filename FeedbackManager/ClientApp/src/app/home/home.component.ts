import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  tableMode: boolean = true;

  cancel() {
    this.tableMode = true;
  }
 
  add() {
    this.tableMode = false;
  }
}
