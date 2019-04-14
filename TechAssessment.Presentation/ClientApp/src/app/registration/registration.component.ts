import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { environment } from '@env/environment';
import { Logger, I18nService } from '@app/core';
import { UsersClient, UserViewModel, RegisterUserCommand } from '../tech-assessment-api';
const log = new Logger('Login');

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  version: string = environment.version;
  error: string;
  registrationForm: FormGroup;
  isLoading = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private i18nService: I18nService,
    private client: UsersClient
  ) {
    this.createForm();
  }

  ngOnInit() {}

  register() {
    this.isLoading = true;
    let user: UserViewModel = new UserViewModel({
      id : 0,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      username: this.username.value
    });
    let command = new RegisterUserCommand({
      user : user
    });
    this.client
      .register(command)
      .pipe(
        finalize(() => {
          this.registrationForm.markAsPristine();
          this.isLoading = false;
        })
      )
      .subscribe(
        resulat => {
          log.debug(`${this.username} successfully registered`);
          alert(`Username ${this.username.value} successfully registered`);
          this.route.queryParams.subscribe(params =>
            this.router.navigate([params.redirect || '/'], { replaceUrl: true })
          );
        },
        error => {
          log.debug(`Registration error: ${error}`);
          this.error = error;
        }
      );
  }

  setLanguage(language: string) {
    this.i18nService.language = language;
  }

  get currentLanguage(): string {
    return this.i18nService.language;
  }

  get languages(): string[] {
    return this.i18nService.supportedLanguages;
  }

  get firstName() {
    return this.registrationForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.registrationForm.get('lastName') as FormControl;
  }

  get username() {
    return this.registrationForm.get('username') as FormControl;
  }

  private createForm() {
    this.registrationForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.maxLength(60)]],
      lastName: ['', [Validators.required, Validators.maxLength(60)]],
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]]
    });
  }
}





