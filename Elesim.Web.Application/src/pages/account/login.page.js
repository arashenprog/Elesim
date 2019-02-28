import React, { Component } from "react";
import { message, Row, Col, Input, Icon, Button, notification } from "antd";
import Api from "../../services/api";
import { Link } from "react-router-dom";

class LoginPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      phoneNumber: "",
      isForm: true,
      confirmCode: "",
      timer: 60,
      phoneNumberDisabled: false,
      buttonLoading: false,
      hasToken: false
    };
  }
  _renderLoginForm() {
    return (
      <div>
        <Input
          value={this.state.phoneNumber}
          onChange={e => {
            const value = e.target.value;
            const reg = /^-?(|[0-9][0-9]*)(\.[0-9]*)?$/;
            if (
              (!isNaN(value) && reg.test(value)) ||
              value === "" ||
              value === "-"
            ) {
              this.setState({ phoneNumber: value });
            }
          }}
          onPressEnter={this.onLoginClick.bind(this)}
          size="large"
          placeholder="شماره تلفن"
          prefix={<Icon type="phone" style={{ color: "rgba(0,0,0,.25)" }} />}
        />
        <div className="mr-lg" />
        <Button
          type="primary"
          size="large"
          block
          onClick={this.onLoginClick.bind(this)}
          loading={this.state.buttonLoading}
        >
          ورود
        </Button>

        <div className="mr-lg" />
        <Button
          type="dashed"
          size="large"
          block
          onClick={() => {
            this.props.history.push("/register");
          }}
        >
          قبلا ثبت نام نکرده ام
        </Button>
      </div>
    );
  }

  _renderConfirmCode = () => {
    return (
      <div>
        <h1 className="counter">{this.state.timer}</h1>
        <h3 style={{ textAlign: "center" }}>
          {" "}
          {`کد تایید برای شماره ${this.state.phoneNumber} ارسال شد`}{" "}
        </h3>
        <Input
          value={this.state.confirmCode}
          disabled={this.state.phoneNumberDisabled}
          onChange={e => {
            this.setState({ confirmCode: e.target.value });
          }}
          size="large"
          placeholder="کد تایید ارسال شده را وارد کنید"
          prefix={<Icon type="message" style={{ color: "rgba(0,0,0,.25)" }} />}
        />
        <div className="mr-lg" />
        {!this.state.phoneNumberDisabled ? (
          <Button
            type="primary"
            size="large"
            block
            onClick={this.onSignInClick.bind(this)}
          >
            تایید کد
          </Button>
        ) : (
            <Button
              type="primary"
              size="large"
              block
              onClick={this.onResendClick.bind(this)}
            >
              ارسال مجدد کد
          </Button>
          )}
      </div>
    );
  };

  _renderForm = () => {
    if (this.state.isForm) {
      return this._renderLoginForm();
    } else {
      return this._renderConfirmCode();
    }
  };
  _counter = () => {
    let seconds = 60;
    let counterInterval = setInterval(() => {
      let _second = --seconds;
      this.setState({ timer: _second });
      if (_second === 0) {
        clearInterval(counterInterval);
        this.setState({ phoneNumberDisabled: true });
        this.openNotificationWithIcon("error");
      }
      if (this.state.hasToken) {
        clearInterval(counterInterval);
      }
    }, 1000);
  };

  openNotificationWithIcon = type => {
    notification.config({
      duration: 20000
    });
    notification[type]({
      message: "مهلت استفاده از کد به پایان رسید",
      description: `شما تنها یک دقیقه فرصت داشتید تا کد ارسال شده به شماره همراه ${
        this.state.phoneNumber
        } را وارد کنید.لطفا در صورت تمایل به ارسال مجدد کد بر روی کلید ارسال مجدد کلیک کنید`
    });
  };
  render() {
   
    return (
      <div>
        <Row>
          <Col xs={24} sm={10} md={10} lg={10} xl={10}>
            <div className="form-logo">
              <Link to="/">
                <img
                  src={require("../../assets/images/logo.png")}
                  alt="اِلِ سیم"
                />
              </Link>
              <h5>ورود به حساب کاربری</h5>
              <Button className="d-block d-sm-none" type="primary" ghost={true} icon="right" size="default" onClick={()=>{
                this.props.history.push("/")
              }}>بازگشت</Button>
            </div>
            <div className="full-form">{this._renderForm()}</div>
          </Col>
          <Col xs={0} sm={14} md={14} lg={14} xl={14}>
            <div className="full-background login-background" />
          </Col>

        </Row>
      </div>
    );
  }
  onLoginClick() {
    let phoneNumber = this.state.phoneNumber;
    this.setState({ buttonLoading: true });
    if (phoneNumber === "") {
      message.error("لطفا شماره موبایل را وارد کنید");
      this.setState({ buttonLoading: false });
    } else if (phoneNumber.length < 11) {
      message.error("لطفا شماره موبایل معتبر وارد کنید");
      this.setState({ buttonLoading: false });
    } else {
      this._counter();
      Api.SendSMS(phoneNumber).then(res => {
        if (res.data.Succeed === true) {
          this.setState({ isForm: false });
          this.setState({ buttonLoading: false });
          this.setState({ isForm: false });
        } else if (res.data.Error != null) {
          message.error(res.data.Messages);
          this.setState({ buttonLoading: false });
        } else {
          message.warning("مشکلی پیش اومده ، لطفا بعدا تلاش کنید");
          this.setState({ buttonLoading: false });
        }
      });
    }
  }
  onResendClick() {
    let phoneNumber = this.state.phoneNumber;
    this._counter();
    this.setState({ phoneNumberDisabled: false });
    Api.SendSMS(phoneNumber).then(res => {
      if (res.data.Succeed === true) {
        Api.setLocalStorage("phoneNumber", phoneNumber);
        this.setState({ isForm: false });
      } else if (res.data.Error != null) {
        message.error(res.data.Messages);
      } else {
        message.warning("مشکلی پیش اومده ، لطفا بعدا تلاش کنید");
      }
    });
  }
  onSignInClick() {
    let state = this.state;
    let { phoneNumber, confirmCode } = state;

    if (confirmCode === "") {
      message.error("لطفا کد ارسال شده را وارد کنید");
    } else {
      Api.GetToken(phoneNumber, confirmCode).then(res => {
        if (res.data.Succeed === true) {
          this.setState({ hasToken: true });

          Api.SignIn(res.data.Result).then(res => {
            Api.setLocalStorage("User", res.data.Result);
            Api.setLocalStorage("Token",res.data.Result.Token)
            this.props.history.push("/profile");
          });
        } else if (res.data.Error != null) {
          message.error(res.data.Messages);
        } else {
          message.warning("مشکلی پیش اومده ، لطفا بعدا تلاش کنید");
        }
      });
    }
  }
}

export default LoginPage;
