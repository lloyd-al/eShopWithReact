import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

import CustomButton from '../custom-elements/custom-button';
import CartItem from './cart-item';
import { toggleCartHidden } from '../../redux/cart/cart-actions';

import './cart-dropdown-style.css';

const CartDropdown = ({ cartItems, history, dispatch }) => {
    return (
        <div className='cart-dropdown'>
            <div className='cart-items'>
                {cartItems.length ? (
                    cartItems.map(cartItem => (
                        <CartItem key={cartItem.id} item={cartItem} />
                    ))
                ) : (
                        <span className='empty-message'>Your cart is empty</span>
                    )}
            </div>
            <CustomButton onClick={() => {
                history.push('/checkout');
                dispatch(toggleCartHidden());
            }}>CHECKOUT</CustomButton>
        </div>
    );
}

const mapStateToProps = ({ cart: { cartItems } }) => ({
    cartItems
});

export default withRouter(connect(mapStateToProps)(CartDropdown));