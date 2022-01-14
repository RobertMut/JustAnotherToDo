import { IProfile } from "./iprofile";

export interface IProfilesPaged {
    items: IProfile[]
    pageNumber: number,
    pageIndex: number,
    totalPages: number,
    totalCount: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}
