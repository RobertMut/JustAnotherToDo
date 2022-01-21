import { Observable } from "rxjs";
import { ICreateToDoCommand } from "../entities/ToDo/icreate-to-do-command";
import { IDeleteToDoCommand } from "../entities/ToDo/idelete-to-do-command";
import { IGetToDosQuery } from "../entities/ToDo/iget-to-dos-query";
import { IUpdateToDoCommand } from "../entities/ToDo/iupdate-to-do-command";

export interface ITodoItems {
    getTodo(): Observable<IGetToDosQuery>;
    create(command: ICreateToDoCommand): Observable<string>;
    update(command: IUpdateToDoCommand): void;
    delete(command: IDeleteToDoCommand): void;
}
