import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WinnerComponent } from './winner.component';

describe('WinnerComponent', () => {
  let component: WinnerComponent;
  let fixture: ComponentFixture<WinnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WinnerComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(WinnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  const winner = {
    winner: 'jugador2', planetWinners: [
      {
        planetName: 'Planet 1',
        winner: 'jugador2',
        winnerPoints: 7,
      },
      {
        planetName: 'Planet 2',
        winner: 'jugador2',
        winnerPoints: 3,
      },
    ]
  }

  it('should create', () => {
    sessionStorage.setItem(
      'gameWinner',
      JSON.stringify(winner)
    );

    expect(component).toBeTruthy();
  });

  it('should return to user page', () => {
    component.gameWinner = JSON.parse(sessionStorage.getItem('gameWinner')!);
    expect(component.gameWinner).toEqual(winner);
  });
});
