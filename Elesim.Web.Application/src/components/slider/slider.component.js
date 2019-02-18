import React, { Component } from "react";
import {withRouter} from 'react-router-dom'
import RBCarousel from "react-bootstrap-carousel";
import "react-bootstrap-carousel/dist/react-bootstrap-carousel.css";
import "./slider.component.scss"
import slide1 from "../../assets/images/slide1.png"
import slide2 from "../../assets/images/slide2.png"

const styles = { height: 355, width: "100%" };
export class Slider extends Component {
  slideNext = () => {
    this.slider.slideNext();
  };
  slidePrev = () => {
    this.slider.slidePrev();
  };
  onSelect = (active, direction) => {
  };

  render() {

    return (
      <div className="slider">
        <RBCarousel
              animation={true}
              autoplay={true}
              slideshowSpeed={2000}
              onSelect={this.onSelect}
              ref={r => (this.slider = r)}
              version={4}
            >
              <div style={{ height: 400 }}>
                <img
                  style={{ ...styles }}
                  src={slide1}
                  alt=""
                />
              </div>
              <div style={{ height: 400 }}>
                <img
                   style={{ ...styles }}
                  src={slide2}
                  alt=""

                />
              </div>
            </RBCarousel>
      </div>

    );
  }
  
}

export default withRouter(Slider);
