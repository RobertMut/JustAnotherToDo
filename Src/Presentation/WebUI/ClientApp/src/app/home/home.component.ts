import { CdkColumnDef } from '@angular/cdk/table';
import { Component } from '@angular/core';
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


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [ToDoService, CdkColumnDef]
})
export class HomeComponent {

  displayedColumns: string[] = ['name']
  displayedCategoriesColumn: string[] = ['name']
  dataSource!: IToDo[];
  categories!: ICategory[];
  todoLoaded: boolean = false;
  categoryLoaded: boolean = false;
  color: any;
  expanded: boolean = false
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
          this.auth.check(e)
        },
        complete: () => {
          this.getToDos()
          this.openSnackBar("Created new To-Do!")
          this.expanded = false;
        }
      })
      
    }
    onCategory(categoryForm: any): void{
      this.categoryService.create({
        'name': categoryForm.name,
        'color': this.color
      }as ICreateCategoryCommand).subscribe({
        error: (e) => {
          this.auth.check(e)
        },
        complete: () =>{
          this.getCategories();
          this.openSnackBar("Created new category!");
          this.expanded = false;
        } 
      });
    }
    delete(todo: any){
      this.todoService.delete({
        'id': todo.id
      } as IDeleteToDoCommand).subscribe({
        error: (e) => {
          this.auth.check(e)
        },
        complete: () => {
          this.getToDos()
          this.openSnackBar("To-Do deleted. Now, you can easily add another!")
        }
      })
      todo.deleted = true;
    }
    deleteCategory(category: any){
      this.categoryService.delete({
        'id': category.id
      } as IDeleteCategoryCommand).subscribe({
        error: (e) => {
          this.auth.check(e)
        },
        complete: () =>{
          this.getCategories();
          this.getToDos();
          this.openSnackBar("Category deleted. Now, you can easily add another!");
          this.expanded = false;
        } 
      });
      category.deleted = true;
    }
    edit(todo: any){
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
            this.auth.check(e)
          },
          complete: () =>{
            this.openSnackBar("Changes applied!")
            
          } 
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
        error: (err) =>{
          this.auth.check(err);
          }
        });
    }
    getToDos(){
      this.todoService.getTodo().subscribe({
        next: (v: IGetToDosQuery) => {
          this.todoLoaded = false
          this.dataSource = v.todos
          this.todoLoaded = true;
        },
        error: (err: any) =>{
          this.auth.check(err)
          }
        })
    }
    openSnackBar(message: string){
      this.snackBar.open(message, "OK!", {
        duration: 5000
      });
    }
}
