import React, { Component } from "react";
import SideMenu from "../../components/sidemenu/sidemenu.component";
import Slider from "../../components/slider/slider.component";
import Api from "../../services/api";
import SearchComponent from "../../components/search/search.component";

import DataBoxComponent from "../../components/databox/databox.component"

import normalIcon from "../../assets/images/sim-def.png";
import roundIcon from "../../assets/images/sim-rond.png";
import packageIcon from "../../assets/images/sim-pack.png";

let regularData = [];
let roundData = [];
let packData = [];
export class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loadingRegular: true,
      loadingRound: true,
      loadingPacks: true,
      showPaymentModal: false,
    }
  }

  componentWillMount() {
    Api.GetRegulars(0)
      .then(res => {
        if (regularData.length <= 0) {
          regularData.push(...res.data.Result);
        }
        this.setState({ loadingRegular: false });

      })
      .catch(err => console.log(err));
    Api.GetRounds(0)
      .then(res => {
        if (roundData.length <= 0) {
          roundData.push(...res.data.Result);
        }
        this.setState({ loadingRound: false });

      })
      .catch(err => console.log(err));
    Api.GetPackages(0)
      .then(res => {
        if (packData.length <= 0) {
          packData.push(...res.data.Result);
        }
        this.setState({ loadingPacks: false });

      })
      .catch(err => console.log(err));

  }
  
  render() {
    return (
      <div>
        <div className="container">
          <div className="mr-lg" />
          <div className="row">
            <div className="col-sm-3 col-xs-12 d-none d-sm-block">
              <SideMenu />
            </div>
            <div className="col-sm-9 col-xs-12 d-none d-sm-block">
              <Slider />
            </div>
          </div>
          <div className="mr-lg" />
          <div className="row">
            <div className="col-sm-12">
              <SearchComponent />
            </div>
          </div>
          <div className="mr-lg" />
          <DataBoxComponent
            titleIcon={normalIcon}
            title="سیم کارت های معمولی"
            dataSource={regularData}
            moreText="موارد بیشتر"
            loadingbt={this.state.loadingRegular}
            onMoreClick={this.onMoreReqular}
          >
          </DataBoxComponent>
          <div className="mr-lg" />
          <DataBoxComponent
            titleIcon={roundIcon}
            title="سیم کارت های رند"
            dataSource={roundData}
            moreText="موارد بیشتر"
            loadingbt={this.state.loadingRound}
            onMoreClick={this.onMoreRound}
          >
          </DataBoxComponent>
          <div className="mr-lg" />
          <DataBoxComponent
            titleIcon={packageIcon}
            title="پکیج ها"
            dataSource={packData}
            moreText="موارد بیشتر"
            loadingbt={this.state.loadingPacks}
            onMoreClick={this.onMorePacks}
            description={<div>ss</div>}
          >
          </DataBoxComponent>

        </div>
      </div>
    )
  }
  onMoreReqular = () => {
    let lastObject = regularData[regularData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingRegular: true });
    Api.GetRegulars(lastId).then(res => {
      regularData.push(...res.data.Result);
      this.setState({ loadingRegular: false });
    });
  }

  onMoreRound = () => {
    let lastObject = regularData[roundData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingRound: true });
    Api.GetRounds(lastId).then(res => {
      roundData.push(...res.data.Result);
      this.setState({ loadingRound: false });
    });
  }
  onMorePacks = () => {
    let lastObject = packData[packData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingPacks: true });
    Api.GetPackages(lastId).then(res => {
      packData.push(...res.data.Result);
      this.setState({ loadingPacks: false });
    });
  }

}

export default HomePage;
