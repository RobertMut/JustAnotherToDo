import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isValid = false;
  constructor(private oauthService: OAuthService,
    private router: Router){
      router.events.subscribe((event) => {
        if (event instanceof NavigationStart) {
          this.isValid = sessionStorage.getItem('access_token') !== null
        }
      });

  }
  ngOnInit(): void {
  }
  


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout(){
    this.oauthService.revokeTokenAndLogout({
      'client_id': this.oauthService.clientId,
      //'id_token_hint': this.oauthService.getIdToken(),
      'post_logout_redirect_uri': this.oauthService.postLogoutRedirectUri,
    })
    this.isValid = false;
    this.router.navigate(['/'])
  }
}
