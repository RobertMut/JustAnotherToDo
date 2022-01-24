import { Observable } from "rxjs";
import { ICreateCategoryCommand } from "../entities/Category/icreate-category-command";
import { IDeleteCategoryCommand } from "../entities/Category/idelete-category-command";
import { IGetCategoriesQuery } from "../entities/Category/iget-categories-query";
import { IUpdateCategoryCommand } from "../entities/Category/iupdate-category-command";

export interface ICategoryItems {
    getCategories(): Observable<IGetCategoriesQuery>;
    create(command: ICreateCategoryCommand): Observable<string>;
    update(command: IUpdateCategoryCommand): Observable<string>;
    delete(command: IDeleteCategoryCommand): Observable<string>;
}
