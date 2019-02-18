import React, { Component } from 'react'
import { Card, Icon, Avatar, Skeleton } from "antd";
import {Separate} from "../separator/separator"

const Meta = Card;
export class CardComponent extends Component {
  render() {
    return (
      <React.Fragment>
        <Card
          actions={[
            <Icon
              style={{ fontSize: 16 }}
              type="shopping-cart"
              onClick={this.props.onClickBuy}
            />,
            <Icon type="star" style={{ fontSize: 16 }} onClick={this.props.onClickStar} />,

          ]}
        >
          <Skeleton loading={this.props.loading} active avatar>
            <Meta
              className="sim-item"
              avatar={<Avatar size={64} src={this.props.avatar} />}
              title={this.props.title}
              description={
                <p className="card-price">
                  {this.props.price}
                </p>
              }
            />
          </Skeleton>
        </Card>
      </React.Fragment>
    )
  }
}

export default CardComponent
