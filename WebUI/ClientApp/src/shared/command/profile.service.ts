import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICreateProfileCommand } from '../entities/Profile/icreate-profile-command';
import { IDeleteProfileCommand } from '../entities/Profile/idelete-profile-command';
import { IProfilesPaged } from '../entities/Profile/iprofiles-paged';
import { IUpdateProfileCommand } from '../entities/Profile/iupdate-profile-command';
import { IProfileItems } from '../interfaces/iprofile-items';
import angular from '../../assets/angular.json'
import { IProfile } from '../entities/Profile/iprofile';
import { HttpClient } from '@angular/common/http';
import { AuthGuardService } from '../auth/auth.guard.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService implements IProfileItems {
  base_url = angular.Authority+'/api/Profile'
  constructor(private http: HttpClient,
    private auth: AuthGuardService) { }
  getPaged(pageNumber: number = 1, pageSize: number = 10): Observable<IProfilesPaged> {
    return this.http.get<IProfilesPaged>(this.base_url+'?pageNumber=' +pageNumber + '&pageSize=' + pageSize);
  }
  getProfile(): Observable<IProfile> {
    return this.http.get<IProfile>(this.base_url+'/profile');
  }
  create(command: ICreateProfileCommand): Observable<string> {
    return this.http.post(this.base_url, command, {
      responseType: "text"
    });
  }
  update(command: IUpdateProfileCommand): void {
    this.http.put(this.base_url, command).subscribe({
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    })
  }
  delete(command: IDeleteProfileCommand): void {
    this.http.delete(this.base_url, {
      body: command
    }).subscribe({
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    });
  }
}
