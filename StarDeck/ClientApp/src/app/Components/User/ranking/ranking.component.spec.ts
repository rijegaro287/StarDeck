import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RankingComponent } from './ranking.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SelectionCardComponent } from '../../Register/selection-card/selection-card.component';
import { MatDialogRef } from '@angular/material/dialog';

describe('RankingComponent', () => {
  let component: RankingComponent;
  let fixture: ComponentFixture<RankingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [SelectionCardComponent],
      providers: [
        SelectionCardComponent,
        { provide: 'BASE_URL', useValue: 'http://localhost' },
        { provide: MatDialogRef, useValue: {} }
      ]

    })
      .compileComponents();

    fixture = TestBed.createComponent(RankingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
