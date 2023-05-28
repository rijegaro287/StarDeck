import { HttpClientTestingModule } from '@angular/common/http/testing';
import { InjectionToken } from '@angular/core';
import { TestBed } from '@angular/core/testing';

import { LoginService } from './login.service';
import {HttpClient} from "@angular/common/http";

export const BASE_URL = new InjectionToken<string>('BASE_URL');
describe('LoginService', () => {
  let service: LoginService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [LoginService, { provide: 'BASE_URL', useValue: 'http://localhost'}]
    });
    service = TestBed.inject(LoginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
