import React, { Component } from "react";
import { Menu } from "antd";
import {Redirect} from 'react-router-dom'
const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

export class SideMenu extends Component {
  constructor(props) {
    super(props)
    this.state = {
      redirect: false,
      preCode: ""
    }
  }
  render() {
    let Numbers = ["912", "911", "913", "919", "910"]
    if (this.state.redirect) {
      const { preCode, num4, num5, num6, num7, num8, num9, num10 } = this.state;
      return <Redirect push to={`search/${(preCode ? preCode : "***") + (num4 ? num4 : "*") + (num5 ? num5 : "*") + (num6 ? num6 : "*") + (num7 ? num7 : "*") + (num8 ? num8 : "*") + (num9 ? num9 : "*") + (num10 ? num10 : "*")}`} />;
    }
    return (
      <Menu
        theme="light"
        mode="vertical-right"
        className="cat-menu"
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
          <MenuItemGroup key="hamrahEtebari" >
            {Numbers.map(num => {
              return (
                <Menu.Item key={num}>{num}</Menu.Item>
              )
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
          <MenuItemGroup key="hamrahDaemi">
            {Numbers.map(num => {
              return (
                <Menu.Item key={num}>{num}</Menu.Item>
              )
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
    )
  }
  onMenuHandelClick = (e) => {
    console.log(e.key)
    this.setState({ preCode: e.key, redirect: true })
  }
}


export default SideMenu
