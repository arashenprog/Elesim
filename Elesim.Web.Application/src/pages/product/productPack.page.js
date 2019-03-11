import React, { Component } from "react";
import { Card, Button } from "antd";
import Api from "../../services/api";
import { Separate } from "../../components/separator/separator";
import Loading from "../../components/loading/loading.component";

const { Meta } = Card;
class ProductPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      product: {},
      loading: true,
      price: ""
    };
  }
  componentWillMount() {
    window.scrollTo(0, 0);
    Api.GetPackProduct(this.props.match.params.id)
      .then(res => {
        console.log("res", res);
        this.setState({
          product: res.data.Result,
          loading: false,
          price: Separate(res.data.Result.Price)
        });
        console.log(this.state.product);
      })
      .catch(err => console.log(err));
  }
  render() {
    return (
      <div>
        <div className="contianer-fluid search-page">
          <div className="container">
            <div className="mr-lg" />
            {!this.state.loading ? (
              <React.Fragment>
                <div className="row">
                  <div className="col-sm-9 col-xs-12">
                    <article className="product-article">
                      <header>
                        <h1>{this.state.product.Title}</h1>
                        <img
                          alt="همراه اول"
                          src={require("../../assets/images/hamrah-aval.png")}
                        />
                      </header>
                      <ul>
                        <li>
                          <span>شماره ها : &nbsp; </span>
                          <ol>
                            {this.state.product.Numbers.map(number => {
                              return (
                                <li key={"number" + Math.random()}>{number}</li>
                              );
                            })}
                          </ol>
                        </li>
                        <li>
                          <span> عنوان : </span>
                          <span>{this.state.product.Title}</span>
                        </li>
                        <li>
                          <span>قیمت :</span>{" "}
                          <span>{this.state.price} تومان</span>
                        </li>
                      </ul>
                    </article>
                  </div>
                  <div className="col-sm-3 col-xs-12">
                    <Card hoverable>
                      <Meta
                        title={
                          "قیمت : " + Separate(this.state.price) + " تومان"
                        }
                      />
                      <div className="mr-lg" />
                      <Button type="primary" className="green-button" block>
                        خرید
                      </Button>
                    </Card>
                    <div className="mr-lg" />

                    <Card
                      hoverable
                      cover={
                        <img
                          alt="example"
                          src="https://via.placeholder.com/250x250"
                        />
                      }
                    >
                      <Meta
                        title="فروشنده : ال سیم"
                        description="شماره تماس : 09135424277"
                      />
                      <div className="mr-lg" />
                      <a href="tel:09135424277">
                        <Button type="dashed" block>
                          تماس با فروشنده
                        </Button>
                      </a>
                    </Card>
                  </div>
                </div>
              </React.Fragment>
            ) : (
              <Loading />
            )}
          </div>
        </div>
        <div className="mr-lg" />
      </div>
    );
  }
}
export default ProductPage;
