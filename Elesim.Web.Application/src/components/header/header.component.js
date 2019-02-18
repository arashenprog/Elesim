import React, { PureComponent } from "react";

import HeaderMobile from "./headermobile.component";
import HeaderDesktop from "./headerdesktop.component";

class HeaderComponent extends PureComponent {
  render() {
    return (
      <div>
        <HeaderMobile />
        <HeaderDesktop />
      </div>
    );
  }
}

export default HeaderComponent;
