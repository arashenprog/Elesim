import React from "react";

import Header from "../components/header/header.component";
import Footer from "../components/footer/footer.component";

let HomeLayout = ({ children, ...rest }) => {
  return (
    <div>
      <Header/>
      {children}
      <Footer/>
    </div>
  );
};
export default HomeLayout;
