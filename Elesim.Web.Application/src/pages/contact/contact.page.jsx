import React, { Component } from 'react'
import { Row, Col, Input, Form, Button, Icon, Card } from "antd";
const FormItem = Form.Item;
const { TextArea } = Input;
export class contactPage extends Component {
  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <div className="container-fluid contact-page">
        <div className="container">
          <div className="mr-lg" />

          <div className="row">

            <div className="col-sm-9 col-xs-12">
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
                      {getFieldDecorator("phone", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input
                          size="large"
                          placeholder="شماره تماس"
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

                  <Col span={12}>
                    <FormItem>
                      {getFieldDecorator("email", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input

                          size="large"
                          placeholder="ایمیل"
                          prefix={
                            <Icon
                              type="mail"
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
                      {getFieldDecorator("subject", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <Input

                          size="large"
                          placeholder="موضوع"
                          prefix={
                            <Icon
                              type="edit"
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
                      {getFieldDecorator("message", {
                        rules: [
                          {
                            required: true,
                            message: "لطفا این قسمت را تکمیل کنید"
                          }
                        ]
                      })(
                        <TextArea
                          placeholder="متن پیام"
                        />
                      )}
                    </FormItem>
                  </Col>

                </Row>
                <FormItem >
                  <Button
                    type="primary"
                    size="large"
                    htmlType="submit"
                    block
                  >
                    ارسال پیام
                  </Button>
                  <div className="mr-lg" />

                </FormItem>
              </Form>
            </div>
            <div className="col-sm-3 col-xs-12">
              <Row gutter={24}>
                <Col span={24}>
                  <Card title="تماس تلفنی" bordered={false}>
                    <h4>
                      09132041267
                    </h4>
                    <h4>
                      09135424277
                    </h4>
                  </Card>
                </Col>
                <Col span={24}>
                  <Card title="ایمیل" bordered={false}>
                    <h4>
                      info@elesim.ir
                    </h4>
                    <h4>
                      support@elesim.ir
                    </h4>
                  </Card>
                </Col>

              </Row>
            </div>
          </div>
        </div>
      </div>
    )
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        console.log("Received values of form: ", values);
        // let userInformation = {
        //   Firstname: values.firstName,
        //   Lastname: values.lastName,
        //   NationalCode: values.nationalCode,
        //   Mobile: values.phoneNumber
        // };

      }
      this.setState({ buttonLoading: true });

    });

  };
}
const ContactPage = Form.create()(contactPage);
export default ContactPage
