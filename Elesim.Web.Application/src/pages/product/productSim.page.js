import React, { Component } from 'react'
import { Card, Button, Modal, Icon } from 'antd';
import Api from '../../services/api';
import { Separate } from '../../components/separator/separator';
import Loading from "../../components/loading/loading.component";
import {withRouter} from 'react-router-dom'

const { Meta } = Card;
class ProductPage extends Component {
    constructor(props) {
        super(props)
        this.state = {
            product: {},
            price: "",
            showPaymentModal: false,
            loading: true
        }
    }
    componentWillMount() {
        window.scrollTo(0, 0)
        Api.GetSimProduct(this.props.match.params.id).then(res => {
            this.setState({ product: res.data.Result, price: Separate(res.data.Result.Price),loading:false })
            console.log(res.data.Result)
        }).catch(err => console.log(err))

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
                                                <h1>
                                                    <span className="ltr">
                                                        {this.state.product.Number}
                                                    </span>
                                                    &nbsp; :
                                                    خرید شماره

                                        </h1>
                                                <img alt="همراه اول" src={require("../../assets/images/hamrah-aval.png")} />
                                            </header>
                                            <ul>
                                                <li>
                                                    <span> شماره :  </span>
                                                    <span className="ltr">
                                                        {this.state.product.Number}
                                                    </span>
                                                </li>
                                                <li>
                                                    
                                                    <span>پیش شماره : </span><span>{this.state.product.Code}</span>
                                                </li>
                                                <li>
                                                    <span>قیمت : </span><span>{this.state.price} تومان</span>
                                                </li>

                                            </ul>
                                        </article>
                                    </div>
                                    <div className="col-sm-3 col-xs-12">
                                        <Card
                                            hoverable
                                        >
                                            <Meta
                                                title={"قیمت : " + this.state.price + " تومان"}
                                            />
                                            <div className="mr-lg" />
                                            <Button type="primary" className="green-button" block onClick={this.onBuyButtonClick}>خرید</Button>
                                        </Card>
                                        <div className="mr-lg" />

                                        <Card
                                            hoverable
                                            cover={<img alt="example" src={require("../../assets/images/elesim-seller.png")} />}
                                        >
                                            <Meta
                                                title="فروشنده : ال سیم"
                                                description="شماره تماس : 09135424277"
                                            />
                                            <div className="mr-lg" />
                                            <a href="tel:09135424277">
                                            <Button type="dashed" block>تماس با فروشنده</Button>
                                            </a>
                                        </Card>
                                    </div>
                                </div>
                            </React.Fragment>
                        ) : (
                                <Loading ></Loading>
                            )}
                    </div>

                </div>
                <div className="mr-lg" />
                <Modal
                    ref={this.PayModal}
                    centered
                    footer={null}
                    visible={this.state.showPaymentModal}
                    onCancel={() => this.onCancelPayModal(false)}
                >
                    <div className="payModal">
                        <h3>
                            شما قصد خرید شماره <span className="ltr">{this.state.product.Number}</span> را دارید
                        </h3>
                        <h4>
                            <Icon type="barcode" style={{ fontSize: 24, verticalAlign: "middle" }} />   مبلغ : {this.state.price} ریال
                        </h4>
                        <p>
                            <Icon type="dollar" style={{ fontSize: 18, verticalAlign: "middle" }} />  روش پرداخت :
                        </p>
                        <Button.Group size="large">
                            <Button type="dashed">
                                <Icon type="bank" />
                                پرداخت نقدی
                            </Button>
                            <Button type="primary" onClick={this.onPayWalletClick}>
                                <Icon type="wallet" />
                                پرداخت از اعتبار
                            </Button>
                        </Button.Group>
                    </div>
                </Modal>
            </div>
        )
    }
    onBuyButtonClick = () => {
        this.setState({ showPaymentModal: true })
    }
    onCancelPayModal = () => {
        this.setState({ showPaymentModal: false })
    }
    onPayWalletClick = () => {
        let token = localStorage.getItem("Token")
        if (token) {
            console.log("ok pay")
        }
        else {
            this.props.history.push("/login")
            console.log("not login")
        }
    }
}
export default withRouter(ProductPage);