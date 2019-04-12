import React, { Component } from "react";
import fa_IR from "antd/lib/locale-provider/fa_IR";
import { LocaleProvider } from "antd";
import {
  BrowserRouter as Router,
  Switch
} from "react-router-dom";
import HomePage from "./pages/home/home.page";
import SearchPage from './pages/search/search.page'
import LoginPage from "./pages/account/login.page";
import RegisterPage from "./pages/account/register.page";
import HomeRouting from "./routes/home.routing";
import NoneRouting from "./routes/none.routing"
import ProfilePage from "./pages/profile/profile.page";
import ProductSimPage from "./pages/product/productSim.page";
import ProductPackPage from "./pages/product/productPack.page";
import hisory from "./history";
import ContactPage from "./pages/contact/contact.page";
import ApplicationPage from "./pages/application/application.page";
import ResultPage from "./pages/result/result.page";
import FavoritesPage from "./pages/favorites/favorites.page";


class Routing extends Component {
  render() {
    return (
      <LocaleProvider locale={fa_IR}>
          <Router hisory={hisory}>
            <Switch>
              <HomeRouting exact path="/" component={HomePage} />
              <HomeRouting exact path="/application" component={ApplicationPage} />
              <HomeRouting exact path="/contact" component={ContactPage} />
              <HomeRouting exact path="/result" component={ResultPage} />
              <HomeRouting exact path="/sim/:id" component={ProductSimPage} />
              <HomeRouting exact path="/pack/:id" component={ProductPackPage} />
              <HomeRouting exact path="/search/:code" component={SearchPage} />
              <HomeRouting exact path="/profile" component={ProfilePage} />
              <HomeRouting exact path="/favorites" component={FavoritesPage} />

              <NoneRouting exact path="/login" component={LoginPage} />
              <NoneRouting exact path="/register" component={RegisterPage} />

            </Switch>
          </Router>
      </LocaleProvider>
    );
  }
}
export default Routing;
