import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCarriersComponent } from './my-carriers.component';

describe('MyCarriersComponent', () => {
  let component: MyCarriersComponent;
  let fixture: ComponentFixture<MyCarriersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyCarriersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCarriersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
