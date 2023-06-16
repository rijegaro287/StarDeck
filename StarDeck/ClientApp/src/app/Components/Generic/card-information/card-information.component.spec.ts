import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardInformationComponent } from './card-information.component';

describe('CardInformationComponent', () => {
  let component: CardInformationComponent;
  let fixture: ComponentFixture<CardInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardInformationComponent ],
      providers:[CardInformationComponent, { provide: 'BASE_URL', useValue: 'http://localhost'}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
