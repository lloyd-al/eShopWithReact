import ProductActionTypes from './productTypes';

export const getAllProducts = (currentPage, pageSize) => ({
    type: ProductActionTypes.GET_ALL_PRODUCTS,
    currentPage,
    pageSize
});

export const getCategoryProducts = (categoryId) => ({
    type: ProductActionTypes.GET_CATEGORY_PRODUCTS,
    categoryId
});

export const getProductsSuccess = data => ({
    type: ProductActionTypes.GET_PRODUCTS_SUCCESS,
    payload: data
});

export const getProductsError = error => ({
    type: ProductActionTypes.GET_PRODUCTS_ERROR,
    payload: error
});