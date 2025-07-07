import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const canAcivateGuard: CanActivateFn = (route, state) => {
 const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
  const router = inject(Router);

  if (isLoggedIn) {
    return true;
  } else {
    // Redirect to login if not logged in
    return router.createUrlTree(['/login']);
  }
};
