import React, { Component } from 'react'
import { Card, Button, Spin, Icon } from 'antd';
import Api from '../../services/api';
import { Separate } from '../../components/separator/separator';

const { Meta } = Card;
class ProductPage extends Component {
    constructor(props) {
        super(props)
        this.state = {
            product: {},
            loading: true
        }
    }
    componentWillMount() {
        window.scrollTo(0, 0)
        Api.GetPackProduct(this.props.match.params.id).then(res => {
            console.log("res", res)
            this.setState({ product: res.data.Result, loading: false })
        }).catch(err => console.log(err))


    }
    render() {
        const loadingSpin = <Icon type="loading" style={{ fontSize: 24 }} spin />;
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
                                                <h1>
                                                    {this.state.product.Title}
                                                </h1>
                                                <img alt="همراه اول" src={require("../../assets/images/hamrah-aval.png")} />
                                            </header>
                                            <ul>
                                                <li>
                                                    شماره : {this.state.product.Number}
                                                </li>
                                                <li>
                                                    پیش شماره : {this.state.product.Code}
                                                </li>
                                                <li>
                                                    قیمت : {this.state.product.Price}
                                                </li>
                                                <li>
                                                    تاریخ ثبت : {this.state.product.RegisterTime}
                                                </li>
                                            </ul>
                                        </article>
                                    </div>
                                    <div className="col-sm-3 col-xs-12">
                                        <Card
                                            hoverable
                                        >
                                            <Meta
                                                title={"قیمت : " + Separate(this.state.product.Price) + " تومان"}
                                            />
                                            <div className="mr-lg" />
                                            <Button type="primary" block>خرید</Button>
                                        </Card>
                                        <div className="mr-lg" />

                                        <Card
                                            hoverable
                                            cover={<img alt="example" src="https://via.placeholder.com/250x250" />}
                                        >
                                            <Meta
                                                title="فروشنده : ال سیم"
                                                description="شماره تماس : 09135424277"
                                            />
                                            <div className="mr-lg" />
                                            <Button type="dashed" block>تماس با فروشنده</Button>
                                        </Card>
                                    </div>
                                </div>
                            </React.Fragment>
                        ) : (
                                <React.Fragment>
                                    <Spin indicator={loadingSpin} />
                                </React.Fragment>)}
                    </div>
                </div>
                <div className="mr-lg" />

            </div>
        )
    }
}
export default ProductPage;