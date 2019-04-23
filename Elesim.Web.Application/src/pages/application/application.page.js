import React, { Component } from "react";
import "./application.page.scss";
import { Button } from "antd";
export class ApplicationPage extends Component {
  render() {
    return (
      <div className="application-page">
        <div className="device-wrapper">
          <div
            className="device"
            data-device="Pixel"
            data-orientation="portrait"
            data-color="black"
          >
            <div className="screen">
              <img
                src={require("../../assets/images/screen.jpg")}
                alt="elesim app"
              />
            </div>
            <div
              className="button"
              onClick={() => {
                window.open("http://elesim.ir/download/elesim.apk", "_blank");
              }}
            />
          </div>
        </div>
        <div className="mr-lg" />
        <Button
          type="primary"
          size="large"
          onClick={() => {
            window.open("http://elesim.ir/download/elesim.apk", "_blank");
          }}
        >
          دریافت اپلیکیشن اندروید اِلِ ســیم
        </Button>
      </div>
    );
  }
}

export default ApplicationPage;
