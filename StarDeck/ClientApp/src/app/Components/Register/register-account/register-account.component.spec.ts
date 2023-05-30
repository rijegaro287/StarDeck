import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterAccountComponent } from './register-account.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import { AccountService } from 'src/app/Services/account.service';
import { LoginService } from 'src/app/Services/login.service';
import {RequestService} from "../../../Services/request.service";

describe('RegisterAccountComponent', () => {
  let component: RegisterAccountComponent;
  let fixture: ComponentFixture<RegisterAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ RegisterAccountComponent ],
      providers:[RegisterAccountComponent, { provide: 'BASE_URL', useValue: 'http://localhost'}, AccountService, LoginService, RequestService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should throw an error when required fields are missing', () => {
    expect(() => {
      component.validateAccount({
        id: '',
        name: '',
        nickname: '',
        email: '',
        country: '',
        password: ''
      });
    }).toThrowError('Sus datos están incompletos, intente de nuevo');
  });

  it('should call onCheck method and toggle term value', () => {
    component.onCheck();
    expect(component.term).toBe(true);

    component.onCheck();
    expect(component.term).toBe(false);
  });

  it('should return true if password has both letters and numbers', () => {
    const passwordWithLettersAndNumbers = 'Password123';
    const result = component.hasLetterAndNumber(passwordWithLettersAndNumbers);
    expect(result).toBe(true);
  });

  it('should return false if password does not have both letters and numbers', () => {
    const passwordWithOnlyLetters = 'Password';
    const result = component.hasLetterAndNumber(passwordWithOnlyLetters);
    expect(result).toBe(false);

    const passwordWithOnlyNumbers = '123456';
    const result2 = component.hasLetterAndNumber(passwordWithOnlyNumbers);
    expect(result2).toBe(false);
  });


  it('should throw an error when name length is greater than 30 characters', () => {
    const invalidUser: any = {
      id: 'U-1234567890',
      name: 'John Doe John Doe John Doe John Doe John Doe',
      nickname: 'johndoe',
      email: 'johndoe@gmail.com',
      country: 'United States',
      password: 'Password1'
    };

    expect(() => {
      component.validateAccount(invalidUser);
    }).toThrowError('El nombre del usuario debe tener entre 5 y 30 caracteres');
  });

  it('should throw an error when name length is less than 5 characters', () => {
    const invalidUser: any = {
      id: 'U-1234567890',
      name: 'John',
      nickname: 'johndoe',
      email: 'johndoe@gmail.com',
      country: 'United States',
      password: 'Password1'
    };

    expect(() => {
      component.validateAccount(invalidUser);
    }).toThrowError('El nombre del usuario debe tener entre 5 y 30 caracteres');
  });

  it('should throw an error when nickname length is less than 5 characters', () => {
    const invalidUser: any = {
      id: 'U-1234567890',
      name: 'John Doe Martin',
      nickname: 'john',
      email: 'johndoe@gmail.com',
      country: 'United States',
      password: 'Password1'
    };

    expect(() => {
      component.validateAccount(invalidUser);
    }).toThrowError('El apodo del usuario debe tener entre 5 y 30 caracteres');
  });
  it('should throw an error when nickname length is more than 30 characters', () => {
    const invalidUser: any = {
      id: 'U-1234567890',
      name: 'John Doe Martin',
      nickname: 'johndoejohndoejohndoejohndoejohndoejohndoejohndoejohndoejohndoe',
      email: 'johndoe@gmail.com',
      country: 'United States',
      password: 'Password1'
    };

    expect(() => {
      component.validateAccount(invalidUser);
    }).toThrowError('El apodo del usuario debe tener entre 5 y 30 caracteres');
  });
  it('should throw an error when email is invalid', () => {
    component.newUser.patchValue({ email: 'invalid-email' });

    expect(() => {
      component.validateAccount({
        id: 'U-1234567890',
        name: 'John Doe',
        nickname: 'johndoe',
        email: component.newUser.value.email,
        country: 'United States',
        password: 'Password1'
      });
    }).toThrowError('Ingrese una dirección de correo electrónico válida');
  });



});
