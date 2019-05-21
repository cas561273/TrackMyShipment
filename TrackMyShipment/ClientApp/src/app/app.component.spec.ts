import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debuguser.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'TrackMyShipment'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debuguser.componentInstance;
    expect(app.title).toEqual('TrackMyShipment');
  });

  it('should render title in a h1 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debuguser.nativeuser;
    expect(compiled.querySelector('h1').textContent).toContain('Welcome to TrackMyShipment!');
  });
});
