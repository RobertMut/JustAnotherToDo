import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../shared/command/profile.service';
import { IProfilesPaged } from '../../shared/entities/Profile/iprofiles-paged';
import { AccessLevel } from '../../shared/enum/access-level';
import { PageEvent } from '@angular/material/paginator';
import { IDeleteProfileCommand } from '../../shared/entities/Profile/idelete-profile-command';
import { IUpdateProfileCommand } from '../../shared/entities/Profile/iupdate-profile-command';
import { AuthGuardService } from '../../shared/auth/auth.guard.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {
  loaded = false;
  levels = AccessLevel
  displayedColumns: string[] = ['id', 'name', 'accesslevel']
  dataSource!: IProfilesPaged //= {} as IProfilesPaged;
  pageSize = 10;
  pageNumber = 0;
  
  constructor(private profileService: ProfileService,
    private authService: AuthGuardService,
    private snackBar: MatSnackBar) {
    profileService.getPaged().subscribe({
      next: (n) => {
        this.dataSource = n;
        this.loaded = true;
      },
      error: (e) => {
        console.error(e)
        this.authService.canActivate();
      }
    })
   }

  ngOnInit(): void {
  }
  public getData(event: PageEvent){
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex;
    this.profileService.getPaged(event.pageIndex+1, event.pageSize).subscribe({
      next: (n) => {
        this.dataSource = n;
      },
      error: (e) => {
        console.error(e)
        this.authService.canActivate()
      }
    })
  }
  public edit(element: any){
    console.warn(element)
    if(element.canEdit === undefined || element.canEdit === false){
      element.canEdit = true;
    } else {
      element.canEdit = false;
      this.profileService.update({
        'userId': element.userId,
        'password': element.pass,
        'username': element.username,
        'accessLevel': element.accessLevel
      } as IUpdateProfileCommand).subscribe({
        error: (e) => {
          this.authService.canActivate()
          this.openSnackBar("ERROR! Try again!");
          console.error(e)
        },
        complete: () => {
          this.getData({
            'pageIndex': this.pageNumber,
            'pageSize' : this.pageSize
          }as PageEvent)
          this.openSnackBar("Changes applied!");
        }
      });
    }
  }
  public delete(element: any){
    this.profileService.delete({
      'userId': element.userId
    } as IDeleteProfileCommand).subscribe({
      error: (e) => {
        this.authService.canActivate();
        this.openSnackBar("ERROR! Try again!");
        console.error(e)
      },
      complete: () => {
        this.openSnackBar("Changes applied!")
        this.getData({
          'pageIndex': this.pageNumber,
          'pageSize' : this.pageSize
        }as PageEvent)
      }
    })
    element.deleted = true;
  }
  private openSnackBar(message: string){
    this.snackBar.open(message, "Ok", { duration: 5000 })
  }
}
