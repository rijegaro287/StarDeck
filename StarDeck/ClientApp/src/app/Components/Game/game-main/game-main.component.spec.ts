import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameMainComponent } from './game-main.component';

describe('GameMainComponent', () => {
  let component: GameMainComponent;
  let fixture: ComponentFixture<GameMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the game with correct configuration', () => {
    expect(component.config.type).toBe(Phaser.AUTO);
    expect(component.config.width).toBe(component.playableWidth);
    expect(component.config.height).toBe(component.playableHeight);
    expect(component.game).toBeDefined();
  });
});
