import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectionCardComponent } from './selection-card.component';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

describe('SelectionCardComponent', () => {
  let component: SelectionCardComponent;
  let fixture: ComponentFixture<SelectionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [SelectionCardComponent],
      providers: [
        SelectionCardComponent,
        { provide: 'BASE_URL', useValue: 'http://localhost' },
        { provide: MAT_DIALOG_DATA, useValue: {} },
        { provide: MatDialogRef, useValue: {} },
        { provide: MatDialog, useValue: {} }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(SelectionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should initialize variables correctly', () => {
    expect(component.collectionInitial).toEqual([]);
    expect(component.cards).toEqual([]);
    expect(component.selectionCard1).toEqual([]);
    expect(component.selectionCard2).toEqual([]);
    expect(component.selectionCard3).toEqual([]);
    expect(component.selectedCards).toEqual([]);
    expect(component.cardSelected1).toBe(false);
    expect(component.cardSelected2).toBe(false);
    expect(component.cardSelected3).toBe(false);
  });

  it('should show an alert if createInitialCollection is called with less than 3 selected cards', async () => {
    spyOn(window, 'alert');

    await component.createInitialCollection();

    expect(window.alert).toHaveBeenCalledWith('Debe seleccionar 3 cartas');
  });
});
