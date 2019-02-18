import React, { Component } from "react";
import { Menu, Icon, Button } from "antd";
import { Link } from "react-router-dom";
import Api from "../../services/api";
import { withRouter } from 'react-router'

const ButtonGroup = Button.Group;
class HeaderDesktopComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      drawerVisible: false, userToken: "", credit: "", name: "", user: null
    };
    this.handleMenuClick = this.handleMenuClick.bind(this)
  }
  handleMenuClick(e) {
    console.log(e)
    switch (e.key) {
      case "1":
        this.props.history.push("/")
        break;
      case "2":
        this.props.history.push("/application")
        break;
      case "3":
        this.props.history.push("/contact")
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
    Api.getAcyncLocalStorage("User").then(res => {
      this.setState({ user: res })
    }).catch(err=>console.log(err))
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
                    &nbsp;
                    صفحه نخست
                  </Menu.Item>
                  <Menu.Item key="2">
                    <Icon type="mobile" style={styles.icons} />
                    &nbsp;
                     اپلیکیشن اِلِ سیم
                  </Menu.Item>
                  <Menu.Item key="3">
                    <Icon type="phone" style={styles.icons} /> 
                    &nbsp;
                    تماس با ما
                  </Menu.Item>
                </Menu>
              </div>
              <div className="col">
                {this.state.user !== null ?
                  (<ButtonGroup>
                    <Link to="/profile">
                      <Button type="primary">
                        <Icon type="user" />
                        {"سلام " + this.state.user.Firstname}
                      </Button>
                    </Link>
                    <Link to="/profile">
                      <Button>
                        <Icon type="plus-square" />
                        اعتبار : &nbsp; {this.state.user.Credit} &nbsp; ریال
                      </Button>
                    </Link>
                  </ButtonGroup>
                  )
                  :
                  (<ButtonGroup><Link to="/login">
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
                  </ButtonGroup>)
                }

              </div>
            </div>
          </div>
        </div>
      </div>
    );
  } 
}
const styles = {
  icons: {
    fontSize: 18
  }
};
export default withRouter(HeaderDesktopComponent);
