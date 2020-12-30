import React from 'react';

import './cart-item-style.css';

const CartItem = ({ item: { imageUrl, price, productName, quantity } }) => (
    <div className='cart-item'>
        <img src={imageUrl} alt='item' />
        <div className='item-details'>
            <span className='name'>{productName}</span><br />
            <span className='price'>{quantity} x ${price}</span>
        </div>
    </div>
);

export default CartItem;