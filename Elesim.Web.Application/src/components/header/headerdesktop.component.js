import React, { Component } from "react";
import { Radio, Menu, Icon, Button, Input, Form, Modal } from "antd";

import { Link } from "react-router-dom";
import Api from "../../services/api";
import { withRouter } from "react-router";

const FormItem = Form.Item;
const RadioGroup = Radio.Group;
const ButtonGroup = Button.Group;
class HeaderDesktopComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      drawerVisible: false,
      userToken: "",
      credit: "",
      name: "",
      user: null,
      showCreditModal: false,
      customCreditAmount: null,
    };
    this.handleMenuClick = this.handleMenuClick.bind(this);
  }
  handleMenuClick(e) {
    console.log(e);
    switch (e.key) {
      case "1":
        this.props.history.push("/");
        break;
      case "2":
        this.props.history.push("/application");
        break;
      case "3":
        this.props.history.push("/contact");
        break;
      default:
        break;
    }
  }
  componentWillMount() {
    // let user = Api.getLocalStorage("User")
    // if(user){
    //   this.setState({user:user})

    // }
    Api.getAcyncLocalStorage("User")
      .then(res => {
        this.setState({ user: res });
      })
      .catch(err => console.log(err));
  }
  toggleCreditModal(e) {
    this.setState({ showCreditModal: e });
  }
  onCreditAmountChange = (e) => {
    console.log('radio checked', e.target.value);
    this.setState({
      creditAmount: e.target.value,
    });
  }
  render() {
    return (
      <div className="d-none d-sm-block">
        <div className="container-fluid nav-menu-desktop">
          <div className="container">
            <div className="row">
              <div className="col-2">
                <div className="nav-logo">
                  <Link to="/">
                    <img
                      src={require("../../assets/images/logo.png")}
                      alt="اِلِ سیم"
                    />
                  </Link>
                </div>
              </div>
              <div className="col">
                <Menu
                  theme="light"
                  mode="horizontal"
                  defaultSelectedKeys={["0"]}
                  style={{ lineHeight: "64px" }}
                  onClick={this.handleMenuClick}
                >
                  <Menu.Item key="1">
                    <Icon type="home" style={styles.icons} />
                    &nbsp; صفحه نخست
                  </Menu.Item>
                  <Menu.Item key="2">
                    <Icon type="mobile" style={styles.icons} />
                    &nbsp; اپلیکیشن اِلِ سیم
                  </Menu.Item>
                  <Menu.Item key="3">
                    <Icon type="phone" style={styles.icons} />
                    &nbsp; تماس با ما
                  </Menu.Item>
                </Menu>
              </div>
              <div className="col">
                {this.state.user !== null ? (
                  <ButtonGroup>
                    <Link to="/profile">
                      <Button type="primary">
                        <Icon type="user" />
                        {"سلام " + this.state.user.Firstname}
                      </Button>
                    </Link>
                      <Button onClick={() => {
                        this.toggleCreditModal(true)
                      }}>
                        <Icon type="plus-square" />
                        اعتبار : &nbsp; {this.state.user.Credit} &nbsp; ریال
                      </Button>
                  </ButtonGroup>
                ) : (
                  <ButtonGroup>
                    <Link to="/login">
                      <Button type="primary">
                        <Icon type="unlock" />
                        ورود
                      </Button>
                    </Link>
                    <Link to="/register">
                      <Button>
                        <Icon type="plus-square" />
                        ثبت نام
                      </Button>
                    </Link>
                  </ButtonGroup>
                )}
              </div>
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
          <RadioGroup
            onChange={this.onCreditAmountChange}
            value={this.state.creditAmount}
          >
            <Radio style={radioStyle} value={5}>
              5 هزار تومان
            </Radio>
            <Radio style={radioStyle} value={20}>
              20 هزار تومان
            </Radio>
            <Radio style={radioStyle} value={50}>
              50 هزارتومان
            </Radio>
            <Radio style={radioStyle} value={100}>
              100 هزارتومان
            </Radio>
            <Radio style={radioStyle} value="other">
              مبلغ دلخواه
              {this.state.creditAmount === "other" ? (
                <Input
                  placeholder="مبلغ دلخواه را وارد کنید"
                  onChange={e => {
                    this.setState({ customCreditAmount: e.target.value });
                  }}
                  style={{ width: 220, marginRight: 20 }}
                  addonAfter="تومان"
                />
              ) : null}
            </Radio>
          </RadioGroup>
        </Modal>
      </div>
    );
  }
}
const styles = {
  icons: {
    fontSize: 18
  }
};
const radioStyle = {
  display: 'block',
  height: '30px',
  lineHeight: '30px',
};

export default withRouter(HeaderDesktopComponent);
