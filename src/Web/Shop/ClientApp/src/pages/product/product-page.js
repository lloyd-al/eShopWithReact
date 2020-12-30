import React from 'react';
import { useLocation } from "react-router-dom";

import CategoryProducts from '../../components/catalog/category-products';

import './product-page-style.css';

const ProductPage = () => {
    const location = useLocation();

    return (
        <div>
            <CategoryProducts categoryId={location.state.id} categoryName={location.state.categoryName} />
        </div>
    );
};


export default ProductPage;
