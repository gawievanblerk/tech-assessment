import { Observable, of } from 'rxjs';

import { Credentials, LoginContext } from './authentication.service';

export class MockAuthenticationService {
  credentials: Credentials | null = {
    username: 'test',
    fullName: 'Test User',
    token: '123',
    userId: 1
  };

  login(context: LoginContext): Observable<Credentials> {
    return of({
      username: context.username,
      fullName: '',
      token: '123456',
      userId: 1
    });
  }

  logout(): Observable<boolean> {
    this.credentials = null;
    return of(true);
  }

  isAuthenticated(): boolean {
    return !!this.credentials;
  }
}
