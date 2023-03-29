import { BaseEntityDto } from "../model";

export interface CreateUpdateBookDto {
    id? : number;
    authorName : string;
    status :boolean;
}
export interface BookDto extends BaseEntityDto<number>{
    bookName: string;
    bookIssueId?: number;
    issueDate: string | Date;
    issueTo?: number;
    issueToName?: string;
    issueBy?: number;
    issueByName?: string;
    returnDate?: string | Date;
    returnByName?: string;
    isInLibrary: boolean;
    authors?: BookAuthorDto;
    genres?: BookGenreDto;
}
export interface BookAuthorDto extends BaseEntityDto<number>{
   authorId: number; 
   authorName: string;
}
export interface BookGenreDto extends BaseEntityDto<number>{
   genreId: number;
   genreName: string; 
}

