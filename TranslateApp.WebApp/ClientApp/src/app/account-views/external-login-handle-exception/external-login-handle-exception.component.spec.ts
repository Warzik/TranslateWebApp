import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalLoginHandleExceptionComponent } from './external-login-handle-exception.component';

describe('ExternalLoginHandleExceptionComponent', () => {
  let component: ExternalLoginHandleExceptionComponent;
  let fixture: ComponentFixture<ExternalLoginHandleExceptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExternalLoginHandleExceptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExternalLoginHandleExceptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
