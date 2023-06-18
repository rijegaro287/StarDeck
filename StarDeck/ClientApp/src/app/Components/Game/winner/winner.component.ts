import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-winner',
  templateUrl: './winner.component.html',
  styleUrls: ['./winner.component.scss']
})
export class WinnerComponent implements OnInit {
  gameWinner: any;

  constructor() {
    this.gameWinner = null
  }

  ngOnInit(): void {
    this.gameWinner = JSON.parse(sessionStorage.getItem('gameWinner')!);
    console.log(this.gameWinner);
  }

  onAcceptClicked() {
    window.location.href = '/user';
  }
}
