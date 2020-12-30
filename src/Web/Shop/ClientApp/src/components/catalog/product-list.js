import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { compose } from 'redux';
import { createStructuredSelector } from 'reselect';

import { selectProductCollection, selectProductLoading, selectProductError, selectProductFilter } from '../../redux/product/productSelectors';
import { getAllProducts } from '../../redux/product/productActions';
import ProductItem from './product-item';
import Pagination from '../custom-elements/pagination';

import './catalog-main.css';

const ProductList = (props) => {

    const { productFilter, products, loading, error, getPagedProducts } = props;
    console.log(productFilter);
    const { currentPage, totalPages, pageSize } = productFilter;

    useEffect(() => {
        getPagedProducts(currentPage, pageSize)
    }, [getPagedProducts, currentPage, pageSize])

    const handlePageClick = data => {
        getPagedProducts(data, pageSize)
    }

    return (
        <div>
            { loading ? (
                <p> <em>Loading...</em></p >
            ) : error ? (
                    <p><em>Oops... Something Went Wrong</em></p>
                ) : (
                        <div className="container">
                            <div className='product-preview'>
                                <div className='product-preview_body'>
                                    {products
                                        .map(product => (
                                            <ProductItem key={product.id} product={product} />
                                        ))}
                                </div>
                            </div>
                            <div className="col-sm-6">
                                <Pagination currentPage={currentPage} totalPages={totalPages} pageSize={pageSize} handlePagination={handlePageClick} />
                            </div>
                        </div>
                    )
            }
        </div>
    );
}

const mapStateToProps = createStructuredSelector({
    productFilter: selectProductFilter(),
    products: selectProductCollection(),
    loading: selectProductLoading(),
    error: selectProductError()
});


const mapDispatchToProps = dispatch => ({
        getPagedProducts: (currentPage, pageSize) => dispatch(getAllProducts(currentPage, pageSize))
});

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default compose(withConnect)(ProductList);