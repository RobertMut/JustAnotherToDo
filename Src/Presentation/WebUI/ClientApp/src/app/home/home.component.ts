import { CdkColumnDef } from '@angular/cdk/table';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthGuardService } from '../../shared/auth/auth.guard.service';
import { CategoryService } from '../../shared/command/category.service';
import { ToDoService } from '../../shared/command/to-do.service';
import { ICategory } from '../../shared/entities/Category/icategory';
import { ICreateCategoryCommand } from '../../shared/entities/Category/icreate-category-command';
import { IDeleteCategoryCommand } from '../../shared/entities/Category/idelete-category-command';
import { IGetCategoriesQuery } from '../../shared/entities/Category/iget-categories-query';
import { ICreateToDoCommand } from '../../shared/entities/ToDo/icreate-to-do-command';
import { IDeleteToDoCommand } from '../../shared/entities/ToDo/idelete-to-do-command';
import { IGetToDosQuery } from '../../shared/entities/ToDo/iget-to-dos-query';
import { IToDo } from '../../shared/entities/ToDo/ito-do';
import { IUpdateToDoCommand } from '../../shared/entities/ToDo/iupdate-to-do-command';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ThrowStmt } from '@angular/compiler';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [ToDoService, CdkColumnDef]
})
export class HomeComponent {

  displayedColumns: string[] = ['name']
  dataSource!: IToDo[];
  categories!: ICategory[];
  todoLoaded: boolean = false;
  categoryLoaded: boolean = false;
  color: any;
  constructor(private auth: AuthGuardService,
    private todoService: ToDoService,
    private categoryService: CategoryService,
    private snackBar: MatSnackBar){
      this.getToDos();
      this.getCategories();
    }
    onAdd(addForm: any): void {
      let date = null
      if(addForm.endDate) date = new Date(addForm.endDate).toISOString()
      this.todoService.create({
        'name': addForm.name,
        'endDate': date,
        'categoryId': addForm.categoryId,
      } as ICreateToDoCommand).subscribe({
        error: (e) => {
          this.auth.canActivate();
          console.error(e);
        },
        complete: () => {
          this.getToDos()
          this.openSnackBar("Created new To-Do!")
        }
      })
      
    }
    onCategory(categoryForm: any): void{
      this.categoryService.create({
        'name': categoryForm.name,
        'color': this.color
      }as ICreateCategoryCommand).subscribe({
        error: (e) => {
          this.auth.canActivate();
          console.error(e);
        },
        complete: () =>{
          this.getCategories();
          this.openSnackBar("Created new category!");
        } 
      });
    }
    delete(todo: any){
      this.todoService.delete({
        'id': todo.id
      } as IDeleteToDoCommand).subscribe({
        error: (e) => {
          this.auth.canActivate();
          this.openSnackBar(e);
          console.error(e);
        },
        complete: () => this.getToDos()
      })
      todo.deleted = true;
    }
    deleteCategory(category: any){
      this.categoryService.delete({
        'id': category.id
      } as IDeleteCategoryCommand).subscribe({
        error: (e) => {
          this.auth.canActivate()
          this.openSnackBar(e)
          console.error(e)
        },
        complete: () => this.getCategories()
      });
      category.deleted = true;
    }
    edit(todo: any){
      console.warn(todo)
      if(todo.canEdit === undefined || todo.canEdit === false){
        todo.canEdit = true;
      } else {
        todo.canEdit = false;
        this.todoService.update({
          'id': todo.id,
          'name': todo.name,
          'endTime': todo.endTime,
          'categoryId': todo.categoryId
        } as IUpdateToDoCommand).subscribe({
          error: (e) => {
            this.auth.canActivate()
            console.error(e)
          },
          complete: () => this.getToDos()
        });
      }
    }
    visualChange(todo: any, category: any){
        todo.categoryId = category.id
        todo.color = category.color
        todo.category = category.name
    }
    getCategories(){
      this.categoryService.getCategories().subscribe({
        next: (v: IGetCategoriesQuery) => {
          this.categories = v.categories
          this.categoryLoaded = true;
        },
        error: (err: any) =>{
          console.error(err);
          
          this.auth.canActivate();
          }
        });
    }
    getToDos(){
      this.todoService.getTodo().subscribe({
        next: (v: IGetToDosQuery) => {
          this.dataSource = v.todos
          this.todoLoaded = true;
        },
        error: (err: any) =>{
          console.error(err);
          this.auth.canActivate();
          }
        })
    }
    openSnackBar(message: string){
      this.snackBar.open(message, "OK!");
    }
}
