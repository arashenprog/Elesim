import React, { Component } from "react";
import { SearchComponent } from "../../components/search/search.component";
import {withRouter} from 'react-router-dom'

export class SearchPage extends Component {
  constructor(props) {
    super(props)
    this.state = {
      data: []
    }
  }
  componentWillMount() {
    window.scroll(0,0)
    let str = this.props.match.params.code;
    let res = str.split("");
    let _preCode = res[0] + res[1] + res[2] === "***" ? null : res[0] + res[1] + res[2];
    let _num4 = res[3] === "*" ? null : res[3];
    let _num5 = res[4] === "*" ? null : res[4];
    let _num6 = res[5] === "*" ? null : res[5];
    let _num7 = res[6] === "*" ? null : res[6];
    let _num8 = res[7] === "*" ? null : res[7];
    let _num9 = res[8] === "*" ? null : res[8];
    let _num10 = res[9] === "*" ? null : res[9];

    let searchItems = {
      PreCode: _preCode,
      Num4: _num4,
      Num5: _num5,
      Num6: _num6,
      Num7: _num7,
      Num8: _num8,
      Num9: _num9,
      Num10: _num10
    }
    this.setState({ searchParams: searchItems })

  }
  render() {
    return (
      <div className="contianer-fluid search-page">
        <div className="container">
          <div className="mr-lg" />

          <SearchComponent searched={this.state.searchParams} />

          <div className="mr-lg"></div>
          
        </div>
      </div>
    );
  }
}

export default withRouter(SearchPage);
