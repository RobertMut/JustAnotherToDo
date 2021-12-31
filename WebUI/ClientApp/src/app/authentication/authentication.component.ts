import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from "../auth/auth-config.module"

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {
  constructor(private oauthService: OAuthService) { 
  }

  ngOnInit(): void {

  }
  async onLogin(loginFormValue: any) {
    //this.oauthService.configure(authConfig);
    await this.oauthService.loadDiscoveryDocument();
    //sessionStorage.setItem('flow', 'implicit');
    this.oauthService.initLoginFlow();
  }
  onRegister(registerFormValue: any): void {
    console.warn(registerFormValue.login);
    console.warn(registerFormValue.pass);
    console.warn(registerFormValue.repeatedpass);
  }
}
