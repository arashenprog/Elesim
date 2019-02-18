import React, { Component } from "react";
import { List, Card, Avatar, Icon, Button, Modal } from "antd";
import SearchComponent from "../../components/search/search.component";
import Api from "../../services/api";
import hamrahLogo from "../../assets/images/hamrah-aval.png";
import SideMenu from "../../components/sidemenu/sidemenu.component";
import Slider from "../../components/slider/slider.component";
import packageIcon from "../../assets/images/sim-pack.png";
import rondIcon from "../../assets/images/sim-rond.png";
import normalIcon from "../../assets/images/sim-def.png";
import { Separate } from "../../components/separator/separator";

const { Meta } = Card;
const regularData = [];
const roundData = [];
const packData = [];

export class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      regular: [],
      packages: [],
      loadingRegular: true,
      loadingRound: true,
      loadingPacks: true,
      showPaymentModal: false,
      selectedProduct: {}
    };
    this.loadMoreRegular = this.loadMoreRegular.bind(this);
    this.loadMoreRound = this.loadMoreRound.bind(this);
    this.loadMorePacks = this.loadMorePacks.bind(this);
    this._renderPackagesNumber = this._renderPackagesNumber.bind(this);
    this.PayModal = React.createRef();
  }

  componentWillMount() {
    console.log(this.state)
    Api.GetRegulars(0)
      .then(res => {
        regularData.push(...res.data.Result);
        this.setState({ loadingRegular: false });
      })
      .catch(err => console.log(err));

    Api.GetRounds(0)
      .then(res => {
        roundData.push(...res.data.Result);
        this.setState({ loadingRound: false });
      })
      .catch(err => console.log(err));
    Api.GetPackages(0)
      .then(res => {
        packData.push(...res.data.Result);
        this.setState({ loadingRound: false });
      })
      .catch(err => console.log(err));
  }
  _renderPackagesNumber(item) {
    item.map(number => {
      return <p>{number}</p>;
    });
  }
  render() {
    return (
      <div>
        <div className="mr-lg"></div>
        <div className="container">
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
          <Card
            title={
              <div className="panel-header">
                <img src={normalIcon} alt="packages" />
                سیم کارت های معمولی
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
              dataSource={regularData}
              renderItem={item => (
                <List.Item >
                  <Card
                    onClick={this.onCardClick(item)}
                    actions={[
                      <Icon
                        type="shopping-cart"
                        style={{ fontSize: 16 }}
                        onClick={() => {
                          this.togglePayModal(true, item)
                        }}
                      />,
                      <Icon type="star" style={{ fontSize: 16 }} />,

                    ]}
                  >
                    <Meta
                      className="sim-item"
                      avatar={<Avatar size={64} src={hamrahLogo} />}
                      title={item.Number}
                      description={
                        <div>
                          <p>فروشنده : ال سیم</p>
                          <p className="card-price">
                            {Separate(item.Price) + " تومان "}
                          </p>
                        </div>

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
                loading={this.state.loadingRegular}
                onClick={this.loadMoreRegular}
              >
                موراد بیشتر
                  </Button>
            </div>
          </Card>
          <div className="mr-lg" />
          <Card
            title={
              <div className="panel-header">
                <img src={rondIcon} alt="packages" />
                سیم کارت های رُند
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
              dataSource={roundData}
              renderItem={item => (
                <List.Item>
                  <Card

                    actions={[
                      <Icon
                        style={{ fontSize: 16 }}
                        type="shopping-cart"
                      />,
                      <Icon type="star" style={{ fontSize: 16 }} />,

                    ]}
                  >
                    <Meta
                      className="sim-item"
                      avatar={<Avatar size={64} src={hamrahLogo} />}
                      title={item.Number}
                      description={
                        <div>
                          <p>فروشنده : ال سیم</p>
                          <p className="card-price">
                            {Separate(item.Price) + " تومان "}
                          </p>
                        </div>

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
                loading={this.state.loadingRound}
                onClick={this.loadMoreRound}
              >
                موراد بیشتر
                  </Button>
            </div>
          </Card>
          <div className="mr-xl" />
          <div className="mr-lg" />
          <div className="row">
            <div className="col-sm-12">
              <Card
                title={
                  <div className="panel-header">
                    <img src={packageIcon} alt="packages" />
                    پکیج ها
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
                  dataSource={packData}
                  renderItem={item => (
                    <List.Item>
                      <Card
                        actions={[
                          <Icon
                            type="shopping-cart"
                            style={{ fontSize: 16 }}

                          />,
                          <Icon type="star" style={{ fontSize: 16 }} />,

                        ]}
                      >
                        <Meta
                          className="sim-item"
                          avatar={<Avatar size={64} src={hamrahLogo} />}
                          title={item.Title}
                          // description={item.Numbers.map(number => {
                          //   return <p style={{direction:"ltr",textAlign:"center",fontWeight:"bold"}}>{number}</p>;
                          // })}
                          description={
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
                    loading={this.state.loadingRound}
                    onClick={this.loadMorePacks}
                  >
                    موراد بیشتر
                  </Button>
                </div>
              </Card>
              <div className="mr-xl" />
            </div>
          </div>
        </div>
        <Modal
          ref={this.PayModal}
          centered
          visible={this.state.showPaymentModal}
          onOk={() => this.togglePayModal(false)}
          onCancel={() => this.togglePayModalCancel(false)}
        >
          <div className="payModal">
            <h3>
              شما قصد خرید شماره <span>{this.state.selectedProduct.Number}</span> را دارید
          </h3>
            <h4>
              مبلغ : {this.state.selectedProduct.Price} ریال
          </h4>
          </div>
        </Modal>
      </div>
    );
  }
  loadMoreRegular() {
    let lastObject = regularData[regularData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingRegular: true });
    Api.GetRegulars(lastId).then(res => {
      regularData.push(...res.data.Result);
      this.setState({ loadingRegular: false });
    });
  }
  loadMoreRound() {
    let lastObject = regularData[roundData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingRound: true });
    Api.GetRounds(lastId).then(res => {
      roundData.push(...res.data.Result);
      this.setState({ loadingRound: false });
    });
  }
  loadMorePacks() {
    let lastObject = regularData[roundData.length - 1];
    let lastId = lastObject.ID;
    this.setState({ loadingPacks: true });
    Api.GetPackages(lastId).then(res => {
      packData.push(...res.data.Result);
      this.setState({ loadingPacks: false });
    });
  }
  togglePayModal = (showPaymentModal, data) => {
    this.setState({ showPaymentModal })
    this.state.selectedProduct = data
    console.log(this.state.selectedProduct)

  }
  onCardClick = (e) => {
    console.log(e)
  }
  togglePayModalCancel = () => {
    this.setState({ showPaymentModal: false, selectedProduct: {} })
  }

}

export default HomePage;
