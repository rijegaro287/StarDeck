import {Component, Inject, OnInit} from '@angular/core';
import {HelpersService} from "../../../Services/helpers.service";
import {CardService} from "../../../Services/card.service";
import {BattleService} from "../../../Services/battle.service";
import {FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../../../Services/account.service";
import {IGlobalRanking, IIndividualRanking} from "../../../Interfaces/Ranking";

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent implements OnInit{
  displayedColumns: string[] = ['position', 'nickname', 'points'];
  idAccount: string;
  baseurl: string;

  rankingList: IGlobalRanking[];

  myPosition: IIndividualRanking;

  constructor(@Inject('BASE_URL') baseUrl: string,
              protected accouter: AccountService ) {
    //-------------Inizializacion de variables --------------
    this.idAccount = sessionStorage.getItem('ID')!;

    this.baseurl = baseUrl;

    this.rankingList = [];

    this.myPosition = { position: 0, nickname: '', points: 0 };

  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    //Logica para obtener la lista de Decks de un jugador

    await this.accouter.getMyPosition(this.idAccount)
      .then(myPosition => {
        this.myPosition = myPosition;
        console.log(this.myPosition)
      });

    await this.accouter.getRanking(this.idAccount)
      .then(rankingList => {
        this.rankingList = rankingList;
        console.log(this.rankingList)
      });

  }


}
