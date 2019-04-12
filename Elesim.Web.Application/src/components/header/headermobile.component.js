import React, { PureComponent } from "react";
import { Menu, Drawer, Icon } from "antd";
import { Link } from "react-router-dom";
import { withRouter } from "react-router-dom";
import { Redirect } from "react-router-dom";

const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

class HeaderMobileComponent extends PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      drawerVisible: false,
      userToken: ""
    };
  }
  componentWillMount() {
    // Api.getLocalStorage("User").then(res => {
    //   this.setState({ userToken: res.Token })
    // })
  }
  render() {
    let Numbers = ["912", "911", "913", "919", "910"];
    if (this.state.redirect) {
      const { preCode, num4, num5, num6, num7, num8, num9, num10 } = this.state;
      return (
        <Redirect
          push
          to={`search/${(preCode ? preCode : "***") +
            (num4 ? num4 : "*") +
            (num5 ? num5 : "*") +
            (num6 ? num6 : "*") +
            (num7 ? num7 : "*") +
            (num8 ? num8 : "*") +
            (num9 ? num9 : "*") +
            (num10 ? num10 : "*")}`}
        />
      );
    }
    return (
      <div className="d-block d-sm-none">
        <div className="nav-menu-mobile">
          <div
            className="drawer-button"
            onClick={() => {
              this.setState({ drawerVisible: true });
            }}
          >
            <Icon type="menu" style={{ fontSize: 30 }} />
          </div>
          <div className="nav-logo">
            <Link to="/">
              <img
                src={require("../../assets/images/logo-white.png")}
                alt="اِلِ سیم"
              />
            </Link>
          </div>
        </div>
        <Drawer
          title={
            <div className="nav-logo">
              <img
                src={require("../../assets/images/logo-white.png")}
                alt="اِلِ سیم"
              />
            </div>
          }
          placement="right"
          closable={false}
          onClose={this.onClose.bind(this)}
          visible={this.state.drawerVisible}
          className="drawer-menu"
        >
          <Menu
            theme="light"
            mode="vertical-right"
            onClick={this.onClickMenu}
            style={{ lineHeight: "64px" }}
          >
            {this.state.userToken === "" ? (
              <>
                <Menu.Item
                  key="1"
                  className="ant-menu-item"
                  onClick={() => {
                    this.props.history.push("/login");
                    this.onClose()
                  }}
                >
                  <Icon type="unlock" style={styles.icons} /> ورود به حساب
                </Menu.Item>
                <Menu.Item
                  key="2"
                  className="ant-menu-item"
                  onClick={() => {
                    this.props.history.push("/register");
                    this.onClose()
                  }}
                >
                  <Icon type="plus-square" style={styles.icons} /> عضویت در اِلِ
                  سـیم
                </Menu.Item>
              </>
            ) : (
              <>
                <Menu.Item
                  key="1"
                  onClick={() => {
                    this.props.history.push("/profile");
                    this.onClose()
                  }}
                >
                  <Icon type="user" style={styles.icons} />
                  حساب کاربری
                </Menu.Item>
              </>
            )}
              <Menu.Item
              key="7"
              onClick={() => {
                this.props.history.push("/favorites");
                this.onClose()
              }}
            >
              <Icon type="star" style={styles.icons} /> نشان شده ها
            </Menu.Item>
            <Menu.Item
              key="6"
              onClick={() => {
                this.props.history.push("/application");
                this.onClose()
              }}
            >
              <Icon type="mobile" style={styles.icons} /> اپلیکیشن اِلِ سیم
            </Menu.Item>
          </Menu>

          <Menu
            theme="light"
            mode="inline"
            style={{ lineHeight: "64px" }}
            onClick={this.onMenuHandelClick}
          >
            <SubMenu
              key="hamrahEtebari"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/hamrah-aval.png")}
                    alt="همراه اول"
                  />
                  <span>همراه اول اعتباری</span>
                </span>
              }
            >
              <MenuItemGroup key="g1" title="پیش شماره ها">
                {Numbers.map(num => {
                  return <Menu.Item key={num}>{num}</Menu.Item>;
                })}
              </MenuItemGroup>
            </SubMenu>
            <SubMenu
              key="hamrahDaemi"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/hamrah-aval.png")}
                    alt="همراه اول"
                  />
                  <span>همراه اول دائمی</span>
                </span>
              }
            >
              <MenuItemGroup key="g2" title="پیش شماره ها">
                {Numbers.map(num => {
                  return <Menu.Item key={num}>{num}</Menu.Item>;
                })}
              </MenuItemGroup>
            </SubMenu>
            <SubMenu
              disabled
              key="irancellEtebari"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/irancell.png")}
                    alt="ابرانسل"
                  />
                  <span>ایرانسل اعتباری</span>
                </span>
              }
            >
              <MenuItemGroup key="g3" title="پیش شماره ها">
                <Menu.Item key="4">0912</Menu.Item>
                <Menu.Item key="5">0910</Menu.Item>
              </MenuItemGroup>
            </SubMenu>
            <SubMenu
              disabled
              key="irancellDaemi"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/irancell.png")}
                    alt="ابرانسل"
                  />
                  <span>ایرانسل دائمی</span>
                </span>
              }
            >
              <MenuItemGroup key="g3" title="پیش شماره ها">
                <Menu.Item key="6">0912</Menu.Item>
                <Menu.Item key="7">0910</Menu.Item>
              </MenuItemGroup>
            </SubMenu>
            <SubMenu
              disabled
              key="rightelDaemi"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/rightel.png")}
                    alt="رایتل"
                  />
                  <span>رایتل دائمی</span>
                </span>
              }
            >
              <MenuItemGroup key="g3" title="پیش شماره ها">
                <Menu.Item key="7">0912</Menu.Item>
                <Menu.Item key="8">0910</Menu.Item>
              </MenuItemGroup>
            </SubMenu>
            <SubMenu
              disabled
              key="rightelEtebari"
              title={
                <span>
                  <img
                    className="menu-logo"
                    src={require("../../assets/images/rightel.png")}
                    alt="رایتل"
                  />
                  <span>رایتل اعتباری</span>
                </span>
              }
            >
              <MenuItemGroup key="g3" title="پیش شماره ها">
                <Menu.Item key="9">0912</Menu.Item>
                <Menu.Item key="10">0910</Menu.Item>
              </MenuItemGroup>
            </SubMenu>
          </Menu>
        </Drawer>
      </div>
    );
  }
  onClose() {
    this.setState({ drawerVisible: false });
  }
  onClickMenu = (item, key, selected) => {
    console.log(item, key, selected);
  };
  onMenuHandelClick = e => {
    console.log(e.key);
    this.props.history.push(`/search/${e.key}*******`);
    this.onClose();
    let searchbox = document.getElementById("search-result")
    searchbox.scrollIntoView();
    
  };
}
const styles = {
  icons: {
    fontSize: 18
  }
};
export default withRouter(HeaderMobileComponent);
