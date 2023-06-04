import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanetListComponent } from './planet-list.component';
import {MatDialog} from "@angular/material/dialog";
import {CreatePlanetComponent} from "../create/create-planet.component";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('PlanetListComponent', () => {
  let component: PlanetListComponent;
  let fixture: ComponentFixture<PlanetListComponent>;
  let mockDialog: jasmine.SpyObj<MatDialog>;
  beforeEach(async () => {
    mockDialog = jasmine.createSpyObj('MatDialog', ['open']);
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [PlanetListComponent ],
      providers: [
        { provide: MatDialog, useValue: mockDialog }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanetListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open the dialog on createPlanet', () => {
    component.createPlanet();

    expect(mockDialog.open).toHaveBeenCalledWith(CreatePlanetComponent);
  });

});
