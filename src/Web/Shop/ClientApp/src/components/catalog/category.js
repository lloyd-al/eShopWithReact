import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { compose } from 'redux';
import { createStructuredSelector } from 'reselect';

import { selectCategoryCollection, selectCategoryLoading, selectCategoryError } from '../../redux/category/categorySelectors';
import { getAllCategories } from '../../redux/category/categoryActions'; 
import CategoryItem from './category-item';

import './category-style.css';

const Categories = (props) => {
    const { categories, loading, error, getAllCategories } = props;

    useEffect(() => {
        getAllCategories()
    }, [getAllCategories])  


    return (
        <div>
            { loading ? (
                <p><em>Loading...</em></p>
            ) : error ? (
                    <p><em>Oops... Something Went Wrong</em></p>
                ) : (
                        <div className='category-menu'>
                            { categories && categories.map(({ id, ...otherSectionProps }) => (
                                <CategoryItem key={id} id={id} {...otherSectionProps} />
                            ))}
                        </div>
                    )
            }
        </div>
    );  
}

//const mapStateToProps = state => {
//    return {
//        categories: state.categories
//    }
//}

const mapStateToProps = createStructuredSelector({
    categories: selectCategoryCollection(),
    loading: selectCategoryLoading(),
    error: selectCategoryError()

})

const mapDispatchToProps = dispatch => {
    return {
        getAllCategories: () => dispatch(getAllCategories())
    }
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default compose(withConnect)(Categories) 
