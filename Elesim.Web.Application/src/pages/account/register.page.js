import React, { Component } from "react";
import { Row, Col, Input, Form, Button, Icon, notification } from "antd";
import Api from "../../services/api";
import { Link } from "react-router-dom";
const FormItem = Form.Item;

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
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        console.log("Received values of form: ", values);
        let userInformation = {
          Firstname: values.firstName,
          Lastname: values.lastName,
          NationalCode: values.nationalCode,
          Mobile: values.phoneNumber
        };
        Api.Register(userInformation).then(res => {
          this.setState({ buttonLoading: false });
          console.log("data to api", res);
          if (!res.data.Succeed) {
            this.openNotificationWithIcon("error", "خطا", res.data.Messages[0]);
          }
        });
      }
      this.setState({ buttonLoading: true });
    });
  };
  openNotificationWithIcon = (type, title, message) => {
    notification.config({
      duration: 20000
    });
    notification[type]({
      message: title,
      description: message
    });
  };
  _renderRegisterForm() {
    return <div />;
  }

  render() {
    const { getFieldDecorator } = this.props.form;
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
              <h5>ایجاد حساب کاربری</h5>
              <Button
                className="d-block d-sm-none"
                type="primary"
                ghost={true}
                icon="right"
                size="default"
                onClick={() => {
                  this.props.history.push("/");
                }}
              >
                بازگشت
              </Button>
            </div>
            <div className="full-form">
              <Form onSubmit={this.handleSubmit}>
                <Row type="flex" gutter={12}>
                  <Col span={12}>
                    <FormItem>
                      {getFieldDecorator("firstName", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input
                          size="large"
                          placeholder="نام"
                          prefix={
                            <Icon
                              type="user"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>

                  <Col span={12}>
                    <FormItem>
                      {getFieldDecorator("lastName", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input
                          size="large"
                          placeholder="نام خانوادگی"
                          prefix={
                            <Icon
                              type="team"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>
                </Row>
                <Row type="flex" gutter={12}>
                  <Col span={12}>
                    <FormItem>
                      {getFieldDecorator("nationalCode", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input
                          size="large"
                          placeholder="کد ملی"
                          prefix={
                            <Icon
                              type="barcode"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>
                  <Col span={12}>
                    <FormItem>
                      {getFieldDecorator("phoneNumber", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input
                          size="large"
                          placeholder="شماره تلفن همراه"
                          prefix={
                            <Icon
                              type="phone"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>
                </Row>
                <Row type="flex" gutter={12}>
                  <Col span={24}>
                    <FormItem>
                      {getFieldDecorator("address")(
                        <Input
                          size="large"
                          placeholder="آدرس"
                          prefix={
                            <Icon
                              type="environment"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>
                </Row>
                <Row type="flex" gutter={12}>
                  <Col span={24}>
                    <FormItem>
                      {getFieldDecorator("postalCode")(
                        <Input
                          size="large"
                          placeholder="کد پستی"
                          prefix={
                            <Icon
                              type="scan"
                              style={{ color: "rgba(0,0,0,.25)" }}
                            />
                          }
                        />
                      )}
                    </FormItem>
                  </Col>
                </Row>
                <FormItem>
                  <Button type="primary" size="large" htmlType="submit" block>
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
                </FormItem>
              </Form>
            </div>
          </Col>
          <Col xs={0} sm={14} md={14} lg={14} xl={14}>
            <div className="full-background register-background" />
          </Col>
        </Row>
      </div>
    );
  }
}
const RegisterForm = Form.create()(registerPage);

export default RegisterForm;
