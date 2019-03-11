import React, { Component } from "react";
import {
  Card,
  Select,
  Icon,
  Slider,
  Button,
  Input,
  List,
  Avatar,
  Skeleton,
  Modal
} from "antd";
import searchIcon from "../../assets/images/sim-search.png";
import { Redirect } from "react-router-dom";
import hamrahLogo from "../../assets/images/hamrah-aval.png";
import { Tab, Tabs, TabList, TabPanel } from "react-tabs";
import Loading from "../loading/loading.component";
import Api from "../../services/api";
import { withRouter } from "react-router-dom";
const { Meta } = Card;
const Option = Select.Option;
const searchField = {};

export class SearchComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: [],
      loadingbt: false,
      preCode: "",
      num4: "",
      num5: "",
      num6: "",
      num7: "",
      num8: "",
      num9: "",
      num10: "",
      simNormalType: "",
      simPriceMin: "",
      simPriceMax: "",
      simLastId: "",
      packPriceMax: "",
      packPriceMin: "",
      loading: false,
      redirectSim: false,
      selectedProduct: {},
      payModal: false
    };
  }

  formatter(value) {
    return `${value} تومان`;
  }

  componentWillMount() {
    let _recivedData = this.props.searched;
    if (_recivedData !== undefined) {
      let { PreCode, Num4, Num5, Num6, Num7, Num8, Num9, Num10 } = _recivedData;
      this.setState({
        preCode: PreCode,
        num4: Num4,
        num5: Num5,
        num6: Num6,
        num7: Num7,
        num8: Num8,
        num9: Num9,
        num10: Num10
      });
      if (window.location.href.indexOf("search") > -1) {
        this.setState({ loading: true });
        Api.SearchSim(_recivedData)
          .then(res => {
            this.setState({ data: res.data.Result, loading: false });
          })
          .catch(err => console.log(err));
      }
    }
  }

  render() {
    if (this.state.redirect) {
      const { preCode, num4, num5, num6, num7, num8, num9, num10 } = this.state;
      return (
        <Redirect
          push
          to={`search/${(preCode ? preCode : "***") +
            (num4 ? num4 : "*") +
            (num5 ? num5 : "*") +
            (num6 ? num6 : "*") +
            (num7 ? num7 : "*") +
            (num8 ? num8 : "*") +
            (num9 ? num9 : "*") +
            (num10 ? num10 : "*")}`}
        />
      );
    }
    if (this.state.redirectSim) {
      return <Redirect to={`/sim/${this.state.selectedProduct.ID}`} />;
    }

    return (
      <div>
        <Card
          style={{ width: "100%" }}
          className="search-card"
          title={
            <div className="panel-header">
              <img src={searchIcon} alt="packages" />
              جست و جو سیم کارت
            </div>
          }
        >
          <Tabs>
            <TabList>
              <Tab>سیم کارت</Tab>
              <Tab>پکیج</Tab>
            </TabList>

            <TabPanel>
              <div className="row flex-row-left">
                <div className="col-sm-2 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%" }}
                    placeholder="912"
                    optionFilterProp="children"
                    value={this.state.preCode}
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                    onChange={value => {
                      this.setState({ preCode: value });
                    }}
                  >
                    <Option value="" style={{ textAlign: "left" }}>
                      مهم نیست
                    </Option>
                    <Option value="910" style={{ textAlign: "left" }}>
                      910
                    </Option>
                    <Option value="911" style={{ textAlign: "left" }}>
                      911
                    </Option>
                    <Option value="912" style={{ textAlign: "left" }}>
                      912
                    </Option>
                    <Option value="913" style={{ textAlign: "left" }}>
                      913
                    </Option>
                  </Select>
                </div>
                <div className="col-sm-1 col-xs-4 d-none d-sm-block">
                  <div className="phone-number-input">
                    <Icon type="minus" />
                  </div>
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-4">
                  <Input
                    ref={input => {
                      this.Num4 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    value={this.state.num4}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num4: e.target.value });
                      this.Num5.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-4">
                  <Input
                    ref={input => {
                      this.Num5 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num5: e.target.value });
                      this.Num6.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-4">
                  <Input
                    ref={input => {
                      this.Num6 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num6: e.target.value });
                      this.Num7.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12 d-none d-sm-block ">
                  <div className="phone-number-input">
                    <Icon type="minus" />
                  </div>
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12">
                  <Input
                    ref={input => {
                      this.Num7 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num7: e.target.value });
                      this.Num8.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12">
                  <Input
                    ref={input => {
                      this.Num8 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num8: e.target.value });
                      this.Num9.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12 d-none d-sm-block">
                  <div className="phone-number-input">
                    <Icon type="minus" />
                  </div>
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12">
                  <Input
                    ref={input => {
                      this.Num9 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num9: e.target.value });
                      this.Num10.focus();
                    }}
                  />
                </div>
                <div className="mr-sm d-block d-sm-none" />

                <div className="col-sm-1 col-xs-12">
                  <Input
                    ref={input => {
                      this.Num10 = input;
                    }}
                    className="phone-number-input"
                    maxLength={1}
                    placeholder="0"
                    onChange={e => {
                      this.setState({ num10: e.target.value });
                    }}
                  />
                </div>
              </div>
              <div className="mr-sm d-block d-sm-none" />
              <div className="mr-sm d-block d-sm-block" />

              <div className="row">
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="نوع"
                    optionFilterProp="children"
                    onChange={value => {
                      searchField.SimType = value;
                      this.setState({ simNormalType: value });
                    }}
                  >
                    <Option value="0">اعتباری</Option>
                    <Option value="1">دائمی</Option>
                    <Option value="">مهم نیست</Option>
                  </Select>
                </div>
                <div className="mr-sm d-block d-sm-none" />
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="اپراتور"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                  >
                    <Option value="hamrah">همراه اول</Option>
                    <Option value="not">مهم نیست</Option>
                  </Select>
                </div>
                <div className="mr-sm d-block d-sm-none" />
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="وضعیت"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                  >
                    <Option value="normal">معمولی</Option>
                    <Option value="round">رند</Option>
                    <Option value="not">مهم نیست</Option>
                  </Select>
                </div>
                <div className="mr-sm d-block d-sm-none" />
                <div className="col-sm-3 col-xs-12">
                  <Slider
                    range
                    min={20000}
                    max={100000}
                    step={1000}
                    defaultValue={[20000, 50000]}
                    tipFormatter={value => {
                      return `${value}تومان`;
                    }}
                    onChange={this.onSimPriceChange}
                  />
                </div>
              </div>
              <div className="mr-lg" />
              <div className="row">
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12">
                  <Button
                    type="primary"
                    icon="search"
                    style={{ width: "100%" }}
                    onClick={this.onSearchSimClick}
                  >
                    جست و جو کن
                  </Button>
                </div>
              </div>
            </TabPanel>
            <TabPanel>
              <div className="row">
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    onSelect={value => {
                      console.log(value);
                      this.setState({ simPackType: value });
                    }}
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="نوع"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                  >
                    <Option value="0">اعتباری</Option>
                    <Option value="1">دائمی</Option>
                    <Option value="">مهم نیست</Option>
                  </Select>
                  <div className="mr-sm d-block d-sm-block" />
                </div>
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="اپراتور"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                  >
                    <Option value="hamrah">همراه اول</Option>
                    <Option value="irancell">ایرانسل</Option>
                    <Option value="rightel">رایتل</Option>
                    <Option value="not">مهم نیست</Option>
                  </Select>
                  <div className="mr-sm d-block d-sm-block" />
                </div>
                <div className="col-sm-3 col-xs-12">
                  <Select
                    showSearch
                    style={{ width: "100%", textAlign: "right" }}
                    placeholder="وضعیت"
                    optionFilterProp="children"
                    filterOption={(input, option) =>
                      option.props.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                  >
                    <Option value="normal">معمولی</Option>
                    <Option value="round">رند</Option>
                    <Option value="not">مهم نیست</Option>
                  </Select>
                  <div className="mr-sm d-block d-sm-block" />
                </div>
                <div className="col-sm-3 col-xs-12">
                  <Slider
                    range
                    min={20000}
                    max={100000}
                    step={1000}
                    onChange={this.onPackPriceChange}
                    defaultValue={[20000, 50000]}
                    tipFormatter={value => {
                      return `${value}تومان`;
                    }}
                  />
                </div>
              </div>
              <div className="mr-sm d-block d-sm-none" />
              <div className="mr-sm d-block d-sm-block" />
              <div className="row">
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12" />
                <div className="col-sm-3 col-xs-12">
                  <Button
                    type="primary"
                    icon="search"
                    style={{ width: "100%" }}
                    onClick={this.onSearchPackageClick}
                  >
                    جست و جو کن
                  </Button>
                </div>
              </div>
            </TabPanel>
          </Tabs>
        </Card>
        <div className="mr-lg" />
        {!this.state.loading ? (
          <React.Fragment>
            {this.state.data.length >= 1 ? (
              <div>
                <Card
                  id="search-result"
                  title={<div className="panel-header">نتایج جستجو</div>}
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
                    dataSource={this.state.data}
                    renderItem={item => (
                      <List.Item>
                        <Card
                          actions={[
                            <Icon
                              style={{ fontSize: 16 }}
                              type="shopping-cart"
                              onClick={() => {
                                this.togglePayModal(true, item);
                              }}
                            />,
                            <Icon type="star" style={{ fontSize: 16 }} />
                          ]}
                        >
                          <Skeleton loading={this.state.loading} active avatar>
                            <Meta
                              onClick={() => {
                                this.onCardSimClick(item);
                              }}
                              className="sim-item"
                              avatar={<Avatar size={64} src={hamrahLogo} />}
                              title={item.Number}
                              description={
                                <p className="card-price">
                                  {item.Price + " تومان "}
                                </p>
                              }
                            />
                          </Skeleton>
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
                      loading={this.state.loadingbt}
                      onClick={this.onMoreClick}
                    >
                      موارد بیشتر
                    </Button>
                  </div>
                  <Modal
                    ref={this.PayModal}
                    centered
                    footer={null}
                    visible={this.state.showPaymentModal}
                    onCancel={() => this.togglePayModalCancel(false)}
                  >
                    <div className="payModal">
                      <h3>
                        <div>
                          {" "}
                          شما قصد خرید شماره{" "}
                          <span>{this.state.selectedProduct.Number}</span> را
                          دارید{" "}
                          <Icon
                            type="info-circle"
                            style={{ fontSize: 24, verticalAlign: "middle" }}
                          />
                        </div>
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
                </Card>
              </div>
            ) : (
              <div>
                {window.location.href.indexOf("search") > -1 ? (
                  <p style={{ textAlign: "center" }}>نتیجه ای یافت نشد</p>
                ) : (
                  ""
                )}
              </div>
            )}
          </React.Fragment>
        ) : (
          <Loading />
        )}

        <div className="mr-lg" />
      </div>
    );
  }

  onCardSimClick = item => {
    console.log(item);
    this.setState({ selectedProduct: item, redirectSim: true });
  };
  onMoreClick = () => {
    let lastObject = this.state.data[this.state.data.length - 1];
    let lastId = lastObject.ID;
    this.setState({ simLastId: lastId, loadingbt: true }, () => {
      let searchItems = {
        PreCode: this.state.preCode,
        Num4: this.state.num4,
        Num5: this.state.num5,
        Num6: this.state.num6,
        Num7: this.state.num7,
        Num8: this.state.num8,
        Num9: this.state.num9,
        Num10: this.state.num10,
        MaxPrice: this.state.simPriceMax,
        MinPrice: this.state.simPriceMin,
        SimType: this.state.simNormalType,
        LastLoadedId: this.state.simLastId
      };
      Api.SearchSim(searchItems)
        .then(res => {
          this.state.data.push(...res.data.Result);
          console.log("ressssss", ...this.state.data);
          this.setState({ loadingbt: false });
        })
        .catch(err => console.log(err));
    });

    // });
  };

  onSearchSimClick = () => {
    this.setState({ loading: true });
    let searchItems = {
      PreCode: this.state.preCode,
      Num4: this.state.num4,
      Num5: this.state.num5,
      Num6: this.state.num6,
      Num7: this.state.num7,
      Num8: this.state.num8,
      Num9: this.state.num9,
      Num10: this.state.num10,
      MaxPrice: this.state.simPriceMax,
      MinPrice: this.state.simPriceMin,
      SimType: this.state.simNormalType
    };
    if (window.location.href.indexOf("search") > -1) {
      Api.SearchSim(searchItems)
        .then(res => {
          this.setState({ data: res.data.Result, loading: false });
        })
        .catch(err => console.log(err));
    } else {
      Api.SearchSim(searchItems)
        .then(res => {
          this.setState({ data: res.data.Result });
        })
        .catch(err => console.log(err));
      this.setState({ redirect: true, loading: false });
    }
  };

  onSimPriceChange = value => {
    this.setState({ simPriceMin: value[0], simPriceMax: value[1] });
  };

  onSearchPackageClick = () => {
    this.setState({ loading: true });

    let searchItem = {
      lastloadedid: 0
    };
    Api.SearchPack(searchItem)
      .then(res => {
        this.setState({ data: res.data.Result, loading: false });
      })
      .catch(err => console.log(err));
  };
  onPackPriceChange = value => {
    this.setState({ packPriceMin: value[0], packPriceMax: value[1] });
  };
  togglePayModalCancel = () => {
    this.setState({ showPaymentModal: false, selectedProduct: {} });
  };
  togglePayModal = (showPaymentModal, data) => {
    this.setState({ showPaymentModal, selectedProduct: data });
    // this.state.selectedProduct = data
    console.log(this.state.selectedProduct);
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
}

export default withRouter(SearchComponent);
