import { ComponentFixture, TestBed } from '@angular/core/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import { CreatePlanetComponent } from './create-planet.component';
import {FormGroup} from "@angular/forms";

describe('CreatePlanetComponent', () => {
  let component: CreatePlanetComponent;
  let fixture: ComponentFixture<CreatePlanetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      declarations: [CreatePlanetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatePlanetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should throw an error when validatePlanet is called with invalid planet name length', () => {
    const invalidPlanet: any = {
      id: 'P-1234567890',
      name: 'Test',
      image: '',
      active: true,
      type: 1,
      description: 'Planet Test Description'
    };

    expect(() => {
      component.validatePlanet(invalidPlanet);
    }).toThrowError('El nombre del planeta debe tener entre 5 y 30 caracteres');
  });

  it('should throw an error when validatePlanet is called with invalid planet description length', () => {
    const invalidPlanet: any = {
      id: 'P-1234567890',
      name: 'Test Planet',
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
      component.validatePlanet(invalidPlanet);
    }).toThrowError('La descripción del planet debe tener como máximo 1000 caracteres');
  });

});


