import React from 'react';
import { withRouter, useHistory } from 'react-router-dom';

import './category-item-style.css';

const CategoryItem = ({ id, categoryName, imageUrl }) => {
    let history = useHistory();

    return (
        <div className='large menu-item' onClick={() => history.push({
            pathname: `/shop/${categoryName}`,
            state: {id, categoryName}
        })}>
            <div className='menu-item_bg-image' style={{ backgroundImage: `url(${imageUrl})` }} />
            <div className='menu-item_content'>
                <h1 className='menu-item_title'>{categoryName.toUpperCase()}</h1>
                <span className='menu-item_subtitle'>SHOP NOW</span>
            </div>
        </div>
    );
}

export default withRouter(CategoryItem);