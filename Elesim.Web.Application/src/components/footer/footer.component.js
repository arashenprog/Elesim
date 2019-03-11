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
          شرکت ارتباطات صبح امید در سال 1390، به عنوان نماینده رسمی اپراتور همراه اول در زمینه فروش کلیه محصولات فیزیکی و الکترونیک اپراتور همراه اول فعالیت خود را آغاز نمود و با برند تجارت الکترونیک (elesim) و (esunco) پا به عرصه فروش الکترونیک محصولات اپراتور همراه اول نهاده است و با استفاده از تجارب اندوخته شده مدیران به جرات به یکی از پیشرفته ترین شرکتهای فعال در حوزه فروش الکترونیک محصولات همراه تبدیل شده است. سامانه جامع و یکپارچه فروش سیمکارت الکترونیک با ویژگی های انحصاری و امکان ارائه خدمات ویژه، توسط متخصصین فناوری اطلاعات شرکت ارتباطات صبح امید ؛ با هدف ارائه خدمات الکترونیک به هموطنان عزیز و جلب رضایت حداکثری ایشان و با رویکرد گسترش فرهنگ شهروند الکترونیک جهت تسریع و تسهیل در خرید سیمکارت تلفن همراه اپراتورها طراحی و راه اندازی شده است.
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
