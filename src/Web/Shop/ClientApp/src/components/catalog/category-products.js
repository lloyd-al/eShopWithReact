import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { compose } from 'redux';
import { createStructuredSelector } from 'reselect';

import ProductItem from './product-item';
import { selectProductCollection, selectProductLoading, selectProductError } from '../../redux/product/productSelectors';
import { getCategoryProducts } from '../../redux/product/productActions';

import './catalog-main.css';

const CategoryProducts = (props) => {
    const { products, loading, error, categoryId, categoryName, getCategoryProducts } = props;

    useEffect(() => {
        getCategoryProducts(categoryId)
    }, [getCategoryProducts, categoryId])


    return (
        <div>
            { loading ? (
                <p> <em>Loading...</em></p >
            ) : error ? (
                <p><em>Oops... Something Went Wrong</em></p>
                ) : (
                        <div className='product-preview'>
                            <div className='product-preview_title'>{ categoryName }</div>
                            <div className='product-preview_body'>
                                {products
                                    .map(product => (
                                        <ProductItem key={product.id} product={product} />
                                    ))}
                            </div>
                        </div>
                    )
            }
        </div>
    );
};


const mapStateToProps = createStructuredSelector({
    products: selectProductCollection(),
    loading: selectProductLoading(),
    error: selectProductError()
});


const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        getCategoryProducts: categoryId => dispatch(getCategoryProducts(ownProps.categoryId))
    };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default compose(withConnect)(CategoryProducts);
