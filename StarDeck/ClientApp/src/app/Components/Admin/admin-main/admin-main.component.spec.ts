import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMainComponent } from './admin-main.component';

describe('AdminMainComponent', () => {
  let component: AdminMainComponent;
  let fixture: ComponentFixture<AdminMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should contain navbarItems', () => {
    expect(component.navbarItems).toBeDefined();
    expect(component.navbarItems.length).toBeGreaterThan(0);
  });

  it('onPlayClicked should log "Play clicked"', () => {
    spyOn(console, 'log');
    component.onPlayClicked();
    expect(console.log).toHaveBeenCalledWith('Play clicked');
  });
});
