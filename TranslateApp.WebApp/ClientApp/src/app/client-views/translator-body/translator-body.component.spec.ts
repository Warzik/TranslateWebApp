import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslatorBodyComponent } from './translator-body.component';

describe('TranslatorBodyComponent', () => {
  let component: TranslatorBodyComponent;
  let fixture: ComponentFixture<TranslatorBodyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TranslatorBodyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TranslatorBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
