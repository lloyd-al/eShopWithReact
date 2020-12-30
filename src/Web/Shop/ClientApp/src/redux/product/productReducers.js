import ProductActionTypes from './productTypes';
import ProductInitialState from './productInitialState';

const productReducer = (state = ProductInitialState, action) => {
    switch (action.type) {
        case ProductActionTypes.GET_ALL_PRODUCTS:
            return {
                ...state,
                loading: true
            };

        case ProductActionTypes.GET_CATEGORY_PRODUCTS:
            return {
                ...state,
                loading: true
            };

        case ProductActionTypes.GET_PRODUCTS_SUCCESS:
            return {
                ...state,
                loading: false,
                hasError: false,
                currentPage: action.payload.currentPage, 
                totalPages: action.payload.totalPages,
                pageSize: action.payload.pageSize,
                totalCount: action.payload.totalCount,
                hasPrevious: action.payload.hasPrevious,
                hasNext: action.payload.hasNext,
                products: action.payload.data
            };

        case ProductActionTypes.GET_PRODUCTS_ERROR:
            return {
                ...state,
                loading: false,
                hasError: true,
                currentPage: 1,
                totalPages: 1,
                pageSize: 1,
                totalCount: 1,
                hasPrevious: false,
                hasNext: false,
                products: [],
                error: action.payload
            };

        default:
            return state;
    }
}

export default productReducer;