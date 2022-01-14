import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';

@Component({
  selector: 'app-oauth-redirect',
  templateUrl: './oauth-redirect.component.html',
  styleUrls: ['./oauth-redirect.component.css']
})
export class OauthRedirectComponent implements OnInit {

  constructor(private oauth: OAuthService,
    private router: Router) {
    this.oauth.tokenValidationHandler = new JwksValidationHandler();
    this.oauth.loadDiscoveryDocumentAndTryLogin();
    this.oauth.tryLoginImplicitFlow();
    this.router.navigate(['/home'])
   }

  ngOnInit(): void {
  }

}
