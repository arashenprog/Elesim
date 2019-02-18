import React, { Component } from "react";
import { Layout, Menu, Icon } from "antd";
import LoginPage from "../account/login.page";
import { Link } from "react-router-dom";

const { Header, Sider, Content } = Layout;


class App extends Component {
  
  state = {
    collapsed: false
  };
  toggle() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
  onMenuClick(item) {
    switch (item) {
      case 1:
        
        break;

      default:
        break;
    }
  }
  
  render() {
    return (
      <Layout>
        <Sider
          trigger={null}
          width={250}
          collapsible
          collapsed={this.state.collapsed}
        >
          <div className="logo" />
          <Menu
            theme="dark"
            mode="inline"
            defaultSelectedKeys={["2"]}
            onClick={this.onMenuClick.bind(this)}
          >
            <Menu.Item key="1">
              <Icon type="home" />
              <Link to="/">صفحه نخست</Link>
            </Menu.Item>
            <Menu.Item key="2">
              <Icon type="dashboard" />
              <span>داشبورد</span>
            </Menu.Item>
            <Menu.Item key="4">
              <Icon type="video-camera" />
              <span>گالری</span>
            </Menu.Item>
            <Menu.Item key="5">
              <Icon type="upload" />
              <span>آپلود</span>
            </Menu.Item>
          </Menu>
        </Sider>
        <Layout>
          <Header style={{ background: "#fff", padding: 0 }}>
            <Icon
              className="trigger"
              type={this.state.collapsed ? "menu-unfold" : "menu-fold"}
              onClick={this.toggle.bind(this)}
            />
          </Header>
          <Content
            style={{
              margin: "24px 16px",
              padding: 24,
              background: "#fff",
              minHeight: 280
            }}
          >
            {
              //Code Here
            }

            <LoginPage />
          </Content>
        </Layout>
      </Layout>
    );
  }
}

export default App;
