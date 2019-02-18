import React from "react";
import { Spin, Icon } from "antd";
import "./loading.component.scss"
const loadingSpin = <Icon type="loading" style={{ display: "block", margin: "auto", textAlign: "center" }} spin />;

const loading = () => {
    return (
        <React.Fragment>
            <div className="loading-box">
                <Spin size="large" indicator={loadingSpin} />
                <span>
                    در حال بارگذاری
                </span>
            </div>
        </React.Fragment>
    )
}
export default loading;