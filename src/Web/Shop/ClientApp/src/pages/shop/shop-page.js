import React from 'react';
import { Route } from 'react-router-dom';

import ProductList from '../../components/catalog/product-list';
import ProductPage from '../product/product-page';

const ShopPage = ({ match }) => (
    <div>
        <Route exact path={`${match.path}`} component={ProductList} />
        <Route path={`${match.path}/:categoryName`} component={ProductPage} />
    </div>
);

export default ShopPage;