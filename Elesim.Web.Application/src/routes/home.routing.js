import React from "react";
import { Route } from "react-router-dom";
import HomeLayout from "../layouts/home.layout";

const HomeRouting = ({ component: Component, ...rest }) => {
  return (
    <Route
      {...rest}
      render={matchProps => (
        <HomeLayout>
          <Component {...matchProps} />
        </HomeLayout>
      )}
    />
  );
};

export default HomeRouting;
