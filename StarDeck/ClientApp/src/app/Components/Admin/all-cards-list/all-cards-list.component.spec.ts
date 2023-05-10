import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllCardsListComponent } from './all-cards-list.component';

describe('CardListComponent', () => {
  let component: AllCardsListComponent;
  let fixture: ComponentFixture<AllCardsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllCardsListComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AllCardsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
