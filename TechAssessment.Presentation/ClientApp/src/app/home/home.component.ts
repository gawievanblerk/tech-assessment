import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { AuthenticationService } from '@app/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  isLoading: boolean;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit() {
    this.isLoading = true;
    this.isLoading = false;
  }

  get fullName(): string {
    const credentials = this.authenticationService.credentials;
    return credentials ? credentials.fullName : null;
  }
}
