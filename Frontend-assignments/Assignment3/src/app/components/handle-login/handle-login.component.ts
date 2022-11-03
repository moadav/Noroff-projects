import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Collector } from 'src/app/models/collector.model';
import { CollectorService } from 'src/app/services/collector.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-handle-login',
  templateUrl: './handle-login.component.html',
  styleUrls: ['./handle-login.component.scss'],
})
export class HandleLoginComponent {
  @Output() login: EventEmitter<void> = new EventEmitter();
  @Output() registerAcc: EventEmitter<void> = new EventEmitter();

  // Variables
  public choices = 'Login';
  public loading: boolean = false;
  public defaultCheck: number = 0;
  public error: string = '';
  public buttons: boolean = false;
  public userExist: boolean = false;
  public userExistLogin: boolean = false;

  constructor(
    private readonly loginService: LoginService,
    private readonly collectorService: CollectorService
  ) {}
  /**
   * A function that logs the user and navigates to trainer page if it exists
   *
   * @param loginForm NgForm of the form component
   */

  public loginSubmit(loginForm: NgForm): void {
    this.loading = true;
    const { username } = loginForm.value;

    this.loginService.loginUser(username).subscribe({
      next: (collector: Collector) => {
        if (collector !== undefined) {
          this.collectorService.Collector = collector;
          this.userExistLogin = false;
          this.login.emit();
        } else this.userExistLogin = true;
      },

      error: (error: HttpErrorResponse) => {
        this.error = error.message;
      },
    });
    this.loading = false;
  }
  /**
   *
   * A function that checks which of the radio buttons the user has toggled on
   *
   * @param checked NgForm of the checked form
   */

  public checked(checked: NgForm) {
    this.userExist = false;
    this.userExistLogin = false;
    const { user_choice } = checked.value;
    if (user_choice === 'Login') {
      this.buttons = false;
    } else this.buttons = true;
  }

  /**
   *
   * A function to register the user to the API
   *
   * @param registerForm Register form of the NgForm
   */

  public register(registerForm: NgForm) {
    this.loading = true;

    const { username } = registerForm.value;

    this.loginService.createCollector(username).subscribe({
      next: (collector: Collector | undefined) => {
        if (collector === undefined) {
          this.userExist = true;

          return;
        } else {
          this.userExist = false;
          this.registerAcc.emit();
        }
      },
    });

    this.loading = false;
  }
}
