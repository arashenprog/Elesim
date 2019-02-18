import React, { Component } from 'react'
import "./application.page.scss"
export class ApplicationPage extends Component {
  render() {
    return (
      <div className="application-page">
        <div class="device-wrapper">
          <div class="device" data-device="Pixel" data-orientation="portrait" data-color="black">
            <div class="screen">
              <img src={require("../../assets/images/screen.jpg")} alt="elesim app"/>
            </div>
            <div class="button" onClick={()=>{
              window.open("http://elesim.ir/download/elesim.apk","_blank")
            }}>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

export default ApplicationPage
