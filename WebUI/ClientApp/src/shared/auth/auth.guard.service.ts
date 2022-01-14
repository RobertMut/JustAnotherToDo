import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router,
    private oauthService: OAuthService) { 
    }
    canActivate() {
        if(this.oauthService.hasValidAccessToken() &&
        this.oauthService.hasValidIdToken()
        ) return true;
        else {
          this.oauthService.revokeTokenAndLogout();
          this.router.navigate(['']);
          return false; 
        }

    }
    getAccessToken(){
      return this.oauthService.getAccessToken();
    }
    getIdToken(){
      return this.oauthService.getIdToken();
    }
}
