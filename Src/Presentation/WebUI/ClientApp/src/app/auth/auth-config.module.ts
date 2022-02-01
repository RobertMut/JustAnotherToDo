import { NgModule } from '@angular/core';
import { AuthConfig } from 'angular-oauth2-oidc';
import angular from '../../assets/angular.json'

export const authConfig: AuthConfig = {
    issuer: angular.Authority,
    redirectUri: window.location.origin + '/oauth-redirect/',
    clientId: angular.ClientId,
    oidc: true,
    dummyClientSecret: angular.ClientSecret,
    responseType: "id_token token",
    requestAccessToken: true,
    logoutUrl: angular.Authority + '/Account/Logout',
    postLogoutRedirectUri: window.location.origin,
    scope: angular.Scope,
    showDebugInformation: true,
}
