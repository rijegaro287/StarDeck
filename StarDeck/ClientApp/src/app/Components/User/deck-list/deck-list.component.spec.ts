import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckListComponent } from './deck-list.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('DeckListComponent', () => {
  let component: DeckListComponent;
  let fixture: ComponentFixture<DeckListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule],
      declarations: [ DeckListComponent ],
      providers:[DeckListComponent, { provide: 'BASE_URL', useValue: 'http://localhost'}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeckListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
