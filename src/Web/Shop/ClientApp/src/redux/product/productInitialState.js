 const ProductInitialState = {
    loading: false,
    hasError: false,
    error: '',
    categoryTitle: 'All Products',
    currentPage: 1,
    totalPages: 1,
    pageSize: 4,
    totalCount: 10,
    hasPrevious: false,
    hasNext: false,
    products: []
};

export default ProductInitialState;
