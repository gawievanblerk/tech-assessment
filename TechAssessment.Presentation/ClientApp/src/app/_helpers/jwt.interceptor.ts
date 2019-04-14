import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthenticationService } from '../core/authentication/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let credentials = this.authenticationService.credentials;

    if (credentials && credentials.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${credentials.token}`
        }
      });
    }
    return next.handle(request);
  }
}
