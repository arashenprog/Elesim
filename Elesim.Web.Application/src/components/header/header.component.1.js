import React, { PureComponent } from "react";
import { Container, Row, Col, Hidden } from "react-grid-system";
import { Menu, Drawer, Icon, Button } from "antd";
import { Link } from "react-router-dom";
const ButtonGroup = Button.Group;
class HeaderComponent extends PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      drawerVisible: false
    };
  }
  render() {
    return (
      <div>
        <Container>
          <Row>
            <Col xl={2} lg={2} md={2} sm={8} xs={8}>
              <Row>
                <Hidden xl md lg>
                  <Col>
                    <div
                      className="drawer-button"
                      onClick={() => {
                        this.setState({ drawerVisible: true });
                      }}
                    >
                      <Icon type="menu" style={{ fontSize: 30 }} />
                    </div>
                  </Col>
                </Hidden>

                <Col>
                  <div className="nav-logo">
                    <img
                      src={require("../../assets/images/logo.png")}
                      alt="اِلِ سیم"
                    />
                  </div>
                </Col>
              </Row>
            </Col>
            <Hidden sm xs>
              <Col xl={8} lg={8} md={8}>
                <Menu
                  theme="light"
                  mode="horizontal"
                  defaultSelectedKeys={["1"]}
                  style={{ lineHeight: "64px" }}
                >
                  <Menu.Item key="1">
                    <Icon type="home" style={styles.icons} /> صفحه نخست
                  </Menu.Item>
                  <Menu.Item key="2">
                    <Icon type="shopping-cart" style={styles.icons} /> خرید سیم
                    کارت
                  </Menu.Item>
                  <Menu.Item key="3">
                    <Icon type="shop" style={styles.icons} /> فروش سیم کارت
                  </Menu.Item>
                  <Menu.Item key="4">
                    <Icon type="mobile" style={styles.icons} /> اپلیکیشن اِلِ
                    سیم
                  </Menu.Item>
                </Menu>
              </Col>
            </Hidden>
            <Col xl={2} lg={2} md={2} sm={4} xs={4}>
              <div className="account-button">
                <ButtonGroup>
                  <Link to="login">
                    <Button type="primary">
                      <Icon type="unlock" />
                      ورود
                    </Button>
                  </Link>
                  <Link to="register">
                    <Button>
                      <Icon
                        type="plus-square"
                        onClick={() => {
                          this.props.history.push("/register");
                        }}
                      />
                      ثبت نام
                    </Button>
                  </Link>
                </ButtonGroup>
              </div>
            </Col>
          </Row>
        </Container>
        <Drawer
          title={
            <div className="nav-logo">
              <img
                src={require("../../assets/images/logo.png")}
                alt="اِلِ سیم"
              />
            </div>
          }
          placement="right"
          closable={false}
          onClose={this.onClose.bind(this)}
          visible={this.state.drawerVisible}
        >
          <p>Some contents...</p>
          <p>Some contents...</p>
          <p>Some contents...</p>
        </Drawer>
      </div>
    );
  }
  onClose() {
    this.setState({ drawerVisible: false });
  }
}

const styles = {
  icons: {
    fontSize: 18
  }
};

export default HeaderComponent;
