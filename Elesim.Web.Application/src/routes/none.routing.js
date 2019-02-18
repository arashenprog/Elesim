import React from "react";
import { Route } from "react-router-dom";
import NoneLayout from "../layouts/none.layout"

const NoneRouting = ({ component: Component, ...rest }) => {
    return (
        <Route
            {...rest}
            render={matchProps => (
                <NoneLayout>
                    <Component {...matchProps} />
                </NoneLayout>
            )}
        />
    );
};

export default NoneRouting;
