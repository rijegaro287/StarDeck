import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchOpponentComponent } from './search-opponent.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('SearchOpponentComponent', () => {
  let component: SearchOpponentComponent;
  let fixture: ComponentFixture<SearchOpponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [SearchOpponentComponent ],
      providers:[SearchOpponentComponent, { provide: 'BASE_URL', useValue: 'http://localhost'}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchOpponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
