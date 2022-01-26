import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { CdkTableModule } from '@angular/cdk/table';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon'
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatExpansionModule } from '@angular/material/expansion';
import { ColorPickerModule } from 'ngx-color-picker';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
//auth
import {
  OAuthModule,
  OAuthStorage
} from 'angular-oauth2-oidc';
import { AuthConfig } from 'angular-oauth2-oidc';
import { authConfig } from './auth/auth-config.module'

import angular from '../assets/angular.json';
import { OauthRedirectComponent } from './oauth-redirect/oauth-redirect.component';
import { ProfilesComponent } from './profiles/profiles.component';
import { ProfileComponent } from './profile/profile.component'

const MODULES = [
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatCardModule,
  MatTableModule,
  CdkTableModule,
  MatSelectModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatIconModule,
  MatMenuModule,
  MatExpansionModule,
  ColorPickerModule,
  MatToolbarModule,
  MatPaginatorModule,
  MatSnackBarModule,
  MatTabsModule,
  MatListModule
]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AuthenticationComponent,
    OauthRedirectComponent,
    ProfilesComponent,
    ProfileComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AuthenticationComponent, pathMatch: 'full'},
      { path: 'oauth-redirect', component: OauthRedirectComponent},
      { path: 'home', component: HomeComponent },
      { path: 'profiles', component: ProfilesComponent },
      { path: 'profile', component: ProfileComponent }
    ]),
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: [angular.Authority+'/api/'],
        sendAccessToken: true
      }
    }),
    BrowserAnimationsModule,
    //material
    MODULES,
  ],
  exports: [
    MODULES
  ],
  providers: [
        {provide: AuthConfig, useValue: authConfig },
        {provide: OAuthStorage, useValue: sessionStorage }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
