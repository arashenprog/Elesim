import React, { Component } from "react";
import { List, Card, Avatar, Icon, Button, Modal } from "antd";
import { Separate } from "../../components/separator/separator";
import hamrahLogo from "../../assets/images/hamrah-aval.png";
import { Redirect } from "react-router-dom";
import Api from "../../services/api";
import { withRouter } from "react-router-dom";
import _ from "lodash";

const { Meta } = Card;
export class DataBoxComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showPaymentModal: false,
      selectedProduct: {},
      redirectSim: false,
      redirectPack: false,
      isFav: false
    };
    this.onCardSimClick = this.onCardSimClick.bind(this);
  }
  componentDidMount() {}
  render() {
    let _selectedProduct = this.state.selectedProduct;

    if (this.state.redirectSim) {
      return <Redirect push to={`sim/${_selectedProduct.ID}`} />;
    }
    if (this.state.redirectPack) {
      return <Redirect push to={`pack/${_selectedProduct.ID}`} />;
    }
    return (
      <div>
        <Card
          title={
            <div className="panel-header">
              <img src={this.props.titleIcon} alt="titleIconAlt" />
              {this.props.title}
            </div>
          }
        >
          <List
            grid={{
              gutter: 16,
              xs: 1,
              sm: 4,
              md: 4,
              lg: 4,
              xl: 4,
              xxl: 4
            }}
            dataSource={this.props.dataSource}
            renderItem={item => (
              <List.Item>
                <Card
                  actions={[
                    <Icon
                      type="shopping-cart"
                      style={{ fontSize: 16 }}
                      onClick={() => {
                        this.togglePayModal(true, item);
                      }}
                    />,
                    <Icon
                      theme="filled"
                      twoToneColor="#FF6F00"
                      type="star"
                      style={{ fontSize: 16 }}
                      onClick={() => {
                        this.onStarClick(item);
                      }}
                    />
                  ]}
                >
                  <Meta
                    onClick={() => {
                      this.props.description
                        ? this.onCardPackClick(item)
                        : this.onCardSimClick(item);
                    }}
                    className="sim-item"
                    avatar={<Avatar size={64} src={hamrahLogo} />}
                    title={this.props.description ? item.Title : item.Number}
                    description={
                      this.props.description ? (
                        <div>
                          {item.Numbers.map((num, i) => {
                            if (i > 2) {
                              return null;
                            }
                            return (
                              <p
                                key={i}
                                style={{
                                  direction: "ltr",
                                  textAlign: "center",
                                  fontWeight: "bold"
                                }}
                              >
                                {num}
                              </p>
                            );
                          })}
                          <p>فروشنده : ال سیم</p>
                          <p className="card-price">
                            {Separate(item.Price) + " تومان "}
                          </p>
                        </div>
                      ) : (
                        <div>
                          <p>فروشنده : ال سیم</p>
                          <p className="card-price">
                            {Separate(item.Price) + " تومان "}
                          </p>
                        </div>
                      )
                    }
                  />
                </Card>
              </List.Item>
            )}
          />
          <div
            style={{
              textAlign: "center",
              marginTop: 12,
              height: 32,
              lineHeight: "32px"
            }}
          >
            <Button
              type="primary"
              loading={this.props.loadingbt}
              onClick={this.props.onMoreClick}
            >
              {this.props.moreText}
            </Button>
          </div>
        </Card>
        <Modal
          ref={this.PayModal}
          centered
          footer={null}
          visible={this.state.showPaymentModal}
          onCancel={() => this.togglePayModalCancel(false)}
        >
          <div className="payModal">
            <h3>
              {this.props.description ? (
                <div>
                  {" "}
                  شما قصد خرید <span>
                    {this.state.selectedProduct.Title}
                  </span>{" "}
                  را دارید{" "}
                  <Icon
                    type="info-circle"
                    style={{ fontSize: 24, verticalAlign: "middle" }}
                  />
                </div>
              ) : (
                <div>
                  {" "}
                  شما قصد خرید شماره{" "}
                  <span>{this.state.selectedProduct.Number}</span> را دارید{" "}
                  <Icon
                    type="info-circle"
                    style={{ fontSize: 24, verticalAlign: "middle" }}
                  />
                </div>
              )}
            </h3>
            <h4>
              <Icon
                type="barcode"
                style={{ fontSize: 24, verticalAlign: "middle" }}
              />{" "}
              مبلغ : {this.state.selectedProduct.Price} ریال
            </h4>
            <p>
              <Icon
                type="dollar"
                style={{ fontSize: 18, verticalAlign: "middle" }}
              />{" "}
              روش پرداخت :
            </p>
            <Button.Group size="large">
              <Button type="dashed" onClick={this.onPayment}>
                <Icon type="bank" />
                پرداخت نقدی
              </Button>
              <Button type="primary">
                <Icon type="wallet" />
                پرداخت از اعتبار
              </Button>
            </Button.Group>
          </div>
        </Modal>
      </div>
    );
  }
  togglePayModal = (showPaymentModal, data) => {
    this.setState({ showPaymentModal, selectedProduct: data });
    // this.state.selectedProduct = data
  };
  onCardSimClick(item) {
    this.setState({ selectedProduct: item, redirectSim: true });
  }
  onCardPackClick(item) {
    this.setState({ selectedProduct: item, redirectPack: true });
  }
  togglePayModalCancel = () => {
    this.setState({ showPaymentModal: false, selectedProduct: {} });
  };
  onPayment = () => {
    let _token = "";
    let _localstorage = Api.getLocalStorage("User");
    if (_localstorage) {
      _token = _localstorage.Token;
    }
    let _selectedProduct = this.state.selectedProduct.ID;
    let obj = {
      Token: _token,
      Items: [_selectedProduct]
    };
    if (_token) {
      Api.Pay(obj)
        .then(res => {
          console.log(res);
        })
        .catch(err => console.log(err));
    } else {
      this.props.history.push("/login");
    }
  };
  onStarClick(item) {
    let simFav = [];
    let packFav = [];
    let isSim = _.hasIn(item, "Number");
    if (isSim) {
      let simFavLocal = Api.getLocalStorage("sim-fav");
      if (simFavLocal.length) {
        simFav.push(item);
        Api.setLocalStorage("sim-fav", simFav);
      } else {
        let isExist = _.some(simFavLocal, item);
        if (!isExist) {
          let lastData = Api.getLocalStorage("sim-fav");
          lastData.push(item);
          Api.setLocalStorage("sim-fav", lastData);
        }
      }
    } else {
    }
  }
}

export default withRouter(DataBoxComponent);
