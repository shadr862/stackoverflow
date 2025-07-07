import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { canAcivateGuard } from './can-acivate.guard';

describe('canAcivateGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => canAcivateGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
