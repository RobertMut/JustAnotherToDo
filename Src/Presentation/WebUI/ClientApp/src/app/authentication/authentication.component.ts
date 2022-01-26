import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthGuardService } from 'src/shared/auth/auth.guard.service';
import { ProfileService } from '../../shared/command/profile.service';
import { ICreateProfileCommand } from '../../shared/entities/Profile/icreate-profile-command';
import { authConfig } from "../auth/auth-config.module"

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {
  constructor(private oauthService: OAuthService,
    private profileService: ProfileService,
    private auth: AuthGuardService,
    private router: Router,
    private snackBar: MatSnackBar) { 
      if(this.auth.canActivate()) router.navigate(["/home"]);
  }
  error: string = "";
  ngOnInit(): void {

  }
  async onLogin() {
    await this.oauthService.loadDiscoveryDocument();
    this.oauthService.initLoginFlow();
    this.oauthService.setupAutomaticSilentRefresh();
  }
  onRegister(registerFormValue: any): void {
    if(registerFormValue.login === "" && registerFormValue.password === ""){
      this.snackBar.open("Please fill form fields!", "OK", {
        duration: 5000
      })
    } else {
      this.profileService.create({
        'username': registerFormValue.login,
        'password': registerFormValue.password
      } as ICreateProfileCommand).subscribe({
        error: (e) => this.error = e.error,
        complete: () => this.snackBar.open("Successfully registered! Now log in!", "Ok!")
      })
    }
    }

}
