import React, { Component } from 'react'
import Api from '../../services/api';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import { Radio, Row, Col, Input, Form, Button, Icon, notification, Modal, List } from "antd";
import Moment from "moment-jalaali"
import { Separate } from '../../components/separator/separator';
const FormItem = Form.Item;
const RadioGroup = Radio.Group;

export class ProfilePage extends Component {
  constructor(props) {
    super(props)
    this.state = {
      user: {},
      FirstNameCharacter: "",
      showCreditModal: false,
      creditAmount: null,
      customCreditAmount: null,
      paymentHistory: []
    }
    this.logOut = this.logOut.bind(this)
    this.toggleCreditModal = this.toggleCreditModal.bind(this)

  }
  componentWillMount() {

    Api.getAcyncLocalStorage("User").then(res => {
      this.setState({ user: res })
      let userFirstCharName = this.state.user.Firstname.split("")
      this.setState({ FirstNameCharacter: userFirstCharName[0] })

      this.props.form.setFieldsValue({
        firstName: this.state.user.Firstname,
        lastName: this.state.user.Lastname,
        nationalCode: this.state.user.NationalCode,
        mobile: this.state.user.Mobile,
        Phone: this.state.user.Phone,
        address: this.state.user.Address,
        postalCode: this.state.user.PostalCode,
        email: this.state.user.Email
      });
      console.log(this.state.user)


    }).catch(err => { console.log(err) });

    let token = Api.getLocalStorage("Token");
    console.log(token)
    Api.GetPaymentHistory(token).then(res => {
      this.setState({ paymentHistory: res.data.Result })
      console.log(res)
    }).catch(err => { console.log(err) })


  }

  openNotificationWithIcon = (type, title, message) => {
    notification.config({
      duration: 20000
    });
    notification[type]({
      message: title,
      description: message
    });
  };
  toggleCreditModal(e) {
    this.setState({ showCreditModal: e })
  }
  handleSubmitUserInfo = e => {
    e.preventDefault();

    this.props.form.validateFields((err, values) => {
      if (!err) {
        console.log("Received values of form: ", values);
        let userInformation = {
          Firstname: values.firstName,
          Lastname: values.lastName,
          NationalCode: values.nationalCode,
          Phone: values.phoneNumber,
          PostalCode: values.postalCode,
          Address: values.address,
          Email: values.email
        };
        console.log("user info", userInformation)
        Api.UpdateProfile(userInformation).then(res => {
          this.setState({ buttonLoading: false });
          console.log(res);
          if (!res.data.Succeed) {
            this.openNotificationWithIcon("error", "خطا", res.data.Messages[0]);
          }

        });
      }
      this.setState({ buttonLoading: true });

    });

  };
  render() {
    const { getFieldDecorator } = this.props.form;

    return (
      <div className="container-fluid profile-page">
        <div className="container">
          <div className="row">
            <div className="col-sm-12 col-md-6">
              <div className="user-box">
                <div className="user-info">
                  <div className="user-avatar">
                    <img src={`https://ui-avatars.com/api/?name=${this.state.FirstNameCharacter}&size=100&background=37bde3&color=ffffff&rounded=true`} alt="avatar" />

                  </div>
                  <div className="user-des">
                    <h2>
                      {this.state.user.Firstname + " " + this.state.user.Lastname}
                    </h2>
                    <p>
                      اعتبار شما : {this.state.user.Credit} <Button type="primary" size="small" onClick={() => {
                        this.toggleCreditModal(true)
                      }}>افزایش اعتبار</Button>
                    </p>
                  </div>
                </div>

              </div>
            </div>
            <div className="col-sm-12 col-md-6">
              <div className="user-option">
                <Button type="danger" ghost icon="poweroff" onClick={this.logOut}>
                  خروج از حساب کاربری
                  </Button>
              </div></div>
          </div>

          <div className="row">
            <div className="col-12">
              <Tabs >
                <TabList>
                  <Tab>ویرایش اطلاعات</Tab>

                  <Tab>تاریخچه خرید</Tab>
                </TabList>

                <TabPanel>
                  <Form onSubmit={this.handleSubmitUserInfo}>
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
                          {getFieldDecorator("mobile")(
                            <Input
                              size="large"
                              placeholder="شماره تلفن همراه"
                              disabled
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
                      <Col span={12}>
                        <FormItem>
                          {getFieldDecorator("address", {
                            rules: [
                              {
                                required: true,
                                message: "لطفا این قسمت را تکمیل کنید"
                              }
                            ]
                          })(
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
                      <Col span={12}>
                        <FormItem>
                          {getFieldDecorator("postalCode", {
                            rules: [
                              {
                                required: true,
                                message: "لطفا این قسمت را تکمیل کنید"
                              }
                            ]
                          })(
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
                    <Row type="flex" gutter={12}>
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
                              placeholder="تلفن ثابت"
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
                    <FormItem >
                      <Button
                        type="primary"
                        size="large"
                        htmlType="submit"
                        block
                      >
                        بروزرسانی اطلاعات
                  </Button>

                    </FormItem>
                  </Form>
                </TabPanel>
                <TabPanel>
                  <List
                    itemLayout="horizontal"
                    dataSource={this.state.paymentHistory}
                    renderItem={item => (
                      <List.Item>
                        <List.Item.Meta
                          title={<div><h3>محصول : {item.Title}</h3> </div>}
                          description={
                            <div>
                              <p>
                                قیمت : {Separate(item.Price)} تومان
                              </p>
                              <p>
                                روش پرداخت : {item.PaymentType === 1 ? "آنلاین" : "کسر از اعتبار"}
                              </p>
                              <p>
                                زمان : {
                                  Moment(item.Time, 'YYYY-M-D HH:mm:ss').endOf('jMonth').format('jYYYY/jM/jD HH:mm:ss')
                                }
                              </p>

                            </div>
                          }
                        />
                      </List.Item>
                    )}
                  />
                </TabPanel>
              </Tabs>
            </div>
          </div>
        </div>
        <Modal
          title="افزایش اعتبار"
          centered
          visible={this.state.showCreditModal}
          onOk={() => this.toggleCreditModal(false)}
          onCancel={() => this.toggleCreditModal(false)}
        >
          <RadioGroup onChange={this.onCreditAmountChange} value={this.state.creditAmount}>
            <Radio style={radioStyle} value={5}>5 هزار تومان</Radio>
            <Radio style={radioStyle} value={20}>20 هزار تومان</Radio>
            <Radio style={radioStyle} value={50}>50 هزارتومان</Radio>
            <Radio style={radioStyle} value={100}>100 هزارتومان</Radio>
            <Radio style={radioStyle} value="other">
              مبلغ دلخواه
          {this.state.creditAmount === "other" ? <Input placeholder="مبلغ دلخواه را وارد کنید" onChange={(e) => { this.setState({ customCreditAmount: e.target.value }) }} style={{ width: 220, marginRight: 20 }} addonAfter="تومان" /> : null}
            </Radio>
          </RadioGroup>
        </Modal>
      </div>
    )
  }

  logOut() {
    Api.clearLocalStorage();
    this.props.history.push("/");
    window.location.reload()
  }
  onCreditAmountChange = (e) => {
    console.log('radio checked', e.target.value);
    this.setState({
      creditAmount: e.target.value,
    });
  }
}
const radioStyle = {
  display: 'block',
  height: '30px',
  lineHeight: '30px',
};
const profilePage = Form.create()(ProfilePage);

export default profilePage
