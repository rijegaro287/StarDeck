import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllCardsListComponent } from './all-cards-list.component';
import {MatDialog} from "@angular/material/dialog";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {CardFormDialogComponent} from "../../Dialogs/card-form-dialog/card-form-dialog.component";

describe('CardListComponent', () => {
  let component: AllCardsListComponent;
  let fixture: ComponentFixture<AllCardsListComponent>;
  let mockDialog: jasmine.SpyObj<MatDialog>;

  beforeEach(async () => {
    mockDialog = jasmine.createSpyObj('MatDialog', ['open']);
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [AllCardsListComponent],
      providers: [
        { provide: MatDialog, useValue: mockDialog }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AllCardsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open the dialog on openDialog', () => {
    component.openDialog();

    expect(mockDialog.open).toHaveBeenCalledWith(CardFormDialogComponent);
  });


});
