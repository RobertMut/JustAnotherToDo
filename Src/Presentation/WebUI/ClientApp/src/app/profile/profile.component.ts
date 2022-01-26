import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthGuardService } from '../../shared/auth/auth.guard.service';
import { ProfileService } from '../../shared/command/profile.service';
import { IProfile } from '../../shared/entities/Profile/iprofile';
import { IUpdateProfileCommand } from '../../shared/entities/Profile/iupdate-profile-command';
import { AccessLevel } from '../../shared/enum/access-level';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile: IProfile = {} as IProfile
  levels = AccessLevel
  constructor(private profileService: ProfileService,
    private auth: AuthGuardService,
    private snack: MatSnackBar) { 
    profileService.getProfile().subscribe({
      next: (v) => this.profile = v,
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    })


  }
  ngOnInit(): void {
  }
  onProfile(profileForm: any){
    if(profileForm.currentpass === "") this.snack.open("Please fill all fields!", "OK!", {duration: 5000})
    if(profileForm.currentpass === this.profile.password)
    {
      this.profileService.update({
        'userId': this.profile.id,
        'password': profileForm.pass,
        'username': this.profile.username
      } as IUpdateProfileCommand)
    }
    else {
      window.alert('Invalid current password!')
    }
  }

}
