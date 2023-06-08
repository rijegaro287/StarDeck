import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardService } from 'src/app/Services/card.service';
import { RequestService } from 'src/app/Services/request.service';
import { HelpersService } from 'src/app/Services/helpers.service';
import { CardFormDialogComponent } from './card-form-dialog.component';
import * as random from 'random-web-token';

describe('CardFormDialogComponent', () => {
  let component: CardFormDialogComponent;
  let fixture: ComponentFixture<CardFormDialogComponent>;
  let mockCardService: jasmine.SpyObj<CardService>;
  let mockHelpersService: jasmine.SpyObj<HelpersService>;

  beforeEach(async () => {
    mockCardService = jasmine.createSpyObj('CardService', ['addCard']);
    mockHelpersService = jasmine.createSpyObj('HelpersService', ['fileToBase64']);

    await TestBed.configureTestingModule({
      declarations: [CardFormDialogComponent],
      imports: [FormsModule, ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: CardService, useValue: mockCardService },
        { provide: HelpersService, useValue: mockHelpersService }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });



  it('should throw an error when validateCardForm is called with invalid card name length', () => {
    const invalidCard: any = {
      id: '1234567890',
      name: 'Test',
      energy: 10,
      battlecost: 5,
      image: '',
      active: true,
      type: 1,
      description: 'Test Description'
    };

    expect(() => {
      component.validateCardForm(invalidCard);
    }).toThrowError('El nombre de la carta debe tener entre 5 y 30 caracteres');
  });

  it('should throw an error when validateCardForm is called with invalid card description length', () => {
    const invalidCard: any = {
      id: '1234567890',
      name: 'Test Card',
      energy: 10,
      battlecost: 5,
      image: '',
      active: true,
      type: 1,
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam posuere, purus vitae posuere venenatis, metus mi finibus massa, sed sagittis metus sapien ' +
        'a lectus.xniusbndounsconosiucbxnnjsjnonxboinouboubbiuvyuviyvyiyvyvpyvbuyibupbipuobiovuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu' +
        'hklllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnxl' +
        'kmknmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm' +
        'kjjjjjjjjnnnnnnnnnnnnnnnccccccccccccccccccccccrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrraaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' +
        'xkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk' +
        'ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss'
    };

    expect(() => {
      component.validateCardForm(invalidCard);
    }).toThrowError('La descripción de la carta debe tener como máximo 1000 caracteres');
  });

  it('should throw an error when validateCardForm is called with invalid card energy value', () => {
    const invalidCard: any = {
      id: '1234567890',
      name: 'Test Card',
      energy: 200,
      battlecost: 5,
      image: '',
      active: true,
      type: 1,
      description: 'Test Description'
    };

    expect(() => {
      component.validateCardForm(invalidCard);
    }).toThrowError('La energía de la carta debe ser un número entre -100 y 100');
  });

  it('should throw an error when validateCardForm is called with invalid card battle cost value', () => {
    const invalidCard: any = {
      id: '1234567890',
      name: 'Test Card',
      energy: 10,
      battlecost: -10,
      image: '',
      active: true,
      type: 1,
      description: 'Test Description'
    };

    expect(() => {
      component.validateCardForm(invalidCard);
    }).toThrowError('El costo de la carta debe ser un número entre 0 y 100');
  });
});
