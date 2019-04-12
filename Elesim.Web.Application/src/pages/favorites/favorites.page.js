import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import Api from "../../services/api";
import DataBoxComponent from "../../components/databox/databox.component";
import normalIcon from "../../assets/images/sim-def.png";
import packageIcon from "../../assets/images/sim-pack.png";

export class FavoritesPage extends Component {
  state = {
    simFav: [],
    packFav: []
  };
  componentDidMount() {
    let simFav = Api.getLocalStorage("sim-fav");
    let packFav = Api.getLocalStorage("pack-fav");

    this.setState({ simFav, packFav });
  }
  render() {
    return (
      <div className="favorites-page">
        <div className="container">
          <div className="mr-lg" />
          <DataBoxComponent
            titleIcon={normalIcon}
            title="سیم کارت های نشان شده"
            dataSource={this.state.simFav}
          />
          <div className="mr-lg" />

          <DataBoxComponent
            titleIcon={packageIcon}
            title="پکیج های نشان شده"
            dataSource={this.state.packFav}
            description={<div>ss</div>}

          />
        </div>
      </div>
    );
  }
}

export default withRouter(FavoritesPage);
