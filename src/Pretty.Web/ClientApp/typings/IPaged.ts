interface IPaged<T> {
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  data: T[];
}

export default IPaged;