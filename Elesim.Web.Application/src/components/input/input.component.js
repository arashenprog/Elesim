import React, { Component } from "react";
import MaskedInput from "react-text-mask";
import { message, Row, Col, Input, Icon, Button, notification } from "antd";

class CustomInput extends Component {
  render() {
    return (
      <MaskedInput
        mask={this.props.mask}
        id={this.props.id}
        onBlur={this.props.onBlur}
        onChange={this.props.onChange}
        render={(innerRef, props) => <input innerRef={innerRef} {...props} />}
      />
    );
  }
}

export default CustomInput;
