import React, { useState, useCallback } from 'react';

function Tabs({ children }) {
    const initialTab = children[0].props.label
    const [activeTab, setActiveTab] = useState(initialTab)
    const handleActiveTab = useCallback(label => setActiveTab(label), [])

    const tabs = children.map(child => (
        <button
            onClick={e => {
                e.preventDefault();
                handleActiveTab(child.props.label);
            }}
            className={
                child.props.label === activeTab
                    ? ["tabs__tab", "tabs__tab-active"].join(" ")
                    : "tabs__tab"
            }
            key={child.props.label}
        >
            {child.props.tabName}
        </button>
    ))

    const tabContent = children.filter(child => child.props.label === activeTab)

    return (
        <div>
            <div className="tabs__box">{tabs}</div>
            <div>{tabContent}</div>
        </div>
    );
}

function Tab(props) {
    return <>{props.children}</>
}

export { Tabs, Tab };