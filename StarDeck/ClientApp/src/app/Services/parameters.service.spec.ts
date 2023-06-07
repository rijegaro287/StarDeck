import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { ParametersService } from './parameters.service';

describe('ParametersService', () => {
  let service: ParametersService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ParametersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
