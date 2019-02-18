import React, { Component } from "react";
import { Card, Input, Select, Icon, Slider, Button } from "antd";
import searchIcon from "../../assets/images/sim-search.png";
import Api from "../../services/api";
const Option = Select.Option;
const tabList = [
  {
    key: "tab1",
    tab: "سیم کارت "
  },
  {
    key: "tab2",
    tab: "پکیج"
  }
];

const searchField = {};
let contentList = {
  tab1: (
    <div>
      <div className="row flex-row-left">
        <div className="col-sm-2 col-xs-12">
          <Select
            showSearch
            style={{ width: "100%", fontSize: 20 }}
            placeholder="0912"
            optionFilterProp="children"
            filterOption={(input, option) =>
              option.props.children
                .toLowerCase()
                .indexOf(input.toLowerCase()) >= 0
            }
            onChange={value => {
              searchField.PreCode = value;
              console.log(searchField);
            }}
          >
            <Option value="0910" style={{ textAlign: "left" }}>
              0910
            </Option>
            <Option value="0911" style={{ textAlign: "left" }}>
              0911
            </Option>
            <Option value="0912" style={{ textAlign: "left" }}>
              0912
            </Option>
            <Option value="0913" style={{ textAlign: "left" }}>
              0913
            </Option>
          </Select>
        </div>
        <div className="col-sm-1 col-xs-4 d-none d-sm-block">
          <div className="phone-number-input">
            <Icon type="minus" />
          </div>
        </div>
        <div className="col-sm-1 col-xs-4">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num4 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-4">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num5 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-4">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num6 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-12 d-none d-sm-block ">
          <div className="phone-number-input">
            <Icon type="minus" />
          </div>
        </div>
        <div className="col-sm-1 col-xs-12">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num7 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-12">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num8 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-12 d-none d-sm-block">
          <div className="phone-number-input">
            <Icon type="minus" />
          </div>
        </div>
        <div className="col-sm-1 col-xs-12">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num9 = value.target.value;
            }}
          />
        </div>
        <div className="col-sm-1 col-xs-12">
          <Input
            className="phone-number-input"
            maxLength={1}
            placeholder="0"
            onChange={value => {
              searchField.Num10 = value.target.value;
              console.log(searchField);
            }}
          />
        </div>
      </div>
      <div className="mr-sm d-none d-sm-block" />
      <div className="row">
        <div className="col-sm-3 col-xs-12">
          <Select
            showSearch
            style={{ width: "100%", textAlign: "right" }}
            placeholder="نوع"
            optionFilterProp="children"
            onChange={value => {
              searchField.SimType = value;
            }}
          >
            <Option value="0">اعتباری</Option>
            <Option value="1">دائمی</Option>
            <Option value="2">مهم نیست</Option>
          </Select>
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
            <Option value="not">مهم نیست</Option>
          </Select>
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
        </div>
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
            onClick={this.props.onSearch}
          >
            جست و جو کن
          </Button>
        </div>
      </div>
    </div>
  ),
  tab2: (
    <div>
      <div className="row">
        <div className="col-sm-3 col-xs-12">
          <Select
            showSearch
            style={{ width: "100%", textAlign: "right" }}
            placeholder="نوع"
            optionFilterProp="children"
            filterOption={(input, option) =>
              option.props.children
                .toLowerCase()
                .indexOf(input.toLowerCase()) >= 0
            }
          >
            <Option value="etebari">اعتباری</Option>
            <Option value="daemi">دائمی</Option>
            <Option value="not">مهم نیست</Option>
          </Select>
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
        </div>
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
          />
        </div>
      </div>
      <div className="mr-lg" />
      <div className="row">
        <div className="col-sm-3 col-xs-12" />
        <div className="col-sm-3 col-xs-12" />
        <div className="col-sm-3 col-xs-12" />
        <div className="col-sm-3 col-xs-12">
          <Button type="primary" icon="search" style={{ width: "100%" }}>
            جست و جو کن
          </Button>
        </div>
      </div>
    </div>
  )
};

export class SearchComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      key: "tab1",
      noTitleKey: "app",
      SimType: 2
    };
  }

  onTabChange = (key, type) => {
    console.log(key, type);
    this.setState({ [type]: key });
  };
  formatter(value) {
    return `${value} تومان`;
  }

  render() {
    return (
      <div>
        <Card
          style={{ width: "100%" }}
          title={
            <div className="panel-header">
              <img src={searchIcon} alt="packages" />
              جست و جو سیم کارت
            </div>
          }
          tabList={tabList}
          activeTabKey={this.state.key}
          onTabChange={key => {
            this.onTabChange(key, "key");
          }}
        >
          {contentList[this.state.key]}
        </Card>
      </div>
    );
  }
}

export default SearchComponent;
