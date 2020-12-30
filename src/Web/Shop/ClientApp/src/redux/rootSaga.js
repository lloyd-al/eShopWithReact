import { all, call } from 'redux-saga/effects';

import { onGetAllProducts, onGetCategoryProducts } from './product/productSagas';

export default function* rootSaga() {
    yield all([
        call(onGetAllProducts),
        call(onGetCategoryProducts)
    ]);
}