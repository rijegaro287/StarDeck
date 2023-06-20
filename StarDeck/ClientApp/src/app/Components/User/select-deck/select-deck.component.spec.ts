import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectDeckComponent } from './select-deck.component';
import { HttpClientTestingModule } from "@angular/common/http/testing";

describe('SelectDeckComponent', () => {
  let component: SelectDeckComponent;
  let fixture: ComponentFixture<SelectDeckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [SelectDeckComponent],
      providers: [SelectDeckComponent, { provide: 'BASE_URL', useValue: 'http://localhost' }]

    })
      .compileComponents();

    fixture = TestBed.createComponent(SelectDeckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should throw an error if no deck is selected when calling onSelect', () => {
    expect(() => {
      component.onSelect();
    }).toThrowError('Debe seleccionar un escuadrón de batalla');
  });


});
