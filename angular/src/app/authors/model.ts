export interface CreateUpdateAuthorDto {
    id? : number;
    authorName : string;
    status :boolean;
}
export interface AuthorDto{
    id? : number;
    authorName : string;
    status : boolean;
}
export interface AuthorFilter{
    authorName?: string;
    status?: boolean;
    fromDate?: string;
    toDate?: string;
}
