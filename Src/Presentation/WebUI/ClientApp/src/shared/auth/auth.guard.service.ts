import { HttpErrorResponse, HttpResponseBase } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CanActivate, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router,
    private oauthService: OAuthService,
    private snackBar: MatSnackBar) { 
    }
    canActivate() {
        if(this.oauthService.hasValidAccessToken()) {
          return true;
        }else {
          sessionStorage.clear();
          this.router.navigate(['/']);
          return false; 
        }
    }
    check(error: HttpErrorResponse){
      console.error(error)
      if(error.status === 401){
        sessionStorage.clear();
        this.router.navigate(['/'])
      }
      this.snackBar.open("ERROR! Something went wrong!", "OK", {duration:5000})
    }
    getAccessToken(){
      return this.oauthService.getAccessToken();
    }
    getIdToken(){
      return this.oauthService.getIdToken();
    }
}
