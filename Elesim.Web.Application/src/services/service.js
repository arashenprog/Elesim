
import Axios from "axios";

export default class Service {
    static baseURL = "http://elesim.ir:2506/";

    // Get Method
    static get(_params) {
        const AuthKey = "";
        return new Promise((resolve, reject) => {
            var url = this.baseURL + _params;
            Axios.get(url, { headers: { Authorization: AuthKey } })
                .then(response => {
                    resolve(response)
                })
                .catch(error => {
                    reject(error);
                });
        });
    }

    // Post Method
    static post(_url, _params) {
        // const AuthKey = "";
        return new Promise((resolve, reject) => {
            var url = this.baseURL + _url;
            Axios.post(url, _params)
                .then(response => {
                    resolve(response)
                })
                .catch(error => {
                    reject(error);
                });
        });
    }
 
}