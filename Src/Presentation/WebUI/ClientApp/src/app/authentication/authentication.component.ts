import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
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
    private profileService: ProfileService) { 
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
    this.profileService.create({
      'username': registerFormValue.login,
      'password': registerFormValue.password
    } as ICreateProfileCommand).subscribe({
      error: (e) => this.error = e.error
    })
  }
}
