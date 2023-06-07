import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMainComponent } from './user-main.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('UserMainComponent', () => {
  let component: UserMainComponent;
  let fixture: ComponentFixture<UserMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule ],
      declarations: [UserMainComponent ],
      providers:[UserMainComponent, { provide: 'BASE_URL', useValue: 'http://localhost'}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
