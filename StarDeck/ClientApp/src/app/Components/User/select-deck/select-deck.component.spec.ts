import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectDeckComponent } from './select-deck.component';

describe('SelectDeckComponent', () => {
  let component: SelectDeckComponent;
  let fixture: ComponentFixture<SelectDeckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SelectDeckComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectDeckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
