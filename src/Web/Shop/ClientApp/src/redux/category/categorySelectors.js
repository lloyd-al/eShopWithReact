import { createSelector } from 'reselect';

const selectCategory = state => state.categories;

const selectCategoryCollection = () => createSelector(
    selectCategory,
    categoryState => categoryState.categories
);


const selectCategoryLoading = () => createSelector(
    selectCategory,
    categoryState => categoryState.loading
);

const selectCategoryError = () => createSelector(
    selectCategory,
    categoryState => categoryState.error
);

export {
    selectCategory,
    selectCategoryCollection,
    selectCategoryLoading,
    selectCategoryError
};

