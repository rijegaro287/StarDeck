import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  userID: string | null;

  constructor(private gameService: GameService) {
    this.userID = sessionStorage.getItem('ID');
  }

  ngOnInit(): void {
    this.gameService.isInGame(this.userID ? this.userID : '')
      .catch(async (response: any) => {
        if (response.error.value === true) {
          await alert('Se ha detectado una partida activa. Redirigiendo a la sala de juego...');
          window.location.href = `/game`;
        }
      })
  }
}