import CategoryActionTypes from './categoryTypes';

const INITIAL_STATE = {
    loading: false,
    hasError: false,
    error: '',
    categories: []
}

const categoryReducer = (state = INITIAL_STATE, action) => {
    switch (action.type) {
        case CategoryActionTypes.GET_ALL_CATEGORY_REQUEST:
            return {
                ...state,
                loading: true
            };

        case CategoryActionTypes.GET_ALL_CATEGORY_SUCCESS:
            return {
                ...state,
                loading: false,
                hasError: false,
                categories: action.payload
            };

        case CategoryActionTypes.GET_ALL_CATEGORY_ERROR:
            return {
                ...state,
                loading: false,
                hasError: true,
                categories: [],
                error: action.payload
            };

        default:
            return state;
    }
}

export default categoryReducer;