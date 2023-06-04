import { PagedResultResponseDto } from './PagedResultResponseDto';


export interface PagedAndSortedResultResponseDto extends PagedResultResponseDto{
    sorting? : string;
}