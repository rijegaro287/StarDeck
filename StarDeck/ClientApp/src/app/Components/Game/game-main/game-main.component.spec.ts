import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameMainComponent } from './game-main.component';

describe('GameMainComponent', () => {
  let component: GameMainComponent;
  let fixture: ComponentFixture<GameMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [GameMainComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(GameMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the component correctly', () => {
    expect(component.playerCardsID).toEqual('player1Cards');
    expect(component.opponentCardsID).toEqual('player2Cards');
    expect(component.planetsInfo).toEqual([]);
    expect(component.status).toEqual('Iniciando partida...');
    expect(component.currentTurn).toEqual(0);
    expect(component.playingTurn).toEqual(false);
    expect(component.selectedCard).toEqual(null);
  });




});
