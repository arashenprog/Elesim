import React, { Component } from "react";
import { Row, Col, Input, Icon, Button } from "antd";
import Api from "../../services/api";

const InputGroup = Input.Group;
class registerPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      firstName: "",
      lastName: "",
      nationalCode: "",
      phoneNumber: "",
      address: "",
      postalCode: "",
      email: "",
      buttonLoading: false
    };
  }
  _renderRegisterForm() {
    return (
      <div>
        <InputGroup size="large">
          <Row type="flex" gutter={12}>
            <Col span={12}>
              <Input
                value={this.state.firstName}
                onChange={e => {
                  this.setState({ firstName: e.target.value });
                }}
                size="large"
                placeholder="نام"
                prefix={
                  <Icon type="user" style={{ color: "rgba(0,0,0,.25)" }} />
                }
              />
            </Col>
            <Col span={12}>
              <Input
                value={this.state.lastName}
                onChange={e => {
                  this.setState({ lastName: e.target.value });
                }}
                size="large"
                placeholder="نام خانوادگی"
                prefix={
                  <Icon type="team" style={{ color: "rgba(0,0,0,.25)" }} />
                }
              />
            </Col>
          </Row>
        </InputGroup>

        <div className="mr-sm" />

        <InputGroup>
          <Row type="flex" gutter={12}>
            <Col span={12}>
              <Input
                value={this.state.nationalCode}
                onChange={e => {
                  this.setState({ nationalCode: e.target.value });
                }}
                size="large"
                placeholder="کد ملی"
                prefix={
                  <Icon type="barcode" style={{ color: "rgba(0,0,0,.25)" }} />
                }
              />
            </Col>
            <Col span={12}>
              <Input
                value={this.state.phoneNumber}
                onChange={e => {
                  this.setState({ phoneNumber: e.target.value });
                }}
                size="large"
                placeholder="شماره تلفن همراه"
                prefix={
                  <Icon type="phone" style={{ color: "rgba(0,0,0,.25)" }} />
                }
              />
            </Col>
          </Row>
        </InputGroup>
        <div className="mr-sm" />
        <InputGroup>
          <Row type="flex" gutter={12}>
            <Col span={12}>
              <Input
                value={this.state.address}
                onChange={e => {
                  this.setState({ address: e.target.value });
                }}
                size="large"
                placeholder="آدرس"
                prefix={
                  <Icon
                    type="environment"
                    style={{ color: "rgba(0,0,0,.25)" }}
                  />
                }
              />
            </Col>
            <Col span={12}>
              <Input
                value={this.state.postalCode}
                onChange={e => {
                  this.setState({ postalCode: e.target.value });
                }}
                size="large"
                placeholder="کد پستی"
                prefix={
                  <Icon type="scan" style={{ color: "rgba(0,0,0,.25)" }} />
                }
              />
            </Col>
          </Row>
        </InputGroup>

        <div className="mr-sm" />

        <Input
          value={this.state.email}
          onChange={e => {
            this.setState({ email: e.target.value });
          }}
          size="large"
          placeholder="ایمیل"
          prefix={<Icon type="mail" style={{ color: "rgba(0,0,0,.25)" }} />}
        />
        <div className="mr-lg" />
        <Button
          type="primary"
          size="large"
          block
          onClick={this.onRegisterClick.bind(this)}
          loading={this.state.buttonLoading}
        >
          ثبت نام
        </Button>

        <div className="mr-lg" />
        <Button
          type="dashed"
          size="large"
          block
          onClick={() => {
            this.props.history.push("/login");
          }}
        >
          قبلا ثبت نام کرده ام
        </Button>
      </div>
    );
  }

  render() {
    return (
      <div>
        <Row>
          <Col xs={24} sm={8} md={8} lg={8} xl={8}>
            <div className="form-logo">
              <img
                src={require("../../assets/images/logo.png")}
                alt="اِلِ سیم"
              />
              <h5>ایجاد حساب کاربری</h5>
            </div>
            <div className="full-form">{this._renderRegisterForm()}</div>
          </Col>
          <Col xs={0} sm={16} md={16} lg={16} xl={16}>
            <div className="full-background register-background" />
          </Col>
        </Row>
      </div>
    );
  }

  onRegisterClick() {
    this.setState({ buttonLoading: true });
    const state = this.state;
    const { firstName, lastName, nationalCode, phoneNumber } = state;
    let userInformation = {
      Firstname: firstName,
      Lastname: lastName,
      NationalCode: nationalCode,
      Mobile: phoneNumber
    };
    Api.Register(userInformation).then(res => {
      this.setState({ buttonLoading: false });
      console.log(res);
    });
    console.log(this.state);
  }
}

export default registerPage;
