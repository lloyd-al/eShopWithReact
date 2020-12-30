import React from 'react';
import { connect } from 'react-redux';

import CustomButton from '../custom-elements/custom-button';
import { addItem } from '../../redux/cart/cart-actions';

import './product-item-style.css';

const ProductItem = ({ product, addItem  }) => {
    const { productName, price, imageUrl } = product;

    return (
        <div className='collection-item'>
            <div className='image' style={{ backgroundImage: `url(${imageUrl})` }} />
            <div className='collection-footer'>
                <span className='name'>{productName}</span>
                <span className='price'>{price}</span>
            </div>
            <CustomButton onClick={() => addItem(product)} inverted>Add to Cart</CustomButton>
        </div>
    );
}

const mapDispatchToProps = dispatch => ({
    addItem: product => dispatch(addItem(product))
});

export default connect(null, mapDispatchToProps)(ProductItem);
