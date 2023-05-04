import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardFormDialogComponent } from './card-form-dialog.component';

describe('CardFormDialogComponent', () => {
  let component: CardFormDialogComponent;
  let fixture: ComponentFixture<CardFormDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardFormDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
