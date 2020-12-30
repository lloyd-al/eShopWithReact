import { createSelector } from 'reselect';

const selectProduct = state => state.products;

const selectProductCollection = () => createSelector(
    selectProduct,
    productState => productState.products
);

const selectProductFilter = () => createSelector(
    [selectProduct],
    filterState => {
        return {
            currentPage: filterState.currentPage,
            totalPages: filterState.totalPages,
            pageSize: filterState.pageSize,
            totalCount: filterState.totalCount,
            hasPrevious: filterState.hasPrevious,
            hasNext: filterState.hasNext
        }
    }
);

const selectProductLoading = () => createSelector(
    [selectProduct],
    productState => productState.loading
);

const selectProductError = () => createSelector(
    [selectProduct],
    productState => productState.error
);

export {
    selectProduct,
    selectProductCollection,
    selectProductLoading,
    selectProductError,
    selectProductFilter
};