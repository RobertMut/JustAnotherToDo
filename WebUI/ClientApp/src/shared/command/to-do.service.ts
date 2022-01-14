import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICreateToDoCommand } from '../entities/ToDo/icreate-to-do-command';
import { IDeleteToDoCommand } from '../entities/ToDo/idelete-to-do-command';
import { IGetToDosQuery } from '../entities/ToDo/iget-to-dos-query';
import { IUpdateToDoCommand } from '../entities/ToDo/iupdate-to-do-command';
import { ITodoItems } from '../interfaces/itodo-items';
import angular from '../../assets/angular.json'
import { AuthGuardService } from '../auth/auth.guard.service';
@Injectable({
  providedIn: 'root'
})
export class ToDoService implements ITodoItems {
  base_url = angular.Authority+'/api/ToDo'
  constructor(private http: HttpClient,
    private auth: AuthGuardService) { }
  getTodo(): Observable<IGetToDosQuery> {
      return this.http.get<IGetToDosQuery>(this.base_url );
  }
  create(command: ICreateToDoCommand): Observable<string> {
    return this.http.post(this.base_url, command, {
      responseType: 'text'
    });
  }
  update(command: IUpdateToDoCommand): void {
    this.http.put(this.base_url, command).subscribe({
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    })
  }
  delete(command: IDeleteToDoCommand): void {
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
