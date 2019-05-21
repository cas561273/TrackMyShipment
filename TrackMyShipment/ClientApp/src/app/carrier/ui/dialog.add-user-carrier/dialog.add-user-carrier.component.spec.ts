import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAddUserCarrierComponent } from './dialog.add-user-carrier.component';

describe('Dialog.AddUserCarrierComponent', () => {
  let component: DialogAddUserCarrierComponent;
  let fixture: ComponentFixture<DialogAddUserCarrierComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogAddUserCarrierComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogAddUserCarrierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
