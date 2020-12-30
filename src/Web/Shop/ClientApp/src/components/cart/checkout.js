import React from 'react';
import { connect } from 'react-redux';

import { clearItemFromCart, addItem, removeItem } from '../../redux/cart/cart-actions';

import './checkout-style.css';

const Checkout = ({ cartItem, clearItem, addItem, removeItem }) => {
    const { productName, imageUrl, price, quantity } = cartItem;

    return (
        <div className='checkout-item'>
            <div className='image-container'><img src={imageUrl} alt='item' /></div>
            <span className='name'>{productName}</span>
            <span className='quantity'>
                <span className='arrow' onClick={() => removeItem(cartItem)}>&#10094;</span>
                <span className='value'>{quantity}</span>
                <span className='arrow' onClick={() => addItem(cartItem)}>&#10095;</span>
            </span>
            <span className='price'>{price}</span>
            <div className='remove-button' onClick={() => clearItem(cartItem)}>&#10005;</div>
        </div>
    );

};

const mapDispatchToProps = dispatch => ({
    clearItem: item => dispatch(clearItemFromCart(item)),
    addItem: item => dispatch(addItem(item)),
    removeItem: item => dispatch(removeItem(item))
});

export default connect(null, mapDispatchToProps)(Checkout);