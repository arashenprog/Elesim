import Service from "./service";

export default class Api {
  // Local Storage

  static setLocalStorage(key, value) {
    let _value = JSON.stringify(value);
    return localStorage.setItem(key, _value);
  }
  static getAcyncLocalStorage(key) {
    let item = localStorage.getItem(key);
     
    return new Promise((resolve, rejcet) => {
      if (item) {
        resolve(JSON.parse(item))
      }
      rejcet()
    })

  }
  // static getAcyncLocalStorage(key){
  //   return Promise.resolve().then(()=>{
  //     localStorage.getItem(key)
  //   })
  // }
  static getLocalStorage(key){
    let item = localStorage.getItem(key);
    return JSON.parse(item)
  }
  static removeLocalStorage(key) {
    return localStorage.removeItem(key);
  }

  static clearLocalStorage() {
    return localStorage.clear();
  }

  static SendSMS(phoneNumber) {
    let obj = {
      Mobile: phoneNumber
    };
    return Service.post("Account/SendSMS", obj);
  }
  static GetToken(phoneNumber, confirmCode) {
    let obj = {
      Mobile: phoneNumber,
      Code: confirmCode
    };
    return Service.post("Account/GetToken", obj);
  }
  static SignIn(token) {
    let obj = {
      Token: token
    };
    return Service.post("Account/SignIn", obj);
  }

  static Register(userInformation) {
    return Service.post("Account/Register", userInformation);
  }
  static GetRegulars(lastId) {
    let obj = {
      LastLoadedID: lastId
    }
    return Service.post("Shop/Reqular", obj);
  }
  static GetRounds(lastId) {
    let obj = {
      LastLoadedID: lastId
    }
    return Service.post("Shop/Rond", obj)
  }

  static GetPackages(lastId) {
    let obj = {
      LastLoadedID: lastId
    }
    return Service.post("Shop/Packs", obj)
  }

  static SearchSim(searchFields) {

    return Service.post("Shop/Sim/Search", searchFields)
  }
  static SearchPack(searchFields){
    return Service.post("Shop/Sim/Pack",searchFields)
  }
  static UpdateProfile(userinfo) {
    return Service.post("Account/Save", userinfo)
  }

  static GetPaymentHistory(token) {
    let obj = {
      Token: token
    }
    return Service.post("order/history", obj)
  }

  static GetSimProduct(id) {
    console.log("GetSimProduct", id)
    return Service.get(`shop/sim/${id}`)
  }
  static GetPackProduct(id) {
    console.log("GetPackProduct", id)
    return Service.get(`shop/pack/${id}`)
  }

  static Pay(obj){
   
    return Service.post("Order/Payment",obj)
  }
  
}
