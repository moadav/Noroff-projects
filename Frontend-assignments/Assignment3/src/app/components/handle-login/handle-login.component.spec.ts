import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HandleLoginComponent } from './handle-login.component';

describe('HandleLoginComponent', () => {
  let component: HandleLoginComponent;
  let fixture: ComponentFixture<HandleLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HandleLoginComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HandleLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
