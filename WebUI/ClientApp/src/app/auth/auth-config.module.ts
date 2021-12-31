import { NgModule } from '@angular/core';
import { AuthConfig } from 'angular-oauth2-oidc';
import angular from '../../assets/angular.json'

export const authConfig: AuthConfig = {
    issuer: angular.Authority,
    redirectUri: window.location.origin + '/index.html',
    clientId: angular.ClientId,
    oidc: true,
    dummyClientSecret: angular.ClientSecret,
    //responseType: "id_token token",
    scope: angular.Scope,
    showDebugInformation: true
}