import { TestBed } from '@angular/core/testing';

import { GameServices } from './game.services';

describe('GameServices', () => {
  let service: GameServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
