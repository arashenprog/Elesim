import React, { Component } from 'react'
import { Button } from "antd";

export class FooterComponent extends Component {
  render() {
    return (
      <div>
        <div className="mr-lg" />

        <div className="footer">
          <img
            src={require("../../assets/images/logo-white.png")}
            alt="اِلِ سیم"
            width={180}
          />
          <p>
            لورم ایپسوم یا طرح‌نما (به انگلیسی: Lorem ipsum) به متنی آزمایشی و
            بی‌معنی در صنعت چاپ، صفحه‌آرایی و طراحی گرافیک گفته می‌شود. طراح
            گرافیک از این متن به عنوان عنصری از ترکیب بندی برای پر کردن صفحه و
            ارایه اولیه شکل ظاهری و کلی طرح سفارش گرفته شده استفاده می نماید، تا
            از نظر گرافیکی نشانگر چگونگی نوع و اندازه فونت و ظاهر متن باشد.
            معمولا طراحان گرافیک برای صفحه‌آرایی، نخست از متن‌های آزمایشی و
            بی‌معنی استفاده می‌کنند تا صرفا به مشتری یا صاحب کار خود نشان دهند
            که صفحه طراحی یا صفحه بندی شده بعد از اینکه متن در آن قرار گیرد
            چگونه به نظر می‌رسد و قلم‌ها و اندازه‌بندی‌ها چگونه در نظر گرفته
            شده‌است.
          </p>
          <Button
            style={{ fontSize: 18, height: 40 }}
            type="dashed"
            icon="android"
            size="large"
            onClick={() => {
              window.open("http://elesim.ir/download/elesim.apk", "_blank")
            }}
          >
            دریافت نرم افزار اندروید
          </Button>
          <p>تمامی حقوق برای اِل سیم محفوظ می باشد</p>
        </div>
      </div>
    )
  }
}

export default FooterComponent
