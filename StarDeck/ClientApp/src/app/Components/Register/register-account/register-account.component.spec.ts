import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterAccountComponent } from './register-account.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('RegisterAccountComponent', () => {
  let component: RegisterAccountComponent;
  let fixture: ComponentFixture<RegisterAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ RegisterAccountComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
