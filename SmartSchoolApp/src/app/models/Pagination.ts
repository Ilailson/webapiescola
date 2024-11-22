export interface Pagination {
    currentPage: number;
    pageItems: number;
    itemsPerPage: number;
    totalPages: number;
}


export class PaginatedResult<T> {
    result!: T;
    pagination!: Pagination;
}