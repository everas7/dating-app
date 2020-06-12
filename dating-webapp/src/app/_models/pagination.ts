export interface Pagination {
    page: number;
    perPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResponseEnvelope<T> {
    response: T;
    pagination: Pagination;
}
