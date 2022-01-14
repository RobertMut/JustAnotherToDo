import { Observable } from "rxjs";
import { ICreateProfileCommand } from "../entities/Profile/icreate-profile-command";
import { IDeleteProfileCommand } from "../entities/Profile/idelete-profile-command";
import { IProfile } from "../entities/Profile/iprofile";
import { IProfilesPaged } from "../entities/Profile/iprofiles-paged";
import { IUpdateProfileCommand } from "../entities/Profile/iupdate-profile-command";

export interface IProfileItems {
    getPaged(): Observable<IProfilesPaged>;
    getProfile(): Observable<IProfile>;
    create(command: ICreateProfileCommand): Observable<string>;
    update(command: IUpdateProfileCommand): void;
    delete(command: IDeleteProfileCommand): void;
}
