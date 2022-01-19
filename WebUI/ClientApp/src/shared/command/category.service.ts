import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICreateCategoryCommand } from '../entities/Category/icreate-category-command';
import { IDeleteCategoryCommand } from '../entities/Category/idelete-category-command';
import { IGetCategoriesQuery } from '../entities/Category/iget-categories-query';
import { IUpdateCategoryCommand } from '../entities/Category/iupdate-category-command';
import { ICategoryItems } from '../interfaces/icategory-items';
import angular from '../../assets/angular.json'
import { HttpClient } from '@angular/common/http';
import { AuthGuardService } from '../auth/auth.guard.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService implements ICategoryItems{
  base_url = angular.Authority+'/api/Category'
  constructor(private http: HttpClient,
    private auth: AuthGuardService) { }
  getCategories(): Observable<IGetCategoriesQuery> {
    return this.http.get<IGetCategoriesQuery>(this.base_url );
  }
  create(command: ICreateCategoryCommand): Observable<string> {
    return this.http.post(this.base_url, command, {
      responseType: 'text'
    });
  }
  update(command: IUpdateCategoryCommand): void {
    this.http.put(this.base_url, command).subscribe({
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    })
  }
  delete(command: IDeleteCategoryCommand): void {
    this.http.delete(this.base_url+'/'+command.id).subscribe({
      error: (e) => {
        this.auth.canActivate()
        console.error(e)
      }
    });
  }
}
