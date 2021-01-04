import React from "react"
import { Tabs, Tab } from "./Tabs"

const AuthTabPage = () => {
    return (
        <main className="main">
            <h2 className="pageName">Channels</h2>

            <Tabs>
                <Tab label={"recommended"} tabName={"Recommended"}>
                    <p>Recommended channels for you</p>
                </Tab>
                <Tab label={"subscribed"} tabName={"Subscribed"}>
                    <p>You haven't subscribed to any channel</p>
                </Tab>
            </Tabs>

        </main>
    );
}

export default AuthTabPage;