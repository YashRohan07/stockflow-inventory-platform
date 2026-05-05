import { TestBed } from '@angular/core/testing';

import { App } from './app';

// Basic smoke test for the root application component.
// Ensures the app component can be created successfully.
describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;

    expect(app).toBeTruthy();
  });
});
