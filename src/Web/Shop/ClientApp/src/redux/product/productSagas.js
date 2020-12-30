import { takeEvery , put } from "redux-saga/effects";
import ProductActionTypes from './productTypes';
import { catalogInstance } from '../../axios-instance';

import { getProductsSuccess, getProductsError } from './productActions';

export function* onGetAllProducts() {
    yield takeEvery(ProductActionTypes.GET_ALL_PRODUCTS, getAllProductsAsync);
}

export function* getAllProductsAsync(action) {
    try {
        const response = yield catalogInstance.get(`v1/Product?PageSize=${action.pageSize}&PageNumber=${action.currentPage}`)
        const data = yield response.data
        yield put(getProductsSuccess(data));
    } catch (error) {
        yield put(getProductsError(error.message));
    }
}

export function* onGetCategoryProducts() {
    yield takeEvery(ProductActionTypes.GET_CATEGORY_PRODUCTS, getCategoryProductsAsync);
}

export function* getCategoryProductsAsync(action) {
    try {
        const response = yield catalogInstance.get(`v1/Product/GetProductsByCategory/${action.categoryId}`)
        const data = yield response.data
        yield put(getProductsSuccess(data));
    } catch (error) {
        yield put(getProductsError(error.message));
    }
}